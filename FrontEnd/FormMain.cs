using System.Diagnostics;
using System.IO;

namespace FrontEnd {
    public partial class FormMain : Form {
        private BackEnd.Config _config;

        public FormMain() {
            InitializeComponent();
            _config = new();
        }

        private void btnLoad_Click(object sender, EventArgs e) {
            if (opnOpen.ShowDialog() != DialogResult.OK) {
                return;
            }
            foreach (string path in opnOpen.FileNames) {
                bool added = _config.AddInputFile(path);
                if (added) {
                    lstSoubory.Items.Add(path);
                }
            }
            UpdateSaveButton();
        }

        private void btnDelete_Click(object sender, EventArgs e) {
            var path = lstSoubory.SelectedItem;
            if (path == null) {
                return;
            }
            _config.RemoveInputFile(path.ToString());
            lstSoubory.Items.Remove(path);
            UpdateSaveButton();
        }

        private void btnSave_Click(object sender, EventArgs e) {
            if (savSave.ShowDialog() != DialogResult.OK) {
                return;
            }
            _config.OutputPath = savSave.FileName;

            //TODO: nahradit bool nìèím jiným
            var deserialiser = new BackEnd.Converter(_config);
            bool result = deserialiser.Work();
            Debug.Print(result ? "Úspìch" : "Fail.");
        }

        private void lstSoubory_SelectedValueChanged(object sender, EventArgs e) {
            btnDelete.Enabled = lstSoubory.SelectedItems.Count > 0;
        }

        //

        private void UpdateSaveButton() {
            btnSave.Enabled = lstSoubory.Items.Count > 0;
        }
    }
}