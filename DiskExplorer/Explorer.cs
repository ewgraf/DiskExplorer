using DiskExplorer.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DiskExplorer
{
    public class Explorer {
        public Explorer() {

        }

        public IEnumerable<FileInfoExtended> GetFiles(string path, Func<bool> toUpdate = null, IProgress<(int, long, string)> progress = null, /*PauseToken pauseToken = null, */CancellationTokenSource token = null) {
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
                    if (progress != null && toUpdate != null && toUpdate()) {
                        progress.Report((filesDiscovered, filesSizeDiscovered, fileInfo.FullPath));
                    }
                }
                string[] dirDirectories = GetDirectoriesOrDefault(directory);
                directories.AddRange(dirDirectories);
            }
        }

        public string[] GetFilesOrDefault(string path, string searchPattern = "*", SearchOption searchOption = SearchOption.TopDirectoryOnly) {
            try {
                return Directory.GetFiles(path, searchPattern, searchOption);
            } catch (UnauthorizedAccessException ex) {
                return Array.Empty<string>();
            }
        }

        public string[] GetDirectoriesOrDefault(string path, string searchPattern = "*", SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            try {
                return Directory.GetDirectories(path, searchPattern, searchOption);
            } catch (UnauthorizedAccessException ex) {
                return Array.Empty<string>();
            }
        }

    }
}
