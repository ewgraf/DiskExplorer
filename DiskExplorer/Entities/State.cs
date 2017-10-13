using System.Threading;
using Newtonsoft.Json;

namespace DiskExplorer.Entities {
    public class State {
        public string SelectedFolder = @"E:\Unsorted\С торрента";
        public FileInfoExtended[] Files;
        [JsonIgnore]
        public CancellationTokenSource CancellationTokenSource = new CancellationTokenSource();
    }
}
