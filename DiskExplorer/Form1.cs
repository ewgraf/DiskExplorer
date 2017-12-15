using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Newtonsoft.Json;
using DiskExplorer.Entities;

namespace DiskExplorer
{
	public partial class Form1 : Form {
        public static string[] Columns = new[] { "Directory", "Name", "Date creation", "Date changed", "Size", "Hash" };
        private ListViewColumnSorter sorter = new ListViewColumnSorter();
        private ListViewColumnSorter explorerSorter = new ListViewColumnSorter();
        public static State _state = new State();
        public static object _lock = new object();

        public Form1() => InitializeComponent();

		private void Form1_Load(object sender, EventArgs e) {
			listView1.GridLines = true;
			listView1.View = View.Details;
            listView1.Columns.AddRange(Columns.Select(c => new ColumnHeader { Name = c, Text = c }).ToArray());
            listView1.Columns["Directory"].Width = 100;
            listView1.ListViewItemSorter = sorter;
            listView1.FullRowSelect = true;
            listViewExplorer.GridLines = true;
            listViewExplorer.View = View.Details;
            listViewExplorer.Columns.AddRange(new[] { "Name", "Size" }.Select(c => new ColumnHeader { Name = c, Text = c }).ToArray());
            //listViewExplorer.ListViewItemSorter = explorerSorter;
            listViewExplorer.FullRowSelect = true;
            statusStrip1.ShowItemToolTips = false;
            LoadScan();
        }

		static long GetDirectorySize(string path) {
			// 1.
			// Get array of all file names.
			string[] files = Directory.GetFiles(path, "*");
			string[] directories = Directory.GetDirectories(path);
			// 2.
			// Calculate total bytes of all files in a loop.
			long b = 0;
			foreach (string name in files) {
				// 3.
				// Use FileInfo to get length of each file.
				FileInfo info = new FileInfo(name);
				b += info.Length;
			}

			foreach (string dir in directories) {
				b += GetDirectorySize(dir);
			}
			// 4.
			// Return total size
			return b;
		}

		public static string[] GetFilesOrDefault(string path, string searchPattern = "*", SearchOption searchOption = SearchOption.TopDirectoryOnly) {
			try {
				return Directory.GetFiles(path, searchPattern, searchOption);
			} catch (UnauthorizedAccessException ex) {
				return Array.Empty<string>();
			}
		}

		public static string[] GetDirectoriesOrDefault(string path, string searchPattern = "*", SearchOption searchOption = SearchOption.TopDirectoryOnly) {
			try {
				return Directory.GetDirectories(path, searchPattern, searchOption);
			} catch (UnauthorizedAccessException ex) {
				return Array.Empty<string>();
			}
		}

		private static /*async */IEnumerable<FileInfoExtended> GetFiles(string path, IProgress<(int, long, string)> progress = null, /*PauseToken pauseToken = null, */CancellationTokenSource token = null) {
            int filesDiscovered = 0;
            long filesSizeDiscovered = 0;
            List<string> directories = new[] { path }.ToList();
            for (int i = 0; i < directories.Count && (!token?.IsCancellationRequested ?? true); i++) {
				string directory = directories[i];
                foreach (FileInfoExtended fileInfo in GetFilesOrDefault(directory).Select(f => new FileInfoExtended(f))) {
                    if (token?.IsCancellationRequested ?? true) {
                        break;
                    }
                    //if(pauseToken?.IsPaused ?? true) {
                    //    await pauseToken.WaitWhilePausedAsync();
                    //}
                    if (fileInfo.FullPath.Length > 260) { // MAX_PATH = 260, Windows OS since 2000.XP,2003
                        throw new ArgumentException($"Path '{path}' is too long to be handle by your OS. Consined doing something to reduce length.");
                    }
                    filesDiscovered++;
                    filesSizeDiscovered += fileInfo.Length;
                    
                    yield return fileInfo;
                    if(progress != null) {
                        if (DateTime.UtcNow.Millisecond >= 777) {
                            progress.Report((filesDiscovered, filesSizeDiscovered, fileInfo.FullPath));
                        }
                    }
                }
				string[] dirDirectories = GetDirectoriesOrDefault(directory);
				directories.AddRange(dirDirectories);
            }
		}

