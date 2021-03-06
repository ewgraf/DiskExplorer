﻿namespace DiskExplorer
{
    partial class DiscoverForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.components = new System.ComponentModel.Container();
			this.discoveringProgressBar = new System.Windows.Forms.ProgressBar();
			this.cancelButton = new System.Windows.Forms.Button();
			this.fileDiscoveringLabel = new System.Windows.Forms.Label();
			this.progressTimer = new System.Windows.Forms.Timer(this.components);
			this.filesDiscoveredLabel = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.scanGroupBox = new System.Windows.Forms.GroupBox();
			this.richTextBox1 = new System.Windows.Forms.RichTextBox();
			this.filesScannedLabel = new System.Windows.Forms.Label();
			this.fileScanningLabel = new System.Windows.Forms.Label();
			this.scanProgressBar = new System.Windows.Forms.ProgressBar();
			this.groupBox1.SuspendLayout();
			this.scanGroupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// discoveringProgressBar
			// 
			this.discoveringProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.discoveringProgressBar.Location = new System.Drawing.Point(9, 19);
			this.discoveringProgressBar.Name = "discoveringProgressBar";
			this.discoveringProgressBar.Size = new System.Drawing.Size(590, 23);
			this.discoveringProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
			this.discoveringProgressBar.TabIndex = 0;
			this.discoveringProgressBar.Value = 100;
			// 
			// cancelButton
			// 
			this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cancelButton.Location = new System.Drawing.Point(12, 309);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(602, 40);
			this.cancelButton.TabIndex = 1;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
			// 
			// fileDiscoveringLabel
			// 
			this.fileDiscoveringLabel.AutoSize = true;
			this.fileDiscoveringLabel.Location = new System.Drawing.Point(6, 45);
			this.fileDiscoveringLabel.Name = "fileDiscoveringLabel";
			this.fileDiscoveringLabel.Size = new System.Drawing.Size(102, 13);
			this.fileDiscoveringLabel.TabIndex = 2;
			this.fileDiscoveringLabel.Text = "fileDiscoveringLabel";
			// 
			// progressTimer
			// 
			this.progressTimer.Tick += new System.EventHandler(this.progressTimer_Tick);
			// 
			// filesDiscoveredLabel
			// 
			this.filesDiscoveredLabel.AutoSize = true;
			this.filesDiscoveredLabel.Location = new System.Drawing.Point(6, 60);
			this.filesDiscoveredLabel.Name = "filesDiscoveredLabel";
			this.filesDiscoveredLabel.Size = new System.Drawing.Size(89, 13);
			this.filesDiscoveredLabel.TabIndex = 5;
			this.filesDiscoveredLabel.Text = "Files discodeved:";
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.fileDiscoveringLabel);
			this.groupBox1.Controls.Add(this.discoveringProgressBar);
			this.groupBox1.Controls.Add(this.filesDiscoveredLabel);
			this.groupBox1.Location = new System.Drawing.Point(12, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(602, 85);
			this.groupBox1.TabIndex = 7;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Discovery";
			// 
			// scanGroupBox
			// 
			this.scanGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.scanGroupBox.Controls.Add(this.richTextBox1);
			this.scanGroupBox.Controls.Add(this.filesScannedLabel);
			this.scanGroupBox.Controls.Add(this.fileScanningLabel);
			this.scanGroupBox.Controls.Add(this.scanProgressBar);
			this.scanGroupBox.Enabled = false;
			this.scanGroupBox.Location = new System.Drawing.Point(12, 103);
			this.scanGroupBox.Name = "scanGroupBox";
			this.scanGroupBox.Size = new System.Drawing.Size(602, 200);
			this.scanGroupBox.TabIndex = 8;
			this.scanGroupBox.TabStop = false;
			this.scanGroupBox.Text = "Scan";
			// 
			// richTextBox1
			// 
			this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.richTextBox1.Location = new System.Drawing.Point(9, 76);
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.Size = new System.Drawing.Size(587, 118);
			this.richTextBox1.TabIndex = 9;
			this.richTextBox1.Text = "";
			this.richTextBox1.WordWrap = false;
			// 
			// filesScannedLabel
			// 
			this.filesScannedLabel.AutoSize = true;
			this.filesScannedLabel.Location = new System.Drawing.Point(6, 45);
			this.filesScannedLabel.Name = "filesScannedLabel";
			this.filesScannedLabel.Size = new System.Drawing.Size(75, 13);
			this.filesScannedLabel.TabIndex = 9;
			this.filesScannedLabel.Text = "Files scanned:";
			// 
			// fileScanningLabel
			// 
			this.fileScanningLabel.AutoSize = true;
			this.fileScanningLabel.Location = new System.Drawing.Point(6, 60);
			this.fileScanningLabel.Name = "fileScanningLabel";
			this.fileScanningLabel.Size = new System.Drawing.Size(91, 13);
			this.fileScanningLabel.TabIndex = 6;
			this.fileScanningLabel.Text = "fileScanningLabel";
			// 
			// scanProgressBar
			// 
			this.scanProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.scanProgressBar.Location = new System.Drawing.Point(9, 19);
			this.scanProgressBar.Name = "scanProgressBar";
			this.scanProgressBar.Size = new System.Drawing.Size(587, 23);
			this.scanProgressBar.TabIndex = 0;
			// 
			// DiscoverForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(624, 361);
			this.Controls.Add(this.scanGroupBox);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.cancelButton);
			this.Name = "DiscoverForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "DiscoverForm";
			this.Load += new System.EventHandler(this.DiscoverForm_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.scanGroupBox.ResumeLayout(false);
			this.scanGroupBox.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar discoveringProgressBar;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label fileDiscoveringLabel;
        private System.Windows.Forms.Timer progressTimer;
        private System.Windows.Forms.Label filesDiscoveredLabel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox scanGroupBox;
        private System.Windows.Forms.ProgressBar scanProgressBar;
        private System.Windows.Forms.Label filesScannedLabel;
        private System.Windows.Forms.Label fileScanningLabel;
        private System.Windows.Forms.RichTextBox richTextBox1;
    }
}