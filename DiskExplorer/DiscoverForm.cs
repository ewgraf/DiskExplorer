using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DiskExplorer.Entities;

namespace DiskExplorer {
    public partial class DiscoverForm : Form {
        private static readonly double GB = Math.Pow(2, 30);
        private static readonly object Share = new object();
        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private readonly Explorer _explorer = new Explorer();
        private readonly string _folder;
        private readonly string _pattern;
        private readonly IProgress<(int, long, string)> _discoveryProgress;
        private readonly IProgress<(FileInfoExtended, TimeSpan, long, string)> _scanProgress;        

        public Folder Root { get; set; }
        public FileInfoExtended[] Files { get; private set; }
        public Folder[] FoldersTree { get; private set; }
        public HashProgress _progress { get; private set; }

        public class HashProgress {
            public int Hashed { get; set; }
            public int Of { get; set; }
            public override string ToString() => $"{Hashed}/{Of}";
        }

        public DiscoverForm(string folder, string pattern) {
            InitializeComponent();

            _folder = folder;
            _pattern = pattern;
            _progress = new HashProgress {
                Hashed = 0,
                Of = 0
            };
            _discoveryProgress = new Progress<(int, long, string)>(value => {
                this.filesDiscoveredLabel.Text = value.Item1.ToString();
                this.sizeDiscoveredLabel.Text = $"{value.Item2 / GB: 0.##} GB";
                this.fileDiscoveringLabel.Text = value.Item3.ToString();
                this._toUpdateDiscover = false;
            });
            long filesSizeDiscovered = 0;
            _scanProgress = new Progress<(FileInfoExtended, TimeSpan, long, string)>(value => {
                lock (Share) {
                    if (!_toUpdateScan) {
                        return;
                    }
                    if (_scanComplete) {
                        return;
                    }
                    if(value.Item4 != null) {
                        this.richTextBox1.Text = this.richTextBox1.Text.Insert(0, $"{value.Item4}{Environment.NewLine}");
                        return;
                    }
                    FileInfoExtended f = value.Item1;
                    filesSizeDiscovered += f.Length;
                    TimeSpan scannedIn = value.Item2;
                    long threadId = value.Item3;
                    _progress.Hashed++;
                    this.filesScannedLabel.Text = _progress.ToString();
                    this.sizeScannedLabel.Text = FileUtils.SizeSuffix(filesSizeDiscovered).ToString();
                    this.fileScanningLabel.Text = f.FullPath.ToString();
                    this.currentFileSizeLabel.Text = f.SizeWithPrefix();
                    this.scanProgressBar.Value = _progress.Hashed;
                    this._toUpdateScan = false;
                    this.richTextBox1.Text = this.richTextBox1.Text.Insert(0, $"size: {f.SizeWithPrefix()}\tin: {scannedIn}\t{f.Name}{Environment.NewLine}");
                    //Application.DoEvents();
                }
            });
        }

        private bool _toUpdateDiscover = true;
        private bool _toUpdateScan = true;
        private static bool _scanComplete;

        private async void DiscoverForm_Load(object sender, EventArgs e) {
            this.progressTimer.Start();
            this.Root = await Task.Run(() => _explorer.GetFilesTree(
                    _folder,
                    _pattern,
                    () => _toUpdateDiscover,
                    _discoveryProgress,
                    _cancellationTokenSource
                ),
                _cancellationTokenSource.Token);
            //this.Files = await Task.Run(() => _explorer.GetFiles(
            //        _folder,
            //        _pattern,
            //        () => _toUpdateDiscover,
            //        _discoveryProgress,
            //        _cancellationTokenSource
            //    )
            //    .ToArray(),
            //    _cancellationTokenSource.Token);
            _toUpdateScan = true;
            _scanComplete = false;
            this.scanGroupBox.Enabled = true;
            this.discoveringProgressBar.Style = ProgressBarStyle.Blocks;
            this.scanProgressBar.Maximum = this.Root.FilesTotal;
            //this.Files = await Task.Run(() => Files.ComputeHashesParallel(
            //    () => _toUpdateScan,
            //    _scanProgress,
            //    _cancellationTokenSource
            //));
            //_progress.Of = this.Root.FilesTotal;
            //this.Root = await Task.Run(() => this.Root.ComputeHashesParallel(
            //    () => _toUpdateScan,
            //    _scanProgress,
            //    _cancellationTokenSource
            //));
            this.progressTimer.Stop();
            this.DialogResult = DialogResult.OK;
            this.Close();
            _scanComplete = true;
        }

        private void progressTimer_Tick(object sender, EventArgs e) {
            _toUpdateDiscover = true;
            _toUpdateScan = true;
        }
        private void cancelButton_Click(object sender, EventArgs e) => _cancellationTokenSource.Cancel();
    }
}
