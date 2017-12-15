using System.Threading;
using Newtonsoft.Json;

namespace DiskExplorer.Entities {
    public class State {
        public string SelectedFolder = @"C:\";
        public string Pattern = "*";
        public Folder Folder;
        public Folder CurrentlyExploringFolder;
        [JsonIgnore]
        public CancellationTokenSource CancellationTokenSource = new CancellationTokenSource();
    }
}
