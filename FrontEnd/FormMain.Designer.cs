namespace FrontEnd {
    partial class FormMain {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            btnLoad = new Button();
            btnSave = new Button();
            btnDelete = new Button();
            opnOpen = new OpenFileDialog();
            savSave = new SaveFileDialog();
            lstFiles = new ListBox();
            SuspendLayout();
            // 
            // btnLoad
            // 
            btnLoad.Location = new Point(12, 12);
            btnLoad.Name = "btnLoad";
            btnLoad.Size = new Size(112, 34);
            btnLoad.TabIndex = 1;
            btnLoad.Text = "Nahrát soubory...";
            btnLoad.UseVisualStyleBackColor = true;
            btnLoad.Click += btnLoad_Click;
            // 
            // btnSave
            // 
            btnSave.Enabled = false;
            btnSave.Location = new Point(116, 232);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(86, 34);
            btnSave.TabIndex = 2;
            btnSave.Text = "Uložit...";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnDelete
            // 
            btnDelete.Enabled = false;
            btnDelete.Location = new Point(277, 12);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(30, 34);
            btnDelete.TabIndex = 3;
            btnDelete.Text = "X";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // opnOpen
            // 
            opnOpen.Filter = "XML soubory|*.xml|Všechny soubory|*.*";
            opnOpen.Multiselect = true;
            // 
            // savSave
            // 
            savSave.DefaultExt = "csv";
            savSave.Filter = "CSV soubory|*.csv|XLSX soubory|*.xlsx";
            // 
            // lstFiles
            // 
            lstFiles.FormattingEnabled = true;
            lstFiles.ItemHeight = 15;
            lstFiles.Location = new Point(12, 52);
            lstFiles.Name = "lstFiles";
            lstFiles.SelectionMode = SelectionMode.MultiExtended;
            lstFiles.Size = new Size(295, 169);
            lstFiles.TabIndex = 4;
            lstFiles.SelectedValueChanged += lstFiles_SelectedValueChanged;
            lstFiles.DragDrop += FormMain_DragDrop;
            lstFiles.DragEnter += FormMain_DragEnter;
            // 
            // FormMain
            // 
            AllowDrop = true;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(319, 278);
            Controls.Add(lstFiles);
            Controls.Add(btnDelete);
            Controls.Add(btnSave);
            Controls.Add(btnLoad);
            MaximizeBox = false;
            MaximumSize = new Size(335, 317);
            MinimizeBox = false;
            MinimumSize = new Size(335, 317);
            Name = "FormMain";
            Text = "Zaměstnanecký tabulkovátor 3000";
            DragDrop += FormMain_DragDrop;
            DragEnter += FormMain_DragEnter;
            ResumeLayout(false);
        }

        #endregion
        private Button btnLoad;
        private Button btnSave;
        private Button btnDelete;
        private OpenFileDialog opnOpen;
        private SaveFileDialog savSave;
        private ListBox lstFiles;
    }
}