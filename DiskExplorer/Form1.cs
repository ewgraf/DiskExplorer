using System;
using System.Collections;
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
		public class FileInfoExtended
		{
			private FileInfo _fileInfo;
			public string Name { get; set; }
			public long Length { get; set; }
			public string DirectoryName { get; set; }
			public string FullPath { get; set; }
			public string Hash { get; set; }

			public FileInfoExtended() {} // for Json

			public FileInfoExtended(string fileName)
			{
				_fileInfo = new FileInfo(fileName);
				Name = _fileInfo.Name;
				Length = _fileInfo.Length;
				DirectoryName = _fileInfo.DirectoryName;
				FullPath = Path.Combine(DirectoryName, Name);
			}

			public FileInfoExtended(FileInfoExtended info, string hash)
			{
				Name = info.Name;
				Length = info.Length;
				DirectoryName = info.DirectoryName;
				FullPath = Path.Combine(DirectoryName, Name);
				Hash = hash;
			}
		}

		public class State
		{
			public string SelectedFolder = @"E:\3dprint";
			public string[] FilesDiscovered;
			public FileInfoExtended[] FilesScanned;
			[JsonIgnore]
			public CancellationTokenSource CancellationTokenSource = new CancellationTokenSource();
		}

		public static State _state = new State();

		public Form1()
		{
			InitializeComponent();

			textBox1.Text = _state.SelectedFolder;
		}

		private Dictionary<string, long> dict = new Dictionary<string, long>();
		ListViewColumnSorter sorter = new ListViewColumnSorter();

		private void Form1_Load(object sender, EventArgs e)
		{
			listView1.GridLines = true;
			listView1.View = View.Details;

			listView1.Columns.Add("Name").Width = 300;
			listView1.Columns.Add("Length").Width = 100;
			listView1.Columns.Add("Directory name").Width = 100;
			listView1.Columns.Add("Full path").Width = 500;
			listView1.Columns.Add("Hash").Width = 400;

			this.listView1.ListViewItemSorter = sorter;
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

		static string[] GetFiles(string path, IProgress<int> progress = null, CancellationTokenSource token = null)
		{
			List<string> files = GetFilesOrDefault(path).ToList();
			List<string> directories = GetDirectoriesOrDefault(path).ToList();
			for (int i = 0; i < directories.Count && (!token?.IsCancellationRequested ?? true); i++) {
				string directory = directories[i];
				string[] dirFiles = GetFilesOrDefault(directory);
				files.AddRange(dirFiles);
				string[] dirDirectories = GetDirectoriesOrDefault(directory);
				directories.AddRange(dirDirectories);
				if (files.Count % 1000 == 0) {
					progress.Report(files.Count);
				}
			}
			foreach (string filePath in files) {
				if (filePath.Length > 260) { // MAX_PATH = 260, Windows OS since 2000.XP,2003
					throw new ArgumentException($"Path '{path}' is too long to be handle by your OS. Consined doing something to reduce length.");
				}
			}
			return files.ToArray();
		}

		static FileInfoExtended[] GetFileInfos(string[] pathes, IProgress<int> progress = null, CancellationTokenSource token = null)
		{
			var fileInfos = new List<FileInfoExtended>();
			for (int i = 0; i < pathes.Length && (!token?.IsCancellationRequested ?? true); i++) {
				var path = pathes[i];
				fileInfos.Add(new FileInfoExtended(path));
				if (fileInfos.Count % 1000 == 0) {
					progress.Report(fileInfos.Count);
				}
			}
			return fileInfos.ToArray();
		}

		public static bool TryParseFileSize(string value, out long result)
		{
			var values = value.Split(' ');
			if(values.Length != 2) {
				result = 0;
				return false;
			}
			if (SizePrefixes.Contains(values[1])) {
				result = (long)(double.Parse(values[0]) * GetPrefixValue(values[1]));
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

		object _lock = new object();

		private void button1_Click(object sender, EventArgs e)
		{
			_state.SelectedFolder = OSHelper.SelectFolder();
			textBox1.Text = _state.SelectedFolder;
		}

		private void button2_Click(object sender, EventArgs e)
		{
			Process.Start(_state.SelectedFolder);
		}

		private async void button3_Click(object sender, EventArgs e)
		{
			listView1.Items.Clear();
			(listView1.ListViewItemSorter as ListViewColumnSorter).SortColumn = 0;

			var stopwatch = new Stopwatch();
			stopwatch.Start();
			double elapsedSeconds = 0;
			toolStripSplitButtonCancel.Enabled = true;
			_state.CancellationTokenSource = new CancellationTokenSource();
			_state.FilesDiscovered = await Task.Run(() => GetFiles(
				_state.SelectedFolder,
				new Progress<int>(value => {
					lock (_lock) {
						elapsedSeconds = stopwatch.Elapsed.TotalMilliseconds / 1000d;
						if (elapsedSeconds == 0) {
							elapsedSeconds = 1;
						}
						double rate = value / elapsedSeconds;
						toolStripStatusLabel1.Text = $"{_state.SelectedFolder} {value} files discovered... {GetPrefix(rate, PrefixType.File)} files/second";
					}
				}),
				_state.CancellationTokenSource
			));

			stopwatch.Stop();
			elapsedSeconds = stopwatch.Elapsed.TotalMilliseconds / 1000d;

			//ListViewItem name = new ListViewItem(Path.GetFileName(kvp.Key));
			//name.SubItems.Add((kvp.Value / (1024 * 1024)) + " MB");
			
			listView1.Items.AddRange(_state.FilesDiscovered.Select(f => new ListViewItem(f)).ToArray());
			await SetStripStatus(_state.FilesDiscovered.Length, elapsedSeconds);
		}

		private static readonly string[] FilePrefixes = new[] { "kF", "MF", "GF", "TF" };
		private static readonly string[] SizePrefixes = new[] { "kB", "MB", "GB", "TB" };
		private enum PrefixType {
			File,
			Size
		}
		private string GetPrefix(double value, PrefixType type)
		{
			int prefixId = 0;
			if(value < 1000d) {
				return $"{value:#.#} Bytes";
			}
			while((value /= 1024) > 1000d) {
				prefixId++;
			}
			return $"{value:#.#} {(type == PrefixType.File ? FilePrefixes[prefixId] : SizePrefixes[prefixId])}";
		}

		private static int GetPrefixValue(string value)
		{
			if(value.Length > 2) {
				throw new ArgumentException("Prefix should be length on 2, at least at 20.08.2017 14:06 GMT+3");
			}

			int multiplier = 1024;
			int prefixId = 0;
			while (SizePrefixes[prefixId++] != value) {}
			int result = (int)Math.Pow(multiplier, prefixId++);
			return result;
		}

		private Task SetStripStatus(int length, double seconds)
		{
			toolStripStatusLabel1.Text = $"{length} files discovered in {seconds:#.###} seconds";
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

		private async void buttonScan_Click(object sender, EventArgs e)
		{
			listView1.Items.Clear();
			(listView1.ListViewItemSorter as ListViewColumnSorter).SortColumn = 0;

			var stopwatch = new Stopwatch();
			stopwatch.Start();
			double elapsedSeconds = 0;
			toolStripSplitButtonCancel.Enabled = true;
			_state.CancellationTokenSource = new CancellationTokenSource();
			_state.FilesScanned = await Task.Run(() => GetFileInfos(
				_state.FilesDiscovered,
				new Progress<int>(value => {
					lock (_lock)
					{
						elapsedSeconds = stopwatch.Elapsed.TotalMilliseconds / 1000d;
						if (elapsedSeconds == 0) {
							elapsedSeconds = 1;
						}
						double rate = value / elapsedSeconds;
						toolStripStatusLabel1.Text = $"{_state.SelectedFolder} {value} files scanned... {GetPrefix(rate, PrefixType.File)} files/second";
					}
				}),
				_state.CancellationTokenSource
			));

			stopwatch.Stop();
			elapsedSeconds = stopwatch.Elapsed.TotalMilliseconds / 1000d;

			//ListViewItem name = new ListViewItem(Path.GetFileName(kvp.Key));
			//name.SubItems.Add((kvp.Value / (1024 * 1024)) + " MB");

			listView1.Items.Clear();
			listView1.Items.AddRange(_state.FilesScanned.Select(f => {
				ListViewItem item = new ListViewItem(f.Name);
				item.SubItems.Add(GetPrefix(f.Length, PrefixType.Size));
				item.SubItems.Add(f.DirectoryName);
				item.SubItems.Add(f.FullPath);
				item.SubItems.Add(f.Hash);
				return item;
			}).ToArray());
			await SetStripStatus(_state.FilesScanned.Length, elapsedSeconds);
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			File.WriteAllText("analysis.json", JsonConvert.SerializeObject(_state));
		}

		private void buttonLoad_Click(object sender, EventArgs e)
		{
			_state = JsonConvert.DeserializeObject<State>(File.ReadAllText("analysis.json"));

			textBox1.Text = _state.SelectedFolder;
			listView1.Items.Clear();
			if(_state.FilesScanned != null) {
				listView1.Items.AddRange(_state.FilesScanned.Select(f => {
					ListViewItem item = new ListViewItem(f.Name);
					item.SubItems.Add(GetPrefix(f.Length, PrefixType.Size));
					item.SubItems.Add(f.DirectoryName);
					item.SubItems.Add(f.FullPath);
					item.SubItems.Add(f.Hash);
					return item;
				}).ToArray());
			} else if (_state.FilesDiscovered != null) {
				listView1.Items.AddRange(_state.FilesDiscovered.Select(f => new ListViewItem(f)).ToArray());
			}
		}

		private async void buttonHash_Click(object sender, EventArgs e)
		{
			await Task.Run(() => {
				int index = 0;
				_state.FilesScanned = _state.FilesScanned.AsParallel().Select(f => {
					if (index++ % 10 == 0) {
						lock(_lock) {
							toolStripStatusLabel1.Text = $"{index} / {_state.FilesScanned.Length} hashed";
						}
					}
					return new FileInfoExtended(f, Hash.GetSHA256(File.ReadAllBytes(f.FullPath)));
				}).ToArray();
			});
			toolStripStatusLabel1.Text = $"{_state.FilesScanned.Length} files hashed";
			listView1.Items.Clear();
			if(_state.FilesScanned != null) {
				listView1.Items.AddRange(_state.FilesScanned.Select(f => {
					ListViewItem item = new ListViewItem(f.Name);
					item.SubItems.Add(GetPrefix(f.Length, PrefixType.Size));
					item.SubItems.Add(f.DirectoryName);
					item.SubItems.Add(f.FullPath);
					item.SubItems.Add(f.Hash);
					return item;
				}).ToArray());
			} else if (_state.FilesDiscovered != null) {
				listView1.Items.AddRange(_state.FilesDiscovered.Select(f => new ListViewItem(f)).ToArray());
			}
		}

		private void buttonShowDuplicates_Click(object sender, EventArgs e)
		{
			listView1.Items.Clear();
			if (_state.FilesScanned != null) {
				listView1.Items.AddRange(_state.FilesScanned
					.GroupBy(item => item.Hash)
					.Where(g => g.Count() > 1)
					.SelectMany(g => g.ToArray())
					.Select(f => {
						ListViewItem item = new ListViewItem(f.Name);
						item.SubItems.Add(GetPrefix(f.Length, PrefixType.Size));
						item.SubItems.Add(f.DirectoryName);
						item.SubItems.Add(f.FullPath);
						item.SubItems.Add(f.Hash);
						return item;
				}).ToArray());
			}
		}
	}
}
