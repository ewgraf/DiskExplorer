using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using DiskExplorer.Entities;

namespace DiskExplorer {
    public class Explorer {
        public IEnumerable<FileInfoExtended> GetFiles(string folder, string pattern, Func<bool> toUpdate = null, IProgress<(int, long, string)> progress = null, /*PauseToken pauseToken = null, */CancellationTokenSource token = null) {
            int filesDiscovered = 0;
            long filesSizeDiscovered = 0;
            List<string> directories = new[] { folder }.ToList();
            for (int i = 0; i < directories.Count && (!token?.IsCancellationRequested ?? true); i++) {
                string directory = directories[i];
                foreach (FileInfoExtended fileInfo in GetFilesOrDefault(directory, pattern)
                	    .Select(f => { try { return new FileInfoExtended(f); } catch { return null; } })
                	    .Where(f => f != null)) {
                    if (token?.IsCancellationRequested ?? true) {
                        break;
                    }
                    //if(pauseToken?.IsPaused ?? true) {
                    //    await pauseToken.WaitWhilePausedAsync();
                    //}
                    if (fileInfo.FullPath.Length > 260) { // MAX_PATH = 260, Windows OS since 2000.XP,2003
                        throw new ArgumentException($"Path '{folder}' is too long to be handle by your OS. Consined doing something to reduce length.");
                    }
                    filesDiscovered++;
                    filesSizeDiscovered += fileInfo.Length;

                    yield return fileInfo;

                    if (progress != null && toUpdate != null && toUpdate()) {
                        progress.Report((filesDiscovered, filesSizeDiscovered, fileInfo.FullPath));
                    }
                }
                string[] dirDirectories = GetDirectoriesOrDefault(directory);
                directories.AddRange(dirDirectories);
            }
        }

        public Folder GetFilesTree(string folder, string pattern, Func<bool> toUpdate = null, IProgress<(int, long, string)> progress = null, /*PauseToken pauseToken = null, */CancellationTokenSource token = null) {
            int filesDiscovered = 0;
            long filesSizeDiscovered = 0;
            List<string> directories = new[] { folder }.ToList();

            List<Folder> folders = new[] { new Folder { Path = folder } }.ToList();
            for (int i = 0; i < folders.Count; i++) {
                FileInfoExtended[] files = GetFilesOrDefault(folders[i].Path, pattern)
                        .Select(filePath => { try { return new FileInfoExtended(filePath); } catch { return null; } })
                        .Where(fi => fi != null)
                        .ToArray();
                Folder[] subfolders = GetDirectoriesOrDefault(folders[i].Path)
                    .Select(d => new Folder { Path = d })
                    .ToArray();
                folders[i].Files = files;
                folders[i].Subfolders = subfolders;
                folders.AddRange(subfolders);

                filesDiscovered += files.Length;
                filesSizeDiscovered += files.Sum(f => f.Length);
                if (progress != null && toUpdate != null && toUpdate()) {
                    progress.Report((filesDiscovered, filesSizeDiscovered, files.FirstOrDefault()?.FullPath ?? string.Empty));
                }
            }
            for (int i = folders.Count - 1; i >= 0; i--) {
                folders[i].Size = folders[i].Subfolders.Sum(f => f.Size) + folders[i].Files.Sum(f => f.Length);
            }
            if (progress != null) {
                progress.Report((filesDiscovered, filesSizeDiscovered, folders.Where(f => f.Files.Any()).Last().Files.Last().FullPath));
            }
            Folder root = folders[0];
            root.FilesTotal = filesDiscovered;
            return root;
        }

        public string[] GetFilesOrDefault(string folder, string searchPattern, SearchOption searchOption = SearchOption.TopDirectoryOnly) {
            try {
                return Directory.GetFiles(folder, searchPattern, searchOption);
            } catch (UnauthorizedAccessException ex) {
                // Log.Warn Отказано в пути
                return Array.Empty<string>();
            }
        }

        public string[] GetDirectoriesOrDefault(string folder, SearchOption searchOption = SearchOption.TopDirectoryOnly) {
            try {
                return Directory.GetDirectories(folder);
            } catch (UnauthorizedAccessException ex) {
                return Array.Empty<string>();
            }
        }
    }
}
