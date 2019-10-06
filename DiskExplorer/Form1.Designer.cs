namespace DiskExplorer
{
    partial class Form1
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
            if (disposing && (components != null))
            {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			this.listView1 = new DiskExplorer.ListView2();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.toolStripDropDownButtonOld = new System.Windows.Forms.ToolStripDropDownButton();
			this.toolStripDropDownButtonNew = new System.Windows.Forms.ToolStripDropDownButton();
			this.toolStripDropDownButtonSeparator1 = new System.Windows.Forms.ToolStripDropDownButton();
			this.toolStripDropDownButtonSaveAnalisysAs = new System.Windows.Forms.ToolStripDropDownButton();
			this.toolStripDropDownButtonLoadAnalisys = new System.Windows.Forms.ToolStripDropDownButton();
			this.toolStripDropDownButtonSeparator2 = new System.Windows.Forms.ToolStripDropDownButton();
			this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.buttonDiscover = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPageExplorer = new System.Windows.Forms.TabPage();
			this.listViewExplorer = new System.Windows.Forms.ListView();
			this.tabPageAllFiles = new System.Windows.Forms.TabPage();
			this.buttonFindDuplicates = new System.Windows.Forms.Button();
			this.textBoxPattern = new System.Windows.Forms.TextBox();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.toolStripMenuItemOpen = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemShowInFolder = new System.Windows.Forms.ToolStripMenuItem();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.statusStrip1.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabPageExplorer.SuspendLayout();
			this.tabPageAllFiles.SuspendLayout();
			this.contextMenuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// listView1
			// 
			this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.listView1.FullRowSelect = true;
			this.listView1.GridLines = true;
			this.listView1.HideSelection = false;
			this.listView1.Location = new System.Drawing.Point(0, 2);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(669, 381);
			this.listView1.TabIndex = 1;
			this.listView1.UseCompatibleStateImageBehavior = false;
			this.listView1.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView1_ColumnClick);
			this.listView1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseClick);
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButtonOld,
            this.toolStripDropDownButtonNew,
            this.toolStripDropDownButtonSeparator1,
            this.toolStripDropDownButtonSaveAnalisysAs,
            this.toolStripDropDownButtonLoadAnalisys,
            this.toolStripDropDownButtonSeparator2,
            this.toolStripStatusLabel1});
			this.statusStrip1.Location = new System.Drawing.Point(0, 539);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(720, 22);
			this.statusStrip1.TabIndex = 3;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// toolStripDropDownButtonOld
			// 
			this.toolStripDropDownButtonOld.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.toolStripDropDownButtonOld.AutoToolTip = false;
			this.toolStripDropDownButtonOld.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripDropDownButtonOld.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButtonOld.Image")));
			this.toolStripDropDownButtonOld.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.toolStripDropDownButtonOld.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.toolStripDropDownButtonOld.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripDropDownButtonOld.Name = "toolStripDropDownButtonOld";
			this.toolStripDropDownButtonOld.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
			this.toolStripDropDownButtonOld.ShowDropDownArrow = false;
			this.toolStripDropDownButtonOld.Size = new System.Drawing.Size(30, 20);
			this.toolStripDropDownButtonOld.Text = "Old";
			this.toolStripDropDownButtonOld.Click += new System.EventHandler(this.toolStripDropDownButtonOld_Click);
			// 
			// toolStripDropDownButtonNew
			// 
			this.toolStripDropDownButtonNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripDropDownButtonNew.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButtonNew.Image")));
			this.toolStripDropDownButtonNew.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripDropDownButtonNew.Name = "toolStripDropDownButtonNew";
			this.toolStripDropDownButtonNew.ShowDropDownArrow = false;
			this.toolStripDropDownButtonNew.Size = new System.Drawing.Size(35, 20);
			this.toolStripDropDownButtonNew.Text = "New";
			this.toolStripDropDownButtonNew.Click += new System.EventHandler(this.toolStripDropDownButtonNew_Click);
			// 
			// toolStripDropDownButtonSeparator1
			// 
			this.toolStripDropDownButtonSeparator1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripDropDownButtonSeparator1.Enabled = false;
			this.toolStripDropDownButtonSeparator1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButtonSeparator1.Image")));
			this.toolStripDropDownButtonSeparator1.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripDropDownButtonSeparator1.Name = "toolStripDropDownButtonSeparator1";
			this.toolStripDropDownButtonSeparator1.ShowDropDownArrow = false;
			this.toolStripDropDownButtonSeparator1.Size = new System.Drawing.Size(14, 20);
			this.toolStripDropDownButtonSeparator1.Text = "|";
			// 
			// toolStripDropDownButtonSaveAnalisysAs
			// 
			this.toolStripDropDownButtonSaveAnalisysAs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripDropDownButtonSaveAnalisysAs.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButtonSaveAnalisysAs.Image")));
			this.toolStripDropDownButtonSaveAnalisysAs.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripDropDownButtonSaveAnalisysAs.Name = "toolStripDropDownButtonSaveAnalisysAs";
			this.toolStripDropDownButtonSaveAnalisysAs.ShowDropDownArrow = false;
			this.toolStripDropDownButtonSaveAnalisysAs.Size = new System.Drawing.Size(102, 20);
			this.toolStripDropDownButtonSaveAnalisysAs.Text = "Save analisys as...";
			this.toolStripDropDownButtonSaveAnalisysAs.Click += new System.EventHandler(this.toolStripDropDownButtonSaveAnalisysAs_Click);
			// 
			// toolStripDropDownButtonLoadAnalisys
			// 
			this.toolStripDropDownButtonLoadAnalisys.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripDropDownButtonLoadAnalisys.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButtonLoadAnalisys.Image")));
			this.toolStripDropDownButtonLoadAnalisys.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripDropDownButtonLoadAnalisys.Name = "toolStripDropDownButtonLoadAnalisys";
			this.toolStripDropDownButtonLoadAnalisys.ShowDropDownArrow = false;
			this.toolStripDropDownButtonLoadAnalisys.Size = new System.Drawing.Size(90, 20);
			this.toolStripDropDownButtonLoadAnalisys.Text = "Load analysis...";
			this.toolStripDropDownButtonLoadAnalisys.Click += new System.EventHandler(this.toolStripDropDownButtonLoadAnalisys_Click);
			// 
			// toolStripDropDownButtonSeparator2
			// 
			this.toolStripDropDownButtonSeparator2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripDropDownButtonSeparator2.Enabled = false;
			this.toolStripDropDownButtonSeparator2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButtonSeparator2.Image")));
			this.toolStripDropDownButtonSeparator2.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripDropDownButtonSeparator2.Name = "toolStripDropDownButtonSeparator2";
			this.toolStripDropDownButtonSeparator2.ShowDropDownArrow = false;
			this.toolStripDropDownButtonSeparator2.Size = new System.Drawing.Size(14, 20);
			this.toolStripDropDownButtonSeparator2.Text = "|";
			// 
			// toolStripStatusLabel1
			// 
			this.toolStripStatusLabel1.Enabled = false;
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			this.toolStripStatusLabel1.Size = new System.Drawing.Size(39, 17);
			this.toolStripStatusLabel1.Text = "Ready";
			// 
			// textBox1
			// 
			this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBox1.Location = new System.Drawing.Point(10, 16);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(333, 20);
			this.textBox1.TabIndex = 4;
			this.textBox1.Text = "C:\\";
			this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
			this.textBox1.DoubleClick += new System.EventHandler(this.textBox1_DoubleClick);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.textBoxPattern);
			this.groupBox1.Controls.Add(this.textBox1);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Location = new System.Drawing.Point(145, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(415, 54);
			this.groupBox1.TabIndex = 6;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Select folder to discover && scan";
			// 
			// buttonDiscover
			// 
			this.buttonDiscover.Location = new System.Drawing.Point(144, 72);
			this.buttonDiscover.Name = "buttonDiscover";
			this.buttonDiscover.Size = new System.Drawing.Size(222, 24);
			this.buttonDiscover.TabIndex = 7;
			this.buttonDiscover.Text = "Discover files && calculate hashes";
			this.buttonDiscover.UseVisualStyleBackColor = true;
			this.buttonDiscover.Click += new System.EventHandler(this.buttonDiscover_Click);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.tabControl1);
			this.groupBox2.Location = new System.Drawing.Point(12, 102);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(689, 434);
			this.groupBox2.TabIndex = 9;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Analyze";
			// 
			// tabControl1
			// 
			this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl1.Controls.Add(this.tabPageExplorer);
			this.tabControl1.Controls.Add(this.tabPageAllFiles);
			this.tabControl1.Location = new System.Drawing.Point(6, 19);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(677, 409);
			this.tabControl1.TabIndex = 15;
			// 
			// tabPageExplorer
			// 
			this.tabPageExplorer.Controls.Add(this.listViewExplorer);
			this.tabPageExplorer.Location = new System.Drawing.Point(4, 22);
			this.tabPageExplorer.Name = "tabPageExplorer";
			this.tabPageExplorer.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageExplorer.Size = new System.Drawing.Size(669, 383);
			this.tabPageExplorer.TabIndex = 0;
			this.tabPageExplorer.Text = "Explorer";
			this.tabPageExplorer.UseVisualStyleBackColor = true;
			// 
			// listViewExplorer
			// 
			this.listViewExplorer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.listViewExplorer.HideSelection = false;
			this.listViewExplorer.Location = new System.Drawing.Point(0, 2);
			this.listViewExplorer.Name = "listViewExplorer";
			this.listViewExplorer.Size = new System.Drawing.Size(667, 380);
			this.listViewExplorer.TabIndex = 0;
			this.listViewExplorer.UseCompatibleStateImageBehavior = false;
			this.listViewExplorer.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewExplorer_MouseDoubleClick);
			// 
			// tabPageAllFiles
			// 
			this.tabPageAllFiles.Controls.Add(this.listView1);
			this.tabPageAllFiles.Location = new System.Drawing.Point(4, 22);
			this.tabPageAllFiles.Name = "tabPageAllFiles";
			this.tabPageAllFiles.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageAllFiles.Size = new System.Drawing.Size(669, 383);
			this.tabPageAllFiles.TabIndex = 1;
			this.tabPageAllFiles.Text = "All files";
			this.tabPageAllFiles.UseVisualStyleBackColor = true;
			// 
			// buttonFindDuplicates
			// 
			this.buttonFindDuplicates.Location = new System.Drawing.Point(373, 72);
			this.buttonFindDuplicates.Name = "buttonFindDuplicates";
			this.buttonFindDuplicates.Size = new System.Drawing.Size(109, 24);
			this.buttonFindDuplicates.TabIndex = 13;
			this.buttonFindDuplicates.Text = "Show duplicates";
			this.buttonFindDuplicates.UseVisualStyleBackColor = true;
			this.buttonFindDuplicates.Click += new System.EventHandler(this.buttonFindDuplicates_Click);
			// 
			// textBoxPattern
			// 
			this.textBoxPattern.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxPattern.Location = new System.Drawing.Point(349, 16);
			this.textBoxPattern.Name = "textBoxPattern";
			this.textBoxPattern.Size = new System.Drawing.Size(49, 20);
			this.textBoxPattern.TabIndex = 6;
			this.textBoxPattern.Text = "*";
			this.textBoxPattern.TextChanged += new System.EventHandler(this.textBoxPattern_TextChanged);
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemOpen,
            this.toolStripMenuItemShowInFolder});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(178, 48);
			// 
			// toolStripMenuItemOpen
			// 
			this.toolStripMenuItemOpen.Name = "toolStripMenuItemOpen";
			this.toolStripMenuItemOpen.Size = new System.Drawing.Size(177, 22);
			this.toolStripMenuItemOpen.Text = "Открыть";
			this.toolStripMenuItemOpen.Click += new System.EventHandler(this.toolStripMenuItemOpen_Click);
			// 
			// toolStripMenuItemShowInFolder
			// 
			this.toolStripMenuItemShowInFolder.Name = "toolStripMenuItemShowInFolder";
			this.toolStripMenuItemShowInFolder.Size = new System.Drawing.Size(177, 22);
			this.toolStripMenuItemShowInFolder.Text = "Показать в папке...";
			this.toolStripMenuItemShowInFolder.Click += new System.EventHandler(this.toolStripMenuItemShowInFolder_Click);
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(349, 35);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(49, 13);
			this.label2.TabIndex = 7;
			this.label2.Text = "Pattern";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(10, 35);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(139, 13);
			this.label3.TabIndex = 8;
			this.label3.Text = "Double click to select";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(720, 561);
			this.Controls.Add(this.buttonFindDuplicates);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.buttonDiscover);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.statusStrip1);
			this.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Name = "Form1";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.tabControl1.ResumeLayout(false);
			this.tabPageExplorer.ResumeLayout(false);
			this.tabPageAllFiles.ResumeLayout(false);
			this.contextMenuStrip1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private ListView2 listView1;
        private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button buttonDiscover;
		private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonFindDuplicates;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.TextBox textBoxPattern;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButtonOld;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButtonNew;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButtonSaveAnalisysAs;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButtonLoadAnalisys;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButtonSeparator1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButtonSeparator2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemOpen;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemShowInFolder;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageExplorer;
        private System.Windows.Forms.TabPage tabPageAllFiles;
        private System.Windows.Forms.ListView listViewExplorer;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
	}
}

