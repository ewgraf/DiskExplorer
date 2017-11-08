using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DiskExplorer.Entities;

namespace DiskExplorer {
    public partial class DiscoverForm : Form {
        private static double GB = Math.Pow(2, 30);
        private readonly static object share = new object();
        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private readonly Explorer _explorer = new Explorer();
        private readonly string _folder;
        private readonly string _pattern;
        private readonly IProgress<(int, long, string)> _discoveryProgress;
        private readonly IProgress<(int, long, FileInfoExtended, TimeSpan, long, string)> _scanProgress;
        

        public FileInfoExtended[] Files { get; private set; }

        public DiscoverForm(string folder, string pattern) {
            InitializeComponent();

            //this.progressTimer.Interval = 100;

            _folder = folder;
            _pattern = pattern;
            _discoveryProgress = new Progress<(int, long, string)>(value => {
                if(!_toUpdateDiscover) {
                    return;
                }
                this.filesDiscoveredLabel.Text = value.Item1.ToString();
                this.sizeDiscoveredLabel.Text = $"{value.Item2 / GB: 0.##} GB";
                this.fileDiscoveringLabel.Text = value.Item3.ToString();
                this._toUpdateDiscover = false;
            });
            _scanProgress = new Progress<(int, long, FileInfoExtended, TimeSpan, long, string)>(value => {
                lock (share) {
                    if (!_toUpdateScan) {
                        return;
                    }
                    if (_scanComplete) {
                        return;
                    }
                    if(value.Item6 != null) {
                        this.richTextBox1.Text = this.richTextBox1.Text.Insert(0, $"{value.Item6}{Environment.NewLine}");
                        return;
                    }
                    int filesHashed = value.Item1;
                    long filesSizeDiscovered = value.Item2;
                    FileInfoExtended f = value.Item3;
                    TimeSpan scannedIn = value.Item4;
                    long threadId = value.Item5;
                    this.filesScannedLabel.Text = filesHashed.ToString();
                    this.sizeScannedLabel.Text = FileUtils.SizeSuffix(filesSizeDiscovered).ToString();
                    this.fileScanningLabel.Text = f.FullPath.ToString();
                    this.currentFileSizeLabel.Text = f.SizeWithPrefix();
                    this.scanProgressBar.Value = filesHashed;
                    this._toUpdateScan = false;
                    this.richTextBox1.Text = this.richTextBox1.Text.Insert(0, $"size: {f.SizeWithPrefix()}\tin: {scannedIn}\t{f.Name}{Environment.NewLine}");
                    //Application.DoEvents();
                }
            });
        }

        private bool _toUpdateDiscover = true;
        private bool _toUpdateScan = true;
        private static bool _scanComplete = false;

        private async void DiscoverForm_Load(object sender, EventArgs e) {
            this.progressTimer.Start();
            this.Files = await Task.Run(() => _explorer.GetFiles(
                    _folder,
                    _pattern,
                    () => _toUpdateDiscover,
                    _discoveryProgress,
                    _cancellationTokenSource
                )
                .ToArray(),
                _cancellationTokenSource.Token);
            _toUpdateScan = true;
            _scanComplete = false;
            _discoveryProgress.Report((this.Files.Length, this.Files.Sum(f => f.Length), this.Files.LastOrDefault()?.FullPath ?? "not loaded yet"));
            this.scanGroupBox.Enabled = true;
            this.discoveringProgressBar.Style = ProgressBarStyle.Blocks;
            this.scanProgressBar.Maximum = this.Files.Length;
            this.Files = await Task.Run(() => Files.ComputeHashesParallel(
                () => _toUpdateScan,
                _scanProgress,
                _cancellationTokenSource
            ));
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
