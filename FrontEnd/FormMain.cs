namespace FrontEnd {
    public partial class FormMain : Form {
        private BackEnd.Config _config;
        private static log4net.ILog? _log;

        public FormMain() {
            InitializeComponent();
            _config = new();
            //"One important thing to note is that your main runnable project MUST be
            //the first project that calls the LogManager.GetLogger method."
            //Opravdu bych to cht�l vy�e�it jinak...
            _log = log4net.LogManager.GetLogger("FormMain.cs");
            _log = BackEnd.LogHelper.GetLogger();
        }

        private void btnLoad_Click(object sender, EventArgs e) {
            if (opnOpen.ShowDialog() != DialogResult.OK) {
                return;
            }
            LoadFiles(opnOpen.FileNames);
        }

        private void btnDelete_Click(object sender, EventArgs e) {
            while (lstFiles.SelectedItems.Count > 0) {
                var path = lstFiles.SelectedItems[0];
                _config.RemoveInputFile(path.ToString()!);
                lstFiles.Items.Remove(path);
            }

            UpdateSaveButton();
        }

        private void btnSave_Click(object sender, EventArgs e) {
            if (savSave.ShowDialog() != DialogResult.OK) {
                return;
            }
            _config.OutputPath = savSave.FileName;
            //Cht�lo by to n�jakou register metodu, kter� po spu�t�n� napln� dialogfilter
            //a t�eba n�jak� slovn�k nebo pole v BackEnd.Config, aby se nov� form�t dal
            //v GUI nastavit z jednoho m�sta, ale to u� nest�h�m.
            _config.SetExportFormat(GetCurrentDialogFilter(savSave).Substring(2));
            DoConvert();
        }

        private void lstFiles_SelectedValueChanged(object sender, EventArgs e) {
            btnDelete.Enabled = lstFiles.SelectedItems.Count > 0;
        }

        private void FormMain_DragEnter(object sender, DragEventArgs e) {
            if (e.Data == null) return;
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }

        private void FormMain_DragDrop(object sender, DragEventArgs e) {
            if (e.Data == null) return;
            var files = (string[])e.Data.GetData(DataFormats.FileDrop);
            LoadFiles(files);
        }

        //

        private void LoadFiles(string[] paths) {
            foreach (string path in paths) {
                bool added = _config.AddInputFile(path);
                if (added) {
                    lstFiles.Items.Add(path);
                }
            }
            UpdateSaveButton();
        }

        private void UpdateSaveButton() {
            btnSave.Enabled = lstFiles.Items.Count > 0;
        }

        private void DoConvert() {
            var deserialiser = new BackEnd.Converter(_config);
            try {
                deserialiser.Work();
                _log!.Info("Konverze dokon�ena.");
                MessageBox.Show("Hotovo.", this.Name, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) {
                _log!.Fatal("P�i konverzi nastala chyba.", ex);
                MessageBox.Show($"P�i ukl�d�n� nastala chyba:\r\n{ex.Message}", this.Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetCurrentDialogFilter(FileDialog dialog) {
            string[] filterstring = dialog.Filter.Split('|');
            return filterstring[(dialog.FilterIndex - 1) * 2 + 1];
        }
    }
}