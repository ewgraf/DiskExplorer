using DiskExplorer.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiskExplorer {
    public partial class DiscoverForm : Form {
        private static double GB = Math.Pow(2, 30);
        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private readonly Explorer _explorer = new Explorer();
        private readonly string _folderPath;
        private readonly IProgress<(int, long, string)> _discoveryProgress;
        private readonly IProgress<(int, long, string)> _scanProgress;
        private bool _toUpdate;

        public FileInfoExtended[] Files { get; private set; }

        public DiscoverForm(string folderPath) {
            InitializeComponent();

            //this.progressTimer.Interval = 100;

            _folderPath = folderPath;
            _discoveryProgress = new Progress<(int, long, string)>(value => {
                this.filesDiscoveredLabel.Text = value.Item1.ToString();
                this.sizeDiscoveredLabel.Text = $"{value.Item2 / GB: 0.##} GB";
                this.fileDiscoveringLabel.Text = value.Item3.ToString();
                this._toUpdate = false;
            });
            _scanProgress = new Progress<(int, long, string)>(value => {
                this.filesScannedLabel.Text = value.Item1.ToString();
                this.sizeScannedLabel.Text = $"{value.Item2 / GB : 0.##} GB";
                this.fileScanningLabel.Text = value.Item3.ToString();
                this.scanProgressBar.Value = value.Item1;
                this._toUpdate = false;
            });
        }

        private async void DiscoverForm_Load(object sender, EventArgs e) {
            this.progressTimer.Start();
            this.Files = await Task.Run(() => _explorer.GetFiles(_folderPath,
                    () => _toUpdate,
                    _discoveryProgress,
                    _cancellationTokenSource
                )
                .ToArray(),
                _cancellationTokenSource.Token);
            _discoveryProgress.Report((this.Files.Length, this.Files.Sum(f => f.Length), this.Files.Last().FullPath));
            this.scanGroupBox.Enabled = true;
            this.discoveringProgressBar.Style = ProgressBarStyle.Blocks;
            this.scanProgressBar.Maximum = this.Files.Length;
            this.Files = await Task.Run(() => Files.ComputeHashesParallel(
                () => _toUpdate,
                _scanProgress
            ), _cancellationTokenSource.Token);
            this.progressTimer.Stop();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void progressTimer_Tick(object sender, EventArgs e) => _toUpdate = true;
        private void cancelButton_Click(object sender, EventArgs e) => _cancellationTokenSource.Cancel();
    }
}
