using System;
using System.IO;

namespace DiskExplorer
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

        public FileInfoExtended(string fileName)
        {
            _fileInfo = new FileInfo(fileName);
            Name = _fileInfo.Name;
            Length = _fileInfo.Length;
            DirectoryName = _fileInfo.DirectoryName;
            FullPath = Path.Combine(DirectoryName, Name);
            LastWriteTime = _fileInfo.LastWriteTime;
        }

        public FileInfoExtended(FileInfoExtended info, string hash)
        {
            Name = info.Name;
            Length = info.Length;
            DirectoryName = info.DirectoryName;
            FullPath = Path.Combine(DirectoryName, Name);
            Hash = hash;
        }

        public string SizeWithPrefix() => FileUtils.SizeSuffix(this.Length);
    }
}
