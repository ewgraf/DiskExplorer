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
                foreach (FileInfoExtended fileInfo in GetFilesOrDefault(directory, pattern).Select(f => new FileInfoExtended(f))) {
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

        public string[] GetFilesOrDefault(string folder, string searchPattern, SearchOption searchOption = SearchOption.TopDirectoryOnly) {
            try {
                return Directory.GetFiles(folder, searchPattern, searchOption);
            } catch (UnauthorizedAccessException ex) {
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
