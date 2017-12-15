using System;
using System.IO;
using System.Linq;

namespace DiskExplorer.Entities {
    public class Folder {
        public string Path { get; set; }
        public long Size { get; set; }
        public Folder[] Subfolders { get; set; } = Array.Empty<Folder>();
        public FileInfoExtended[] Files { get; set; } = Array.Empty<FileInfoExtended>();
        public int FilesTotal { get; set; }
        //public string Name => Path.Split(new[] { '/' }).Last();

        public FileInfoExtended[] GetAllFiles() {
            return Subfolders.Flatten(f => f.Subfolders)
                .SelectMany(f => f.Files)
                .Concat(Files)
                .ToArray();
        }
    }
}
