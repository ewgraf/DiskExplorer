using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace DiskExplorer
{
	public partial class Form1 : Form
	{
		public class State
		{
			public string SelectedFolder = @"E:\Unsorted\С торрента";
			public FileInfoExtended[] FilesDiscovered;
			[JsonIgnore]
			public CancellationTokenSource CancellationTokenSource = new CancellationTokenSource();
		}

		public static State _state = new State();
        public static object _lock = new object();
        private ListViewColumnSorter sorter = new ListViewColumnSorter();

        public Form1()
		{
			InitializeComponent();
        }

		private void Form1_Load(object sender, EventArgs e)
		{
			listView1.GridLines = true;
			listView1.View = View.Details;

			listView1.Columns.Add("Name").Width = 300;
			listView1.Columns.Add("Length").Width = 100;
			listView1.Columns.Add("Directory name").Width = 100;
			listView1.Columns.Add("Full path").Width = 500;
			listView1.Columns.Add("Hash").Width = 400;

			listView1.ListViewItemSorter = sorter;

            LoadState();
            SetMainForm();
        }

		static long GetDirectorySize(string path)
		{
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

		public static string[] GetFilesOrDefault(string path, string searchPattern = "*", SearchOption searchOption = SearchOption.TopDirectoryOnly)
		{
			try {
				return Directory.GetFiles(path, searchPattern, searchOption);
			} catch (UnauthorizedAccessException ex) {
				return Array.Empty<string>();
			}
		}

		public static string[] GetDirectoriesOrDefault(string path, string searchPattern = "*", SearchOption searchOption = SearchOption.TopDirectoryOnly)
		{
			try {
				return Directory.GetDirectories(path, searchPattern, searchOption);
			} catch (UnauthorizedAccessException ex) {
				return Array.Empty<string>();
			}
		}

		private static IEnumerable<FileInfoExtended> GetFiles(string path, IProgress<(int, long)> progress = null, CancellationTokenSource token = null)
		{
            int filesDiscovered = 0;
            long filesSizeDiscovered = 0;
            List<string> directories = new[] { path }.ToList();
            for (int i = 0; i < directories.Count && (!token?.IsCancellationRequested ?? true); i++) {
				string directory = directories[i];
                foreach (FileInfoExtended fileInfo in GetFilesOrDefault(directory).Select(f => new FileInfoExtended(f))) {
                    if (fileInfo.FullPath.Length > 260) { // MAX_PATH = 260, Windows OS since 2000.XP,2003
                        throw new ArgumentException($"Path '{path}' is too long to be handle by your OS. Consined doing something to reduce length.");
                    }
                    filesDiscovered++;
                    filesSizeDiscovered += fileInfo.Length;
                    yield return fileInfo;
                }
				string[] dirDirectories = GetDirectoriesOrDefault(directory);
				directories.AddRange(dirDirectories);

                if (filesDiscovered % 1000 == 0) {
                    progress.Report((filesDiscovered, filesSizeDiscovered));
                }
            }
		}

		public static bool TryParseFileSize(string value, out long result)
		{
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

		private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
		{
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

		private void button1_Click(object sender, EventArgs e)
		{
			_state.SelectedFolder = OSHelper.SelectFolder();
			textBox1.Text = _state.SelectedFolder;
		}

		private void button2_Click(object sender, EventArgs e)
		{
			Process.Start(_state.SelectedFolder);
		}

		private Task SetStripStatus(int length, double seconds, long totalSize)
		{
			toolStripStatusLabel1.Text = $"{length} files discovered in {seconds:#.###} seconds {FileUtils.SizeSuffix(totalSize)} discovered";
			return Task.CompletedTask;
		}

		private void textBox1_TextChanged(object sender, EventArgs e)
		{
			_state.SelectedFolder = textBox1.Text;
		}

		private void toolStripSplitButtonCancel_ButtonClick(object sender, EventArgs e)
		{
			_state.CancellationTokenSource.Cancel();
			toolStripSplitButtonCancel.Enabled = false;
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			File.WriteAllText("analysis.json", JsonConvert.SerializeObject(_state));
		}

		private void buttonLoad_Click(object sender, EventArgs e)
		{
			_state = JsonConvert.DeserializeObject<State>(File.ReadAllText("analysis.json"));

            SetMainForm();
        }

        private async Task<FileInfoExtended[]> ComputeHashes(FileInfoExtended[] files, IProgress<int> progress = null)
        {
            int index = 0;
            return files
                .AsParallel()
                .Select(f => {
                    if (++index % 10 == 0) {
                        progress.Report(index);
                    }
                    return new FileInfoExtended(f, Hash.GetFileHash(f.FullPath));
                    //return new FileInfoExtended(f, Hash.SuperFastHashUnsafeFile(f.FullPath));
                })
                .ToArray();
        }

        private async void buttonHash_Click(object sender, EventArgs e)
		{

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            _state.FilesDiscovered = await Task.Run(() => 
                ComputeHashes(_state.FilesDiscovered,
                    new Progress<int>(index => {
                        toolStripStatusLabel1.Text = $"{index} / {_state.FilesDiscovered.Length} hashes";
                    })
                )
            );
            stopwatch.Stop();

            toolStripStatusLabel1.Text = $"{_state.FilesDiscovered.Length} files hashed in {stopwatch.Elapsed.TotalSeconds} seconds";

            SetListView();
            SaveState();
		}

        private void SaveState()
        {
            File.WriteAllText("analysis.json", JsonConvert.SerializeObject(_state));
        }

        private void LoadState()
        {
            if (File.Exists("analysis.json")) {
                _state = JsonConvert.DeserializeObject<State>(File.ReadAllText("analysis.json"));
            }            

            SetMainForm();
        }

        private void SetMainForm()
        {
            textBox1.Text = _state.SelectedFolder;
            SetListView();
        }

        private void SetListView()
        {
            listView1.Items.Clear();
            if (_state.FilesDiscovered != null) {
                listView1.Items.AddRange(_state.FilesDiscovered.Select(f => {
                    ListViewItem item = new ListViewItem(f.Name);
                    item.SubItems.Add(FileUtils.SizeSuffix(f.Length));
                    item.SubItems.Add(f.DirectoryName);
                    item.SubItems.Add(f.FullPath);
                    item.SubItems.Add(f.Hash);
                    return item;
                }).ToArray());
            }
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            //string scanResultsPath = Path.Combine(Directory.GetParent(_state.SelectedFolder).ToString(), Path.GetFileName(_state.SelectedFolder) + "-duplecates.txt");
            //// Path.GetFolderName(selectedFolder) on D:\\Новая папка returns D:\\ therefore use Path.GetFileName(selectedFolder)            
            //using (var file = new StreamWriter(scanResultsPath)) {
            //    file.WriteLine(_state.SelectedFolder + @"\*");
            //    foreach (var pair in duplicates) {
            //        foreach (var path in pair.Value) {
            //            file.WriteLine(pair.Key + "@" + path);
            //        }
            //    }
            //}
            //List<string> hashAndPathes = File.ReadAllLines(scanResultsPath).ToList();
            //string folder = hashAndPathes[0];
            //hashAndPathes.RemoveAt(0);
            //Dictionary<string, string> d = new Dictionary<string, string>();
            //foreach (var line in hashAndPathes) {
            //    string[] split = line.Split(new char[] { '@' }, 2);
            //    string hash = split[0];
            //    string path = split[1];
            //    d.Add(path, hash);
            //}
        }

        private async void buttonDiscover_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            (listView1.ListViewItemSorter as ListViewColumnSorter).SortColumn = 0;

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            double elapsedSeconds = 0;
            long totalFilesSize = 0;
            toolStripSplitButtonCancel.Enabled = true;
            _state.CancellationTokenSource = new CancellationTokenSource();

            _state.FilesDiscovered = await Task.Run(() => GetFiles(_state.SelectedFolder,
                    new Progress<(int, long)>(value => {
                        totalFilesSize = value.Item2;
                        lock (_lock) {
                            elapsedSeconds = stopwatch.Elapsed.TotalMilliseconds / 1000d;
                            if (elapsedSeconds == 0) {
                                elapsedSeconds = 1;
                            }
                            double rate = value.Item1 / elapsedSeconds;
                            toolStripStatusLabel1.Text = $"{_state.SelectedFolder} {value.Item1} files discovered... {rate} files/second {FileUtils.SizeSuffix(totalFilesSize)} discovered so far...";
                        }
                    }),
                    _state.CancellationTokenSource
                )
                .ToArray());

            stopwatch.Stop();
            elapsedSeconds = stopwatch.Elapsed.TotalMilliseconds / 1000d;

            listView1.Items.AddRange(_state.FilesDiscovered.Select(f => {
                var item = new ListViewItem(f.Name);
                item.SubItems.Add(FileUtils.SizeSuffix(f.Length));
                item.SubItems.Add(f.DirectoryName);
                item.SubItems.Add(f.FullPath);
                item.SubItems.Add(f.Hash);
                return item;
            }).ToArray());

            await SetStripStatus(_state.FilesDiscovered.Length, elapsedSeconds, _state.FilesDiscovered.Sum(f => f.Length));

            SaveState();
        }

        private async void buttonFindDuplicates_Click(object sender, EventArgs e)
        {
            Dictionary<string, List<FileInfoExtended>> duplicates = _state.FilesDiscovered
                .GroupBy(g => g.Hash)
                .ToDictionary(g => g.Key, g => g.ToList())
                .Where(p => p.Value.Count > 1)
                .ToDictionary(g => g.Key, g => g.Value);

            new Form2(duplicates, _state.SelectedFolder).Show();
        }

        private void toolStripSplitButtonOld_ButtonClick(object sender, EventArgs e)
        {
            Application.VisualStyleState = System.Windows.Forms.VisualStyles.VisualStyleState.NonClientAreaEnabled;
        }

        private void toolStripSplitButtonNew_ButtonClick(object sender, EventArgs e)
        {
            Application.VisualStyleState = System.Windows.Forms.VisualStyles.VisualStyleState.ClientAndNonClientAreasEnabled;
        }
    }
}