		public static bool TryParseFileSize(string value, out long result) {
			var values = value.Split(' ');
			if(values.Length != 2) {
				result = 0;
				return false;
			}
			if (FileUtils.SizeSuffixes.Contains(values[1])) {
				result = (long)(double.Parse(values[0]) * FileUtils.GetPrefixValue(values[1]));
				return true;
			} else if (values[1] == "Bytes") {
				result = 1;
				return true;
			} else {
				result = 0;
				return false;
			}
		}

		private void listView1_ColumnClick(object sender, ColumnClickEventArgs e) {
			//if (e.Column != 1)
			//	return;

			// Reverse the current sort direction for this column.
			if (sorter.Order == SortOrder.Ascending) {
				sorter.Order = SortOrder.Descending;
			} else {
				sorter.Order = SortOrder.Ascending;
			}
			
			// Set the column number that is to be sorted; default to ascending.
			sorter.SortColumn = e.Column;
			

			// Perform the sort with these new sort options.
			this.listView1.Sort();

			//Dictionary<string, long> newDic = new Dictionary<string, long>();

			//if (e.Column != sortColumn) {
			//	sortColumn = e.Column;

			//	foreach (var item in dict.OrderByDescending(i => i.Value)) {
			//		newDic.Add(item.Key, item.Value);
			//	}
			//} else {
			//	sortColumn = -1;
			//	foreach (var item in dict.OrderBy(i => i.Value)) {
			//		newDic.Add(item.Key, item.Value);
			//	}
			//}

			//dict = newDic;

			//listView1.Items.Clear();

			//foreach (KeyValuePair<string, long> kvp in dict) {
			//	ListViewItem name = new ListViewItem(Path.GetFileName(kvp.Key));
			//	name.SubItems.Add((kvp.Value / (1024 * 1024)) + " MB");
			//	listView1.Items.AddRange(new ListViewItem[] { name });
			//}
		}

		private void button1_Click(object sender, EventArgs e) {
			textBox1.Text = OSHelper.SelectFolder();
		}
        
        private void textBox1_TextChanged(object sender, EventArgs e) {
            _state.SelectedFolder = textBox1.Text;
        }

        private void button2_Click(object sender, EventArgs e) {
            Process.Start(_state.SelectedFolder);
        }

        private void LoadScan(string analisysPath = "analysis.json") {
            var loadStateForm = new LoadStateForm(analisysPath);
            var dialogResult = loadStateForm.ShowDialog();
            if (dialogResult == DialogResult.OK) {
                _state = loadStateForm.State;
                
                SetMainForm();
            }
        }

        private void SaveState() {
            File.WriteAllText("analysis.json", JsonConvert.SerializeObject(_state));
        }

        private void SaveStateAs() {
            string filepath = OSHelper.SaveFile("analysis.json", "JSON | *.json");
            File.WriteAllText(filepath, JsonConvert.SerializeObject(_state));
        }

        private void SetMainForm() {
            textBox1.Text = _state.SelectedFolder;
            textBoxPattern.Text = _state.Pattern;

            listView1.BeginUpdate();
            listView1.Items.Clear();
            if (_state.Folder != null) {
                var items = _state.Folder
                    .GetAllFiles()
                    .Select(f => {
                        var item = new ListViewItem(new string[] {
                                f.DirectoryName,
                                f.Name,
                                f.CreationTime.ToShortDateString(),
                                f.LastWriteTime.ToShortDateString(),
                                f.SizeWithPrefix(),
                                f.Hash
                        });
                        return item;
                    }).ToArray();
                listView1.Items.AddRange(items);
                (listView1.ListViewItemSorter as ListViewColumnSorter).SortColumn = 2; // "Size" column
                listView1.Sort();
                listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            }
            listView1.EndUpdate();

            SetExplorerTab();
        }

        private void SetExplorerTab() {
            listViewExplorer.BeginUpdate();
            listViewExplorer.Items.Clear();
            if (_state.Folder != null) {
                var folderItems = _state.CurrentlyExploringFolder.Subfolders
                    .OrderByDescending(f => f.Size)
                    .Select(f => new ListViewItem(new string[] { $"[{f.Path}]", FileUtils.SizeSuffix(f.Size) }))
                    .ToArray();
                var fileItems = _state.CurrentlyExploringFolder.Files
                    .OrderByDescending(f => f.Length)
                    .Select(f => new ListViewItem(new string[] { $" {f.FullPath}", f.SizeWithPrefix() }))
                    .ToArray();
                listViewExplorer.Items.Add(new ListViewItem(new[] { "[..]", string.Empty }));
                listViewExplorer.Items.AddRange(folderItems);
                listViewExplorer.Items.AddRange(fileItems);
                //(listViewExplorer.ListViewItemSorter as ListViewColumnSorter).SortColumn = 1; // "Size" column
                //listViewExplorer.Sort();
                listViewExplorer.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            }
            listViewExplorer.EndUpdate();
        }

