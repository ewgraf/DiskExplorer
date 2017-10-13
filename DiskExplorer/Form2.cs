using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Microsoft.VisualBasic.FileIO;
using DiskExplorer.Entities;

namespace DiskExplorer
{
    public partial class Form2 : Form
    {
        private Dictionary<string, List<FileInfoExtended>> duplicates;
        private string folder;
        private string fontName = string.Empty; // Шрифт
        private float  fontSize = 0f;
        private Bitmap preview = null;

        public string[] FilesRemoved { get; private set; }

        public Form2(Dictionary<string, List<FileInfoExtended>> d, string f)
        {
            InitializeComponent();

            duplicates = d;
            folder = f;
            var columns = new [] { 
                new ColumnHeader(0){ Text = "Name",             Width = 120 },
                new ColumnHeader(0){ Text = "Directory",        Width = 50  },
                new ColumnHeader(0){ Text = "Size",             Width = 70  },
                new ColumnHeader(0){ Text = "Last write time",  Width = 150 },
                new ColumnHeader(0){ Text = "Hash",             Width = 460 }
            };
            listView1.Columns[1].Width = listView1.Width - listView1.Columns[0].Width - listView1.Columns[2].Width - listView1.Columns[3].Width - 21;
            listView1.Columns.Clear();
            listView1.Columns.AddRange(columns);
            listView1.FullRowSelect = true;
            fontName = "Consolas";
            fontSize = 9.5f;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            label1.Text = folder;

            bool coloriseGroup = true;

            foreach (var duplicate in duplicates)
            {
                ListViewItem[] items = duplicate.Value.Select(f => {
                    var item = new ListViewItem(
                        new string[] {
                            f.Name,
                            f.DirectoryName,
                            f.SizeWithPrefix(),
                            f.LastWriteTime.ToString(),
                            f.Hash,
                        });
                        if (coloriseGroup) {
                            item.BackColor = SystemColors.Control;
                        }
                        return item;
                    })
                    .ToArray();
                coloriseGroup = !coloriseGroup;                
                listView1.Items.AddRange(items);                
            }

            listView1.Focus();
            if (listView1.Items.Count != 0) {
                listView1.Items[0].Selected = true;
            }            
        }

        private void listView1_Resize(object sender, EventArgs e)
        {
            listView1.Columns[1].Width = listView1.Width - listView1.Columns[0].Width - listView1.Columns[2].Width - listView1.Columns[3].Width - listView1.Columns[4].Width - 21;
        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right) {
                if (listView1.FocusedItem.Bounds.Contains(e.Location) == true) {
                    contextMenuStrip1.Show(Cursor.Position);
                }
            }
        }

        private void pathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Common.OpenFolder(listView1.SelectedItems[0].SubItems[1].Text);
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(listView1.SelectedItems[0].SubItems[1].Text);
        }

        private void listView1_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (e.Item.Checked)
            {
                e.Item.Font = new Font(fontName, fontSize, FontStyle.Bold);
            } else {
                e.Item.Font = new Font(fontName, fontSize);
            }
                            
        }

        private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            string folder = e.Item.SubItems[1].Text;
            string filename = e.Item.SubItems[0].Text;
            if (
                filename.EndsWith(".jpg")  ||
                filename.EndsWith(".jpeg") ||
                filename.EndsWith(".png")  ||
                filename.EndsWith(".gif")  ||
                filename.EndsWith(".bmp")  ||
                filename.EndsWith(".gif")) {
                preview = new Bitmap(Path.Combine(folder, filename));
            } else {
                preview = null;
            }                
            pictureBox1.Image = preview;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var files = new List<string>();
            foreach (ListViewItem item in listView1.Items) {
                if (item.Checked) {
                    files.Add(Path.Combine(item.SubItems[1].Text, item.SubItems[0].Text));
                }                    
            }
            preview?.Dispose();
            pictureBox1.Image = null;
            if (MessageBox.Show(String.Format("Вы хотите удалить {0} файлов?", files.Count), "Подтвержнение", MessageBoxButtons.YesNo) == DialogResult.Yes) {
                files.Where(f => File.Exists(f))
                    .ToList()
                    .ForEach(file => {
                        if ((File.GetAttributes(file) & FileAttributes.Directory) == FileAttributes.Directory) {
                            throw new Exception("path to remove is folder. it is anacceptable");
                        }
                        FileSystem.DeleteFile(file, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
                        FilesRemoved = files.ToArray();
                        this.Close();
                    });
            }
        }
    }
}
