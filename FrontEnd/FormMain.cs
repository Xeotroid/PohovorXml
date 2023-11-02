using log4net;
using Microsoft.VisualBasic;
using System.Diagnostics;
using System.IO;

namespace FrontEnd {
    public partial class FormMain : Form {
        private BackEnd.Config _config;
        private static log4net.ILog? _log;

        public FormMain() {
            InitializeComponent();
            _config = new();
            //"One important thing to note is that your main runnable project MUST be
            //the first project that calls the LogManager.GetLogger method."
            _log = log4net.LogManager.GetLogger("FormMain.cs");
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

            var deserialiser = new BackEnd.Converter(_config);
            try {
                deserialiser.Work();
                _log.Info("Konverze dokon�ena.");
                MessageBox.Show("Hotovo.", this.Name, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) {
                _log.Fatal("P�i konverzi nastala chyba.", ex);
                MessageBox.Show($"P�i ukl�d�n� nastala chyba:\r\n{ex.Message}", this.Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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