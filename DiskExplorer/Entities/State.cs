using System.Threading;
using Newtonsoft.Json;

namespace DiskExplorer.Entities {
    public class State {
        public string SelectedFolder = @"C:\";
        public string Pattern = "*";
        public FileInfoExtended[] Files;
        [JsonIgnore]
        public CancellationTokenSource CancellationTokenSource = new CancellationTokenSource();
    }
}
