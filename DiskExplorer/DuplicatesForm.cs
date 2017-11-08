﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Microsoft.VisualBasic.FileIO;
using DiskExplorer.Entities;
using System.Text.RegularExpressions;
using System.Threading;

namespace DiskExplorer
{
    public partial class DuplicatesForm : Form {
        private string _folder;
        private string _fontName = "Consolas";
        private float  _fontSize = 9.5f;
        private Bitmap preview = null;
        private FileInfoExtended[] _files;

        public string[] FilesDeletedHashes { get; private set; }

        public DuplicatesForm(FileInfoExtended[] files) {
            InitializeComponent();

            _files = files;
        }

        private void Form2_Load(object sender, EventArgs e) {
            Dictionary<string, List<FileInfoExtended>> duplicates = _files.GroupBy(g => g.Hash)
                .ToDictionary(g => g.Key, g => g.ToList())
                .Where(p => p.Value.Count > 1)
                .ToDictionary(g => g.Key, g => g.Value);

            UpdateListView(duplicates);            
        }

        private void UpdateListView(Dictionary<string, List<FileInfoExtended>> duplicates) {
            listView1.BeginUpdate();
            listView1.Clear();
            listView1.Columns.Clear();
            listView1.Columns.AddRange(Form1.Columns.Select(c => new ColumnHeader { Name = c, Text = c }).ToArray());
            listView1.FullRowSelect = true;
            bool coloriseGroup = true;
            foreach (var duplicate in duplicates) {
                ListViewItem[] items = duplicate.Value.Select(f => {
                    var item = new ListViewItem(new string[] {
                            f.DirectoryName,
                            f.Name,
                            f.SizeWithPrefix(),
                            f.Hash
                        });
                    if (f.Name.EndsWith("asm")) {
                        ;
                    }
                    if (coloriseGroup) {
                        item.BackColor = SystemColors.Control;
                    }
                    return item;
                })
                    .ToArray();
                coloriseGroup = !coloriseGroup;
                listView1.Items.AddRange(items);
            }
            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            listView1.Focus();
            if (listView1.Items.Count != 0) {
                listView1.Items[0].Selected = true;
            }
            listView1.EndUpdate();
            toolStripStatusLabel1.Text = $"{duplicates.Count} groups and {duplicates.Sum(d => d.Value.Count)} files loaded";
        }

        private void listView1_MouseClick(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Right) {
                if (listView1.FocusedItem.Bounds.Contains(e.Location) == true) {
                    contextMenuStrip1.Show(Cursor.Position);
                }
            }
        }

        private void pathToolStripMenuItem_Click(object sender, EventArgs e) {
            
        }

        private void listView1_ItemChecked(object sender, ItemCheckedEventArgs e) {
            if (e.Item.Checked) {
                e.Item.Font = new Font(_fontName, _fontSize, FontStyle.Bold);
            } else {
                e.Item.Font = new Font(_fontName, _fontSize);
            }                            
        }

        private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e) {
            string folder = e.Item.SubItems[0].Text;
            string filename = e.Item.SubItems[1].Text;
            if (
                filename.EndsWith(".jpg")  ||
                filename.EndsWith(".jpeg") ||
                filename.EndsWith(".png")  ||
                filename.EndsWith(".gif")  ||
                filename.EndsWith(".bmp")  ||
                filename.EndsWith(".gif")) {
                try {
                    preview?.Dispose();
                    preview = null;
                    preview = new Bitmap(Path.Combine(folder, filename));
                } catch {
                    preview = null;
                } finally {
                    pictureBox1.BringToFront();
                    pictureBox1.Image = preview;
                }
            } else if(filename.EndsWith(".txt")) {
                try {
                    richTextBox1.BringToFront();
                    richTextBox1.Text = File.ReadLines(Path.Combine(folder, filename)).Take(42).Aggregate((text, nextLine) => text += $"{nextLine}{Environment.NewLine}");
                } catch { }
            }
        }

        private void deleteButton_Click(object sender, EventArgs e) {
            FileInfoExtended[] filesToDelete = listView1.CheckedItems
                .ToArray() // extension to allow .Select() on CheckedItems
                .Select(i => new FileInfoExtended {
                    DirectoryName = i.SubItems[0].Text,
                    Name = i.SubItems[1].Text,
                    Hash = i.SubItems[3].Text
                })
                .ToArray();
            preview?.Dispose();
            preview = null;
            pictureBox1.Image = null;

            if (MessageBox.Show($"Вы хотите удалить {filesToDelete.Length} файлов?", "Подтвержнение", MessageBoxButtons.YesNo) == DialogResult.Yes) {
                FilesDeletedHashes = filesToDelete
                    .Where(f => File.Exists(f.FullPath))
                    .Select(f => {
                        if ((File.GetAttributes(f.FullPath) & FileAttributes.Directory) == FileAttributes.Directory) {
                            throw new Exception("path to remove is folder. it is anacceptable");
                        }
                        FileSystem.DeleteFile(f.FullPath, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
                        return f.Hash;
                    })
                    .ToArray();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e) {
            Regex regex = null;
            try {
                regex = new Regex(textBoxFilterRegex.Text);
            } catch (ArgumentException ex) {
                MessageBox.Show(ex.ToString(), "Regexp partins error");
                return;
            }

            var filtered = _files
                .Where(f => regex.Match(f.Name).Success)
                .ToArray();

            Dictionary<string, List<FileInfoExtended>> duplicates = filtered
                .GroupBy(g => g.Hash)
                .ToDictionary(g => g.Key, g => g.ToList())
                .Where(p => p.Value.Count > 1)
                .ToDictionary(g => g.Key, g => g.Value);

            UpdateListView(duplicates);
        }

        private void button2_Click(object sender, EventArgs e) {
            Dictionary<string, List<FileInfoExtended>> duplicates = _files
                .GroupBy(g => g.Hash)
                .ToDictionary(g => g.Key, g => g.ToList())
                .Where(p => p.Value.Count > 1)
                .ToDictionary(g => g.Key, g => g.Value);

            var q = duplicates.Where(dd => dd.Value.Any(f => f.Name.EndsWith("asm"))).ToArray();

            UpdateListView(duplicates);
        }

        private void toolStripMenuItemOpen_Click(object s, EventArgs e) {
            foreach (ListViewItem selectedItem in listView1.SelectedItems) {
                string directory = selectedItem.SubItems[0].Text;
                string fileName = selectedItem.SubItems[1].Text;
                Process.Start(Path.Combine(directory, fileName));
            }
        }

        private void toolStripMenuItemShowInFolder_Click(object s, EventArgs e) {
            foreach (ListViewItem selectedItem in listView1.SelectedItems) {
                string directory = selectedItem.SubItems[0].Text;
                string fileName = selectedItem.SubItems[1].Text;
                Common.OpenFolderAndSelectFile(Path.Combine(directory, fileName));
            }
        }
    }
}