using System;
using System.Collections.Concurrent;
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
        public DateTime LastWriteTime { get; set; }
        public string Hash { get; set; }

        public FileInfoExtended() {} // for Json

        public FileInfoExtended(string fileName) {
            _fileInfo = new FileInfo(fileName);
            Name = _fileInfo.Name;
            Length = _fileInfo.Length;
            DirectoryName = _fileInfo.DirectoryName;
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
        public static FileInfoExtended[] ComputeHashesParallel(this FileInfoExtended[] files, Func<bool> toUpdate = null, IProgress<(int, long, FileInfoExtended, TimeSpan, long, string)> progress = null, CancellationTokenSource cancellationTokenSource = null) {
            if(files == null) {
                throw new ArgumentNullException(nameof(files));
            }
            if(files.Length == 0) {
                return files;
            }

            var orderedFiles = files.OrderByDescending(f => f.Length);
            var bag = new ConcurrentBag<FileInfoExtended>();
            int filesHashed = 0;
            long filesSizeDiscovered = 0;
            Parallel.ForEach(orderedFiles, new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount }, (f, loopState, i) => {
                if (cancellationTokenSource?.IsCancellationRequested ?? false) {
                    loopState.Break();
                }
                if (filesHashed == 0) {
                    progress.Report((0, 0, null, TimeSpan.Zero, 0, $"Started hashing size: {f.SizeWithPrefix()} name: {f.Name}"));
                }

                Stopwatch sw = new Stopwatch();
                sw.Start();
                f.Hash = Hash.GetSHA1(f.FullPath);
                sw.Stop();

                filesHashed++;
                filesSizeDiscovered += f.Length;
                progress.Report((filesHashed, filesSizeDiscovered, f, sw.Elapsed, i, null));
                bag.Add(f);
            });
            progress.Report((0, 0, null, TimeSpan.Zero, 0, "Done"));
            return bag.ToArray();
        }
    }
}