        private void buttonDiscover_Click(object sender, EventArgs e) {
            var discoverForm = new DiscoverForm(_state.SelectedFolder, _state.Pattern);

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var dialogResult = discoverForm.ShowDialog();
            stopwatch.Stop();

            if(dialogResult == DialogResult.OK) {
                _state.Folder = discoverForm.Root;
                _state.CurrentlyExploringFolder = _state.Folder;
                SaveState();
                SetMainForm();
            }

            toolStripStatusLabel1.Text = $"Scan of {FileUtils.SizeSuffix(_state.Folder.FilesTotal)} complete in {stopwatch.Elapsed}";
        }

        private void buttonFindDuplicates_Click(object sender, EventArgs e) {
            var duplicatesForm = new DuplicatesForm(_state.Folder);
            if (duplicatesForm.ShowDialog() == DialogResult.OK && duplicatesForm.FilesDeletedHashes.Any()) {
                List<Folder> folders = new[] { _state.Folder }.ToList();
                for (int i = 0; i < folders.Count; i++) {
                    folders[i].Files = folders[i].Files.Where(f => !duplicatesForm.FilesDeletedHashes.Contains(f.Hash)).ToArray();
                    folders.AddRange(folders[i].Subfolders);
                }
                SaveState();
                SetMainForm();
            }
        }

        private void textBoxPattern_TextChanged(object sender, EventArgs e) {
            _state.Pattern = textBoxPattern.Text;
        }

        private void toolStripDropDownButtonOld_Click(object sender, EventArgs e)
        {

            Application.VisualStyleState = System.Windows.Forms.VisualStyles.VisualStyleState.NonClientAreaEnabled;
        }

        private void toolStripDropDownButtonNew_Click(object sender, EventArgs e)
        {

            Application.VisualStyleState = System.Windows.Forms.VisualStyles.VisualStyleState.ClientAndNonClientAreasEnabled;
        }

        private void toolStripDropDownButtonSaveAnalisysAs_Click(object sender, EventArgs e) {
            SaveStateAs();
        }

        private void toolStripDropDownButtonLoadAnalisys_Click(object sender, EventArgs e) {
            string filepath = OSHelper.OpenFile("analysis.json", "JSON | *.json");
            LoadScan(filepath);
        }

        private void listView1_MouseClick(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Right) {
                if (listView1.FocusedItem.Bounds.Contains(e.Location) == true) {
                    contextMenuStrip1.Show(Cursor.Position);
                }
            }
        }

        private void toolStripMenuItemOpen_Click(object sender, EventArgs e) {
            foreach (ListViewItem selectedItem in listView1.SelectedItems) {
                string directory = selectedItem.SubItems[0].Text;
                string fileName = selectedItem.SubItems[1].Text;
                Process.Start(Path.Combine(directory, fileName));
            }
        }

        private void toolStripMenuItemShowInFolder_Click(object sender, EventArgs e) {
            foreach (ListViewItem selectedItem in listView1.SelectedItems) {
                string directory = selectedItem.SubItems[0].Text;
                string fileName = selectedItem.SubItems[1].Text;
                Common.OpenFolderAndSelectFile(Path.Combine(directory, fileName));
            }
        }

        private void listViewExplorer_MouseDoubleClick(object sender, MouseEventArgs e) {
            string seceltion = listViewExplorer.SelectedItems[0].Text.Trim(new[] { '[', ' ', ']' });
            if (seceltion == "..") {
                string parentFolderPath = Directory.GetParent(_state.CurrentlyExploringFolder.Path).FullName;
                _state.CurrentlyExploringFolder = new[] { _state.Folder }
                    .Concat(_state.Folder.Subfolders.Flatten(f => f.Subfolders))
                    .Single(f => f.Path == parentFolderPath);
            } else {
                if (File.GetAttributes(seceltion).HasFlag(FileAttributes.Directory)) {
                    _state.CurrentlyExploringFolder = _state.CurrentlyExploringFolder.Subfolders.Single(f => f.Path == seceltion);
                } else { // file?
                    Process.Start(seceltion);
                    return;
                }                    
            }
            SetExplorerTab();
        }
    }
}
