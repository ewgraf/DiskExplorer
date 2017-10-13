using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DiskExplorer.Entities
{
    public class FileInfoExtended
    {
        private FileInfo _fileInfo;
        public string Name { get; set; }
        public long Length { get; set; }
        public string DirectoryName { get; set; }
        public string FullPath { get; set; }
        public DateTime LastWriteTime { get; set; }
        public string Hash { get; set; }

        public FileInfoExtended() {} // for Json

        public FileInfoExtended(string fileName) {
            _fileInfo = new FileInfo(fileName);
            Name = _fileInfo.Name;
            Length = _fileInfo.Length;
            DirectoryName = _fileInfo.DirectoryName;
            FullPath = Path.Combine(DirectoryName, Name);
            LastWriteTime = _fileInfo.LastWriteTime;
        }

        public FileInfoExtended(FileInfoExtended info, string hash) {
            Name = info.Name;
            Length = info.Length;
            DirectoryName = info.DirectoryName;
            FullPath = Path.Combine(DirectoryName, Name);
            Hash = hash;
        }

        public string SizeWithPrefix() => FileUtils.SizeSuffix(this.Length);
    }

    public static class FileInfoExtendedExtensions {
        public static FileInfoExtended[] ComputeHashesParallel(this FileInfoExtended[] files, Func<bool> toUpdate = null, IProgress<(int, long, string)> progress = null, CancellationTokenSource cancellationTokenSource = null) {
            if(files == null) {
                throw new ArgumentNullException(nameof(files));
            }
            if(files.Length == 0) {
                return files;
            }

            int filesHashed = 0;
            long filesSizeDiscovered = 0;
            return files.AsParallel()
                .Select(f => {
                    if (cancellationTokenSource?.IsCancellationRequested ?? false) {
                        throw new OperationCanceledException();
                    }
                    filesHashed++;
                    filesSizeDiscovered += f.Length;
                    if (progress != null && toUpdate != null && toUpdate()) {
                        progress.Report((filesHashed, filesSizeDiscovered, f.FullPath));
                    }
                    return new FileInfoExtended(f, Hash.GetFileHash(f.FullPath));
                    //return new FileInfoExtended(f, Hash.SuperFastHashUnsafeFile(f.FullPath));
                })
                .ToArray();
        }
    }
}
