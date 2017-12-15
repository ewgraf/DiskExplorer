using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DiskExplorer.Entities {
    public class FileInfoExtended {
        private FileInfo _fileInfo;
        public string Name { get; set; }
        public long Length { get; set; }
        public string DirectoryName { get; set; }
        public string FullPath => Path.Combine(DirectoryName, Name);
        public DateTime CreationTime { get; set; }
        public DateTime LastWriteTime { get; set; }
        public string Hash { get; set; }

        public FileInfoExtended() {} // for Json

        public FileInfoExtended(string fileName) {
            _fileInfo = new FileInfo(fileName);
            Name = _fileInfo.Name;
            Length = _fileInfo.Length;
            DirectoryName = _fileInfo.DirectoryName;
            CreationTime = _fileInfo.CreationTime;
            LastWriteTime = _fileInfo.LastWriteTime;
        }

        public FileInfoExtended(FileInfoExtended info, string hash) {
            Name = info.Name;
            Length = info.Length;
            DirectoryName = info.DirectoryName;
            Hash = hash;
        }

        public string SizeWithPrefix() => FileUtils.SizeSuffix(this.Length);
    }

    public static class FileInfoExtendedExtensions {
        public static FileInfoExtended[] ComputeHashesParallel(this FileInfoExtended[] files, Func<bool> toUpdate = null, IProgress<(FileInfoExtended, TimeSpan, long, string)> progress = null, CancellationTokenSource cancellationTokenSource = null) {
            if(files == null) {
                throw new ArgumentNullException(nameof(files));
            }
            if(files.Length == 0) {
                return files;
            }

            var orderedFiles = files.OrderByDescending(f => f.Length);
            var bag = new ConcurrentBag<FileInfoExtended>();
            bool firstIteration = true;
            Parallel.ForEach(orderedFiles, new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount }, (f, loopState, i) => {
                if (cancellationTokenSource?.IsCancellationRequested ?? false) {
                    loopState.Break();
                }
                if (firstIteration) {
                    progress.Report((null, TimeSpan.Zero, 0, $"Started hashing size: {f.SizeWithPrefix()} name: {f.Name}"));
                    firstIteration = false;
                }

                Stopwatch sw = new Stopwatch();
                sw.Start();
                if (!FileUtils.IsFileLocked(f.FullPath)) {
                    //f.Hash = Hash.GetSHA1(f.FullPath);
                    try {
                        f.Hash = Hash.GetMD5(f.FullPath);
                    } catch (Exception ex) {
                        f.Hash = ex.Message;
                    }                    
                }
                sw.Stop();
                
                if (progress != null && toUpdate != null && toUpdate()) {
                    progress.Report((f, sw.Elapsed, i, null));
                }                
                bag.Add(f);
            });
            progress?.Report((null, TimeSpan.Zero, 0, "Done"));
            return bag.Where(i => i.Hash != null && i.Hash != string.Empty).ToArray();
        }

        public static Folder ComputeHashesParallel(this Folder root, Func<bool> toUpdate = null, IProgress<(FileInfoExtended, TimeSpan, long, string)> progress = null, CancellationTokenSource cancellationTokenSource = null) {
            if (root == null) {
                throw new ArgumentNullException(nameof(root));
            }

            List<Folder> folders = new[] { root }.ToList();
            for (int i = 0; i < folders.Count; i++) {
                folders[i].Files = ComputeHashesParallel(folders[i].Files, toUpdate, progress, cancellationTokenSource);
                folders.AddRange(folders[i].Subfolders);
            }
            return folders[0];
        }
    }
}
