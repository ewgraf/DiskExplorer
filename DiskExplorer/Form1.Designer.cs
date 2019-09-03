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
			this.button1 = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.button2 = new System.Windows.Forms.Button();
			this.buttonDiscover = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPageExplorer = new System.Windows.Forms.TabPage();
			this.listViewExplorer = new System.Windows.Forms.ListView();
			this.tabPageAllFiles = new System.Windows.Forms.TabPage();
			this.label1 = new System.Windows.Forms.Label();
			this.buttonFindDuplicates = new System.Windows.Forms.Button();
			this.textBoxPattern = new System.Windows.Forms.TextBox();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.toolStripMenuItemOpen = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemShowInFolder = new System.Windows.Forms.ToolStripMenuItem();
			this.statusStrip1.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabPageExplorer.SuspendLayout();
			this.tabPageAllFiles.SuspendLayout();
			this.groupBox3.SuspendLayout();
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
			this.listView1.Location = new System.Drawing.Point(0, 2);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(838, 336);
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
			this.statusStrip1.Size = new System.Drawing.Size(884, 22);
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
			this.textBox1.Location = new System.Drawing.Point(6, 18);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(651, 20);
			this.textBox1.TabIndex = 4;
			this.textBox1.Text = "C:\\";
			this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button1.Location = new System.Drawing.Point(663, 16);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(113, 24);
			this.button1.TabIndex = 5;
			this.button1.Text = "Select folder...";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.button2);
			this.groupBox1.Controls.Add(this.textBox1);
			this.groupBox1.Controls.Add(this.button1);
			this.groupBox1.Location = new System.Drawing.Point(12, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(860, 44);
			this.groupBox1.TabIndex = 6;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Select path";
			// 
			// button2
			// 
			this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button2.Location = new System.Drawing.Point(782, 16);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(72, 24);
			this.button2.TabIndex = 0;
			this.button2.Text = "Explore";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// buttonDiscover
			// 
			this.buttonDiscover.Location = new System.Drawing.Point(6, 19);
			this.buttonDiscover.Name = "buttonDiscover";
			this.buttonDiscover.Size = new System.Drawing.Size(222, 24);
			this.buttonDiscover.TabIndex = 7;
			this.buttonDiscover.Text = "Discover files && calculate hashes";
			this.buttonDiscover.UseVisualStyleBackColor = true;
			this.buttonDiscover.Click += new System.EventHandler(this.buttonDiscover_Click);
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox2.Controls.Add(this.tabControl1);
			this.groupBox2.Controls.Add(this.label1);
			this.groupBox2.Controls.Add(this.buttonFindDuplicates);
			this.groupBox2.Controls.Add(this.buttonDiscover);
			this.groupBox2.Location = new System.Drawing.Point(12, 116);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(860, 420);
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
			this.tabControl1.Location = new System.Drawing.Point(6, 49);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(848, 365);
			this.tabControl1.TabIndex = 15;
			// 
			// tabPageExplorer
			// 
			this.tabPageExplorer.Controls.Add(this.listViewExplorer);
			this.tabPageExplorer.Location = new System.Drawing.Point(4, 22);
			this.tabPageExplorer.Name = "tabPageExplorer";
			this.tabPageExplorer.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageExplorer.Size = new System.Drawing.Size(840, 339);
			this.tabPageExplorer.TabIndex = 0;
			this.tabPageExplorer.Text = "Explorer";
			this.tabPageExplorer.UseVisualStyleBackColor = true;
			// 
			// listViewExplorer
			// 
			this.listViewExplorer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.listViewExplorer.Location = new System.Drawing.Point(0, 2);
			this.listViewExplorer.Name = "listViewExplorer";
			this.listViewExplorer.Size = new System.Drawing.Size(838, 336);
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
			this.tabPageAllFiles.Size = new System.Drawing.Size(840, 339);
			this.tabPageAllFiles.TabIndex = 1;
			this.tabPageAllFiles.Text = "All files";
			this.tabPageAllFiles.UseVisualStyleBackColor = true;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(234, 25);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(13, 13);
			this.label1.TabIndex = 14;
			this.label1.Text = "|";
			// 
			// buttonFindDuplicates
			// 
			this.buttonFindDuplicates.Location = new System.Drawing.Point(253, 19);
			this.buttonFindDuplicates.Name = "buttonFindDuplicates";
			this.buttonFindDuplicates.Size = new System.Drawing.Size(109, 24);
			this.buttonFindDuplicates.TabIndex = 13;
			this.buttonFindDuplicates.Text = "Show duplicates";
			this.buttonFindDuplicates.UseVisualStyleBackColor = true;
			this.buttonFindDuplicates.Click += new System.EventHandler(this.buttonFindDuplicates_Click);
			// 
			// textBoxPattern
			// 
			this.textBoxPattern.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxPattern.Location = new System.Drawing.Point(6, 19);
			this.textBoxPattern.Name = "textBoxPattern";
			this.textBoxPattern.Size = new System.Drawing.Size(848, 20);
			this.textBoxPattern.TabIndex = 6;
			this.textBoxPattern.Text = "*";
			this.textBoxPattern.TextChanged += new System.EventHandler(this.textBoxPattern_TextChanged);
			// 
			// groupBox3
			// 
			this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox3.Controls.Add(this.textBoxPattern);
			this.groupBox3.Location = new System.Drawing.Point(12, 62);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(860, 48);
			this.groupBox3.TabIndex = 10;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "With pattern";
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
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(884, 561);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox2);
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
			this.groupBox2.PerformLayout();
			this.tabControl1.ResumeLayout(false);
			this.tabPageExplorer.ResumeLayout(false);
			this.tabPageAllFiles.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.contextMenuStrip1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private ListView2 listView1;
        private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button buttonDiscover;
		private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonFindDuplicates;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.TextBox textBoxPattern;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label1;
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
    }
}

