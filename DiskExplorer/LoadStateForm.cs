using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using DiskExplorer.Entities;
using Newtonsoft.Json;

namespace DiskExplorer {
    public partial class LoadStateForm : Form {
        private readonly string _analisysPath;
        public State State { get; set; }

        public LoadStateForm(string analisysPath) {
            InitializeComponent();
            _analisysPath = analisysPath;
        }

        private async void LoadStateForm_Load(object s, EventArgs e) {
            if (File.Exists(_analisysPath)) {                
                State = await Task.Run(() => JsonConvert.DeserializeObject<State>(File.ReadAllText(_analisysPath)));
                this.DialogResult = DialogResult.OK;
            }
            this.Close();
        }
    }
}
