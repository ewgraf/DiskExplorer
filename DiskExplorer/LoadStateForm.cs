using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using DiskExplorer.Entities;
using Newtonsoft.Json;

namespace DiskExplorer {
    public partial class LoadStateForm : Form {
        public State State { get; set; }
        public LoadStateForm() => InitializeComponent();

        private async void LoadStateForm_Load(object sender, EventArgs e) {
            if (File.Exists("analysis.json")) {                
                State = await Task.Run(() => JsonConvert.DeserializeObject<State>(File.ReadAllText("analysis.json")));
                this.DialogResult = DialogResult.OK;
            }
            this.Close();
        }
    }
}
