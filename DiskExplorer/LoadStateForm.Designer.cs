namespace DiskExplorer
{
    partial class LoadStateForm
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
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.stateFileLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 12);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(260, 23);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar1.TabIndex = 0;
            this.progressBar1.Value = 100;
            // 
            // stateFileLabel
            // 
            this.stateFileLabel.AutoSize = true;
            this.stateFileLabel.Location = new System.Drawing.Point(45, 38);
            this.stateFileLabel.Name = "stateFileLabel";
            this.stateFileLabel.Size = new System.Drawing.Size(183, 13);
            this.stateFileLabel.TabIndex = 1;
            this.stateFileLabel.Text = "Loading last scan from ./analysis.json";
            // 
            // LoadStateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 59);
            this.Controls.Add(this.stateFileLabel);
            this.Controls.Add(this.progressBar1);
            this.Name = "LoadStateForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Loading...";
            this.Load += new System.EventHandler(this.LoadStateForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label stateFileLabel;
    }
}