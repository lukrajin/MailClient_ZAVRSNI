namespace MailClient
{
    partial class LoginForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbUsername = new System.Windows.Forms.TextBox();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.comboBoxServer = new System.Windows.Forms.ComboBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBoxLoading = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBoxSmtpPort = new System.Windows.Forms.TextBox();
            this.textBoxSmtp = new System.Windows.Forms.TextBox();
            this.labelSmtpPort = new System.Windows.Forms.Label();
            this.labelSmtp = new System.Windows.Forms.Label();
            this.textBoxImapPort = new System.Windows.Forms.TextBox();
            this.textBoxImap = new System.Windows.Forms.TextBox();
            this.labelImapPort = new System.Windows.Forms.Label();
            this.labelImap = new System.Windows.Forms.Label();
            this.radioButtonCustomServer = new System.Windows.Forms.RadioButton();
            this.radioButtonChoosePreset = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLoading)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(12, 123);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Username:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(12, 170);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 21);
            this.label2.TabIndex = 1;
            this.label2.Text = "Password:";
            // 
            // tbUsername
            // 
            this.tbUsername.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbUsername.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.tbUsername.Location = new System.Drawing.Point(119, 120);
            this.tbUsername.Name = "tbUsername";
            this.tbUsername.Size = new System.Drawing.Size(362, 29);
            this.tbUsername.TabIndex = 1;
            // 
            // tbPassword
            // 
            this.tbPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbPassword.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tbPassword.Location = new System.Drawing.Point(119, 167);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.Size = new System.Drawing.Size(362, 29);
            this.tbPassword.TabIndex = 2;
            this.tbPassword.UseSystemPasswordChar = true;
            // 
            // btnLogin
            // 
            this.btnLogin.BackColor = System.Drawing.Color.LightGreen;
            this.btnLogin.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnLogin.Location = new System.Drawing.Point(393, 496);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(104, 35);
            this.btnLogin.TabIndex = 4;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.BackColor = System.Drawing.Color.Snow;
            this.btnLogout.Enabled = false;
            this.btnLogout.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnLogout.Location = new System.Drawing.Point(283, 496);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(104, 35);
            this.btnLogout.TabIndex = 5;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // comboBoxServer
            // 
            this.comboBoxServer.BackColor = System.Drawing.Color.White;
            this.comboBoxServer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxServer.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.comboBoxServer.FormattingEnabled = true;
            this.comboBoxServer.Items.AddRange(new object[] {
            "Yandex",
            "Gmail"});
            this.comboBoxServer.Location = new System.Drawing.Point(174, 28);
            this.comboBoxServer.Name = "comboBoxServer";
            this.comboBoxServer.Size = new System.Drawing.Size(134, 29);
            this.comboBoxServer.TabIndex = 3;
            this.comboBoxServer.SelectedIndexChanged += new System.EventHandler(this.comboBoxServer_SelectedIndexChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(130)))), ((int)(((byte)(222)))));
            this.pictureBox1.ErrorImage = null;
            this.pictureBox1.Image = global::MailClient.Properties.Resources.logo;
            this.pictureBox1.Location = new System.Drawing.Point(-10, -51);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(676, 151);
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBoxLoading
            // 
            this.pictureBoxLoading.Image = global::MailClient.Properties.Resources.loading;
            this.pictureBoxLoading.Location = new System.Drawing.Point(16, 491);
            this.pictureBoxLoading.Name = "pictureBoxLoading";
            this.pictureBoxLoading.Size = new System.Drawing.Size(42, 40);
            this.pictureBoxLoading.TabIndex = 6;
            this.pictureBoxLoading.TabStop = false;
            this.pictureBoxLoading.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBoxSmtpPort);
            this.groupBox1.Controls.Add(this.textBoxSmtp);
            this.groupBox1.Controls.Add(this.labelSmtpPort);
            this.groupBox1.Controls.Add(this.labelSmtp);
            this.groupBox1.Controls.Add(this.textBoxImapPort);
            this.groupBox1.Controls.Add(this.textBoxImap);
            this.groupBox1.Controls.Add(this.labelImapPort);
            this.groupBox1.Controls.Add(this.labelImap);
            this.groupBox1.Controls.Add(this.radioButtonCustomServer);
            this.groupBox1.Controls.Add(this.radioButtonChoosePreset);
            this.groupBox1.Controls.Add(this.comboBoxServer);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.groupBox1.Location = new System.Drawing.Point(16, 213);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(481, 272);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Server Information";
            // 
            // textBoxSmtpPort
            // 
            this.textBoxSmtpPort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxSmtpPort.Enabled = false;
            this.textBoxSmtpPort.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBoxSmtpPort.Location = new System.Drawing.Point(144, 219);
            this.textBoxSmtpPort.Name = "textBoxSmtpPort";
            this.textBoxSmtpPort.Size = new System.Drawing.Size(85, 29);
            this.textBoxSmtpPort.TabIndex = 19;
            // 
            // textBoxSmtp
            // 
            this.textBoxSmtp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxSmtp.Enabled = false;
            this.textBoxSmtp.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBoxSmtp.Location = new System.Drawing.Point(144, 184);
            this.textBoxSmtp.Name = "textBoxSmtp";
            this.textBoxSmtp.Size = new System.Drawing.Size(321, 29);
            this.textBoxSmtp.TabIndex = 18;
            // 
            // labelSmtpPort
            // 
            this.labelSmtpPort.AutoSize = true;
            this.labelSmtpPort.Enabled = false;
            this.labelSmtpPort.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelSmtpPort.Location = new System.Drawing.Point(54, 221);
            this.labelSmtpPort.Name = "labelSmtpPort";
            this.labelSmtpPort.Size = new System.Drawing.Size(85, 21);
            this.labelSmtpPort.TabIndex = 17;
            this.labelSmtpPort.Text = "SMTP Port:";
            // 
            // labelSmtp
            // 
            this.labelSmtp.AutoSize = true;
            this.labelSmtp.Enabled = false;
            this.labelSmtp.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelSmtp.Location = new System.Drawing.Point(54, 184);
            this.labelSmtp.Name = "labelSmtp";
            this.labelSmtp.Size = new System.Drawing.Size(53, 21);
            this.labelSmtp.TabIndex = 16;
            this.labelSmtp.Text = "SMTP:";
            // 
            // textBoxImapPort
            // 
            this.textBoxImapPort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxImapPort.Enabled = false;
            this.textBoxImapPort.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBoxImapPort.Location = new System.Drawing.Point(144, 145);
            this.textBoxImapPort.Name = "textBoxImapPort";
            this.textBoxImapPort.Size = new System.Drawing.Size(85, 29);
            this.textBoxImapPort.TabIndex = 15;
            // 
            // textBoxImap
            // 
            this.textBoxImap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxImap.Enabled = false;
            this.textBoxImap.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBoxImap.Location = new System.Drawing.Point(144, 110);
            this.textBoxImap.Name = "textBoxImap";
            this.textBoxImap.Size = new System.Drawing.Size(321, 29);
            this.textBoxImap.TabIndex = 14;
            // 
            // labelImapPort
            // 
            this.labelImapPort.AutoSize = true;
            this.labelImapPort.Enabled = false;
            this.labelImapPort.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelImapPort.Location = new System.Drawing.Point(54, 147);
            this.labelImapPort.Name = "labelImapPort";
            this.labelImapPort.Size = new System.Drawing.Size(82, 21);
            this.labelImapPort.TabIndex = 13;
            this.labelImapPort.Text = "IMAP Port:";
            // 
            // labelImap
            // 
            this.labelImap.AutoSize = true;
            this.labelImap.Enabled = false;
            this.labelImap.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelImap.Location = new System.Drawing.Point(54, 110);
            this.labelImap.Name = "labelImap";
            this.labelImap.Size = new System.Drawing.Size(50, 21);
            this.labelImap.TabIndex = 12;
            this.labelImap.Text = "IMAP:";
            // 
            // radioButtonCustomServer
            // 
            this.radioButtonCustomServer.AutoSize = true;
            this.radioButtonCustomServer.Location = new System.Drawing.Point(22, 70);
            this.radioButtonCustomServer.Name = "radioButtonCustomServer";
            this.radioButtonCustomServer.Size = new System.Drawing.Size(82, 25);
            this.radioButtonCustomServer.TabIndex = 10;
            this.radioButtonCustomServer.Text = "Custom";
            this.radioButtonCustomServer.UseVisualStyleBackColor = true;
            this.radioButtonCustomServer.CheckedChanged += new System.EventHandler(this.radioButtonCustomServer_CheckedChanged);
            // 
            // radioButtonChoosePreset
            // 
            this.radioButtonChoosePreset.AutoSize = true;
            this.radioButtonChoosePreset.Checked = true;
            this.radioButtonChoosePreset.Location = new System.Drawing.Point(22, 28);
            this.radioButtonChoosePreset.Name = "radioButtonChoosePreset";
            this.radioButtonChoosePreset.Size = new System.Drawing.Size(127, 25);
            this.radioButtonChoosePreset.TabIndex = 9;
            this.radioButtonChoosePreset.TabStop = true;
            this.radioButtonChoosePreset.Text = "Choose preset";
            this.radioButtonChoosePreset.UseVisualStyleBackColor = true;
            // 
            // LoginForm
            // 
            this.AcceptButton = this.btnLogin;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(508, 543);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pictureBoxLoading);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.tbPassword);
            this.Controls.Add(this.tbUsername);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(510, 355);
            this.Name = "LoginForm";
            this.Text = "Login";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LoginForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLoading)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbUsername;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.PictureBox pictureBoxLoading;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ComboBox comboBoxServer;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButtonCustomServer;
        private System.Windows.Forms.RadioButton radioButtonChoosePreset;
        private System.Windows.Forms.TextBox textBoxSmtpPort;
        private System.Windows.Forms.TextBox textBoxSmtp;
        private System.Windows.Forms.Label labelSmtpPort;
        private System.Windows.Forms.Label labelSmtp;
        private System.Windows.Forms.TextBox textBoxImapPort;
        private System.Windows.Forms.TextBox textBoxImap;
        private System.Windows.Forms.Label labelImapPort;
        private System.Windows.Forms.Label labelImap;
    }
}

