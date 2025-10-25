namespace UblSharpDemo
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtDirPath = new TextBox();
            btnListFiles = new Button();
            lstFiles = new ListBox();
            txtStatus = new TextBox();
            SuspendLayout();
            // 
            // txtDirPath
            // 
            txtDirPath.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtDirPath.Location = new Point(114, 12);
            txtDirPath.Name = "txtDirPath";
            txtDirPath.ReadOnly = true;
            txtDirPath.Size = new Size(1458, 23);
            txtDirPath.TabIndex = 0;
            // 
            // btnListFiles
            // 
            btnListFiles.Location = new Point(12, 12);
            btnListFiles.Name = "btnListFiles";
            btnListFiles.Size = new Size(96, 23);
            btnListFiles.TabIndex = 1;
            btnListFiles.Text = "Select Folder";
            btnListFiles.UseVisualStyleBackColor = true;
            btnListFiles.Click += btnListFiles_Click;
            // 
            // lstFiles
            // 
            lstFiles.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lstFiles.FormattingEnabled = true;
            lstFiles.ItemHeight = 15;
            lstFiles.Location = new Point(12, 41);
            lstFiles.Name = "lstFiles";
            lstFiles.Size = new Size(1560, 259);
            lstFiles.TabIndex = 2;
            lstFiles.SelectedIndexChanged += lstFiles_SelectedIndexChanged;
            // 
            // txtStatus
            // 
            txtStatus.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtStatus.Location = new Point(12, 306);
            txtStatus.Multiline = true;
            txtStatus.Name = "txtStatus";
            txtStatus.ReadOnly = true;
            txtStatus.ScrollBars = ScrollBars.Both;
            txtStatus.Size = new Size(1560, 543);
            txtStatus.TabIndex = 3;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1584, 861);
            Controls.Add(txtStatus);
            Controls.Add(lstFiles);
            Controls.Add(btnListFiles);
            Controls.Add(txtDirPath);
            Name = "MainForm";
            Text = "OASIS UBL Invoice Reader";
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private TextBox txtDirPath;
        private Button btnListFiles;
        private ListBox lstFiles;
        private TextBox txtStatus;
    }
}