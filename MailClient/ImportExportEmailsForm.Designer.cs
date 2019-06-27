namespace MailClient
{
    partial class ImportExportEmailsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImportExportEmailsForm));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.pictureBoxLoading = new System.Windows.Forms.PictureBox();
            this.buttonImport = new System.Windows.Forms.Button();
            this.groupBoxCredentials = new System.Windows.Forms.GroupBox();
            this.textBoxImapPort = new System.Windows.Forms.TextBox();
            this.textBoxImap = new System.Windows.Forms.TextBox();
            this.labelImapPort = new System.Windows.Forms.Label();
            this.labelImap = new System.Windows.Forms.Label();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.tbUsername = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBoxImportTo = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxServerFolder = new System.Windows.Forms.ComboBox();
            this.labelFolder = new System.Windows.Forms.Label();
            this.comboBoxImportFrom = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.buttonCancel2 = new System.Windows.Forms.Button();
            this.buttonExport = new System.Windows.Forms.Button();
            this.comboBoxExportFrom = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLoading)).BeginInit();
            this.groupBoxCredentials.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tabControl1.Location = new System.Drawing.Point(4, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(471, 430);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.buttonCancel);
            this.tabPage1.Controls.Add(this.pictureBoxLoading);
            this.tabPage1.Controls.Add(this.buttonImport);
            this.tabPage1.Controls.Add(this.groupBoxCredentials);
            this.tabPage1.Controls.Add(this.comboBoxImportTo);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.comboBoxServerFolder);
            this.tabPage1.Controls.Add(this.labelFolder);
            this.tabPage1.Controls.Add(this.comboBoxImportFrom);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 30);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(463, 396);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Import";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(238, 358);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(108, 35);
            this.buttonCancel.TabIndex = 9;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // pictureBoxLoading
            // 
            this.pictureBoxLoading.Image = global::MailClient.Properties.Resources.loading;
            this.pictureBoxLoading.Location = new System.Drawing.Point(20, 356);
            this.pictureBoxLoading.Name = "pictureBoxLoading";
            this.pictureBoxLoading.Size = new System.Drawing.Size(42, 40);
            this.pictureBoxLoading.TabIndex = 8;
            this.pictureBoxLoading.TabStop = false;
            this.pictureBoxLoading.Visible = false;
            // 
            // buttonImport
            // 
            this.buttonImport.Location = new System.Drawing.Point(352, 358);
            this.buttonImport.Name = "buttonImport";
            this.buttonImport.Size = new System.Drawing.Size(108, 35);
            this.buttonImport.TabIndex = 7;
            this.buttonImport.Text = "Import";
            this.buttonImport.UseVisualStyleBackColor = true;
            this.buttonImport.Click += new System.EventHandler(this.buttonImport_Click);
            // 
            // groupBoxCredentials
            // 
            this.groupBoxCredentials.Controls.Add(this.textBoxImapPort);
            this.groupBoxCredentials.Controls.Add(this.textBoxImap);
            this.groupBoxCredentials.Controls.Add(this.labelImapPort);
            this.groupBoxCredentials.Controls.Add(this.labelImap);
            this.groupBoxCredentials.Controls.Add(this.tbPassword);
            this.groupBoxCredentials.Controls.Add(this.tbUsername);
            this.groupBoxCredentials.Controls.Add(this.label4);
            this.groupBoxCredentials.Controls.Add(this.label5);
            this.groupBoxCredentials.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.groupBoxCredentials.Location = new System.Drawing.Point(6, 143);
            this.groupBoxCredentials.Name = "groupBoxCredentials";
            this.groupBoxCredentials.Size = new System.Drawing.Size(451, 198);
            this.groupBoxCredentials.TabIndex = 6;
            this.groupBoxCredentials.TabStop = false;
            this.groupBoxCredentials.Text = "Server Information";
            this.groupBoxCredentials.Visible = false;
            // 
            // textBoxImapPort
            // 
            this.textBoxImapPort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxImapPort.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBoxImapPort.Location = new System.Drawing.Point(106, 147);
            this.textBoxImapPort.Name = "textBoxImapPort";
            this.textBoxImapPort.Size = new System.Drawing.Size(85, 29);
            this.textBoxImapPort.TabIndex = 11;
            this.textBoxImapPort.Visible = false;
            // 
            // textBoxImap
            // 
            this.textBoxImap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxImap.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBoxImap.Location = new System.Drawing.Point(106, 112);
            this.textBoxImap.Name = "textBoxImap";
            this.textBoxImap.Size = new System.Drawing.Size(330, 29);
            this.textBoxImap.TabIndex = 10;
            this.textBoxImap.Visible = false;
            // 
            // labelImapPort
            // 
            this.labelImapPort.AutoSize = true;
            this.labelImapPort.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelImapPort.Location = new System.Drawing.Point(16, 149);
            this.labelImapPort.Name = "labelImapPort";
            this.labelImapPort.Size = new System.Drawing.Size(82, 21);
            this.labelImapPort.TabIndex = 9;
            this.labelImapPort.Text = "IMAP Port:";
            this.labelImapPort.Visible = false;
            // 
            // labelImap
            // 
            this.labelImap.AutoSize = true;
            this.labelImap.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelImap.Location = new System.Drawing.Point(16, 112);
            this.labelImap.Name = "labelImap";
            this.labelImap.Size = new System.Drawing.Size(50, 21);
            this.labelImap.TabIndex = 8;
            this.labelImap.Text = "IMAP:";
            this.labelImap.Visible = false;
            // 
            // tbPassword
            // 
            this.tbPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbPassword.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tbPassword.Location = new System.Drawing.Point(106, 77);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.Size = new System.Drawing.Size(330, 29);
            this.tbPassword.TabIndex = 7;
            this.tbPassword.UseSystemPasswordChar = true;
            // 
            // tbUsername
            // 
            this.tbUsername.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbUsername.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tbUsername.Location = new System.Drawing.Point(106, 42);
            this.tbUsername.Name = "tbUsername";
            this.tbUsername.Size = new System.Drawing.Size(330, 29);
            this.tbUsername.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.Location = new System.Drawing.Point(16, 79);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 21);
            this.label4.TabIndex = 5;
            this.label4.Text = "Password:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label5.Location = new System.Drawing.Point(16, 42);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 21);
            this.label5.TabIndex = 4;
            this.label5.Text = "Username:";
            // 
            // comboBoxImportTo
            // 
            this.comboBoxImportTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxImportTo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.comboBoxImportTo.FormattingEnabled = true;
            this.comboBoxImportTo.Location = new System.Drawing.Point(112, 86);
            this.comboBoxImportTo.Name = "comboBoxImportTo";
            this.comboBoxImportTo.Size = new System.Drawing.Size(159, 29);
            this.comboBoxImportTo.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(8, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 21);
            this.label3.TabIndex = 4;
            this.label3.Text = "Import to:";
            // 
            // comboBoxServerFolder
            // 
            this.comboBoxServerFolder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxServerFolder.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.comboBoxServerFolder.FormattingEnabled = true;
            this.comboBoxServerFolder.Items.AddRange(new object[] {
            "Inbox",
            "Sent Emails"});
            this.comboBoxServerFolder.Location = new System.Drawing.Point(339, 32);
            this.comboBoxServerFolder.Name = "comboBoxServerFolder";
            this.comboBoxServerFolder.Size = new System.Drawing.Size(103, 29);
            this.comboBoxServerFolder.TabIndex = 3;
            // 
            // labelFolder
            // 
            this.labelFolder.AutoSize = true;
            this.labelFolder.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelFolder.Location = new System.Drawing.Point(279, 35);
            this.labelFolder.Name = "labelFolder";
            this.labelFolder.Size = new System.Drawing.Size(54, 21);
            this.labelFolder.TabIndex = 2;
            this.labelFolder.Text = "folder:";
            // 
            // comboBoxImportFrom
            // 
            this.comboBoxImportFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxImportFrom.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.comboBoxImportFrom.FormattingEnabled = true;
            this.comboBoxImportFrom.Location = new System.Drawing.Point(112, 32);
            this.comboBoxImportFrom.Name = "comboBoxImportFrom";
            this.comboBoxImportFrom.Size = new System.Drawing.Size(159, 29);
            this.comboBoxImportFrom.TabIndex = 1;
            this.comboBoxImportFrom.SelectedIndexChanged += new System.EventHandler(this.comboBoxImportFrom_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(8, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Import from:";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.buttonCancel2);
            this.tabPage2.Controls.Add(this.buttonExport);
            this.tabPage2.Controls.Add(this.comboBoxExportFrom);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Location = new System.Drawing.Point(4, 30);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(463, 396);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Export";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // buttonCancel2
            // 
            this.buttonCancel2.Location = new System.Drawing.Point(238, 358);
            this.buttonCancel2.Name = "buttonCancel2";
            this.buttonCancel2.Size = new System.Drawing.Size(108, 35);
            this.buttonCancel2.TabIndex = 10;
            this.buttonCancel2.Text = "Cancel";
            this.buttonCancel2.UseVisualStyleBackColor = true;
            this.buttonCancel2.Click += new System.EventHandler(this.buttonCancel2_Click);
            // 
            // buttonExport
            // 
            this.buttonExport.Location = new System.Drawing.Point(352, 358);
            this.buttonExport.Name = "buttonExport";
            this.buttonExport.Size = new System.Drawing.Size(108, 35);
            this.buttonExport.TabIndex = 8;
            this.buttonExport.Text = "Export";
            this.buttonExport.UseVisualStyleBackColor = true;
            this.buttonExport.Click += new System.EventHandler(this.buttonExport_Click);
            // 
            // comboBoxExportFrom
            // 
            this.comboBoxExportFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxExportFrom.FormattingEnabled = true;
            this.comboBoxExportFrom.Location = new System.Drawing.Point(115, 19);
            this.comboBoxExportFrom.Name = "comboBoxExportFrom";
            this.comboBoxExportFrom.Size = new System.Drawing.Size(154, 29);
            this.comboBoxExportFrom.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(95, 21);
            this.label6.TabIndex = 2;
            this.label6.Text = "Export from:";
            // 
            // ImportExportEmailsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(477, 433);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(493, 472);
            this.MinimumSize = new System.Drawing.Size(493, 472);
            this.Name = "ImportExportEmailsForm";
            this.Text = "Import/Export Emails";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLoading)).EndInit();
            this.groupBoxCredentials.ResumeLayout(false);
            this.groupBoxCredentials.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBoxCredentials;
        private System.Windows.Forms.ComboBox comboBoxImportTo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxServerFolder;
        private System.Windows.Forms.Label labelFolder;
        private System.Windows.Forms.ComboBox comboBoxImportFrom;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonImport;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.TextBox tbUsername;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonExport;
        private System.Windows.Forms.ComboBox comboBoxExportFrom;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.PictureBox pictureBoxLoading;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.TextBox textBoxImapPort;
        private System.Windows.Forms.TextBox textBoxImap;
        private System.Windows.Forms.Label labelImapPort;
        private System.Windows.Forms.Label labelImap;
        private System.Windows.Forms.Button buttonCancel2;
    }
}