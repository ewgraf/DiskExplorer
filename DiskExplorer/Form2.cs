using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace DiskExplorer
{
    public partial class Form2 : Form
    {
        private Dictionary<string, string> duplicates;
        private string folder;

        // Шрифт
        private string fontName = string.Empty;
        private float  fontSize = 0f;

        private Bitmap preview = null;

        public Form2(Dictionary<string, string> d, string f)
        {
            InitializeComponent();
            duplicates = d;
            folder = f;
            ColumnHeader[] columns = new ColumnHeader[4] { 
                new ColumnHeader(0){ Text = "Имя",      Width = 120  },
                new ColumnHeader(0){ Text = "Путь",     Width = 0   },
                new ColumnHeader(0){ Text = "Размер",   Width = 70  },
                new ColumnHeader(0){ Text = "Изменен",  Width = 150  }
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
            //listView1.Sorting = SortOrder.Ascending;

            string prevHash = string.Empty;
            string currHash = string.Empty;
            FileInfo info = null;

            bool coloriseGroup = true;

            foreach (var item in duplicates)
            {
                currHash = item.Value;
                info = new FileInfo(item.Key);
                ListViewItem item1 = new ListViewItem(new string[] { info.Name, item.Key/*.Replace(folder, "")*/, String.Format("{0:0.##} Mb", (info.Length / (1024d * 1024d))), info.LastWriteTime.ToString() });

                if (currHash != prevHash)
                    coloriseGroup = !coloriseGroup;
                
                if (item.Value == duplicates.First().Value)
                    coloriseGroup = true;

                if (coloriseGroup)
                    item1.BackColor = SystemColors.Control;

                    //item1.Checked = true;
                //else
                //    coloriseGroup = !coloriseGroup;
                    //item1.Checked = false;
                prevHash = currHash;
                    

                listView1.Items.Add(item1);
            }

            this.listView1.Focus();
            this.listView1.Items[0].Selected = true;
        }

        private void listView1_Resize(object sender, EventArgs e)
        {
            listView1.Columns[1].Width = listView1.Width - listView1.Columns[0].Width - listView1.Columns[2].Width - listView1.Columns[3].Width - 21;
        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (listView1.FocusedItem.Bounds.Contains(e.Location) == true)
                {
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
            if(e.Item.Checked)
                e.Item.Font = new System.Drawing.Font(fontName, fontSize, System.Drawing.FontStyle.Bold);
            else
                e.Item.Font = new System.Drawing.Font(fontName, fontSize);
        }

        private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            string path = e.Item.SubItems[1].Text;
            if (
                path.EndsWith(".jpg") ||
                path.EndsWith(".jpeg") || 
                path.EndsWith(".png") || 
                path.EndsWith(".gif") || 
                path.EndsWith(".bmp") ||
                path.EndsWith(".gif"))
                preview = new Bitmap(e.Item.SubItems[1].Text);
            else
                preview = null;
            pictureBox1.Image = preview;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<string> files = new List<string>();
            foreach (ListViewItem item in listView1.Items)
            {
                if (item.Checked)
                    files.Add(item.SubItems[1].Text);
            }
            preview.Dispose();
            pictureBox1.Image = null;
            if (MessageBox.Show(String.Format("Вы хотите удалить {0} файлов?", files.Count), "Подтвержнение", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                foreach (var item in files)
                {
                    File.Delete(item);
                }
            }
        }
    }
}
