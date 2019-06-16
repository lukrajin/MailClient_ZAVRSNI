namespace MailClient
{
    partial class MailClientForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MailClientForm));
            this.dataGridViewEmails = new System.Windows.Forms.DataGridView();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.panelLoading = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.contextMenuStripEmail = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moveToToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripFolder = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.renameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripFolderList = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.toolStripButtonLogin = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonNewEmail = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonInbox = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonSent = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonRefresh = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonCollection = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonImportExport = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonSearch = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEmails)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panelLoading.SuspendLayout();
            this.contextMenuStripEmail.SuspendLayout();
            this.contextMenuStripFolder.SuspendLayout();
            this.contextMenuStripFolderList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewEmails
            // 
            this.dataGridViewEmails.AllowUserToAddRows = false;
            this.dataGridViewEmails.AllowUserToDeleteRows = false;
            this.dataGridViewEmails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewEmails.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dataGridViewEmails.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(239)))), ((int)(((byte)(241)))));
            this.dataGridViewEmails.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dataGridViewEmails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewEmails.GridColor = System.Drawing.Color.LightGray;
            this.dataGridViewEmails.Location = new System.Drawing.Point(0, 43);
            this.dataGridViewEmails.MinimumSize = new System.Drawing.Size(917, 459);
            this.dataGridViewEmails.Name = "dataGridViewEmails";
            this.dataGridViewEmails.ReadOnly = true;
            this.dataGridViewEmails.RowHeadersVisible = false;
            this.dataGridViewEmails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewEmails.Size = new System.Drawing.Size(1102, 490);
            this.dataGridViewEmails.TabIndex = 0;
            this.dataGridViewEmails.VirtualMode = true;
            this.dataGridViewEmails.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewEmails_CellMouseEnter);
            this.dataGridViewEmails.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dataGridViewEmails_MouseDoubleClick);
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(132)))), ((int)(((byte)(226)))));
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 533);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1102, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.ForeColor = System.Drawing.Color.White;
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(67, 17);
            this.toolStripStatusLabel.Text = "Logged Off";
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.BackColor = System.Drawing.Color.White;
            this.toolStrip1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(30, 30);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonLogin,
            this.toolStripButtonNewEmail,
            this.toolStripButtonInbox,
            this.toolStripButtonSent,
            this.toolStripButtonRefresh,
            this.toolStripButtonCollection,
            this.toolStripButtonImportExport,
            this.toolStripButtonSearch});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1102, 40);
            this.toolStrip1.TabIndex = 4;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // panelLoading
            // 
            this.panelLoading.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelLoading.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(239)))), ((int)(((byte)(241)))));
            this.panelLoading.Controls.Add(this.pictureBox1);
            this.panelLoading.Controls.Add(this.label1);
            this.panelLoading.Location = new System.Drawing.Point(0, 43);
            this.panelLoading.Name = "panelLoading";
            this.panelLoading.Size = new System.Drawing.Size(1102, 490);
            this.panelLoading.TabIndex = 5;
            this.panelLoading.Visible = false;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(489, 405);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 30);
            this.label1.TabIndex = 0;
            this.label1.Text = "Loading...";
            // 
            // contextMenuStripEmail
            // 
            this.contextMenuStripEmail.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewToolStripMenuItem,
            this.moveToToolStripMenuItem1,
            this.copyToToolStripMenuItem,
            this.deleteToolStripMenuItem});
            this.contextMenuStripEmail.Name = "contextMenuStripEmail";
            this.contextMenuStripEmail.Size = new System.Drawing.Size(121, 92);
            this.contextMenuStripEmail.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStripEmail_Opening);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.viewToolStripMenuItem.Text = "View";
            this.viewToolStripMenuItem.Click += new System.EventHandler(this.viewToolStripMenuItem_Click);
            // 
            // moveToToolStripMenuItem1
            // 
            this.moveToToolStripMenuItem1.Name = "moveToToolStripMenuItem1";
            this.moveToToolStripMenuItem1.Size = new System.Drawing.Size(120, 22);
            this.moveToToolStripMenuItem1.Text = "Move To";
            this.moveToToolStripMenuItem1.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.moveToToolStripMenuItem1_DropDownItemClicked);
            // 
            // copyToToolStripMenuItem
            // 
            this.copyToToolStripMenuItem.Name = "copyToToolStripMenuItem";
            this.copyToToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.copyToToolStripMenuItem.Text = "Copy To";
            this.copyToToolStripMenuItem.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.copyToToolStripMenuItem_DropDownItemClicked);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // contextMenuStripFolder
            // 
            this.contextMenuStripFolder.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.renameToolStripMenuItem,
            this.deleteToolStripMenuItem1});
            this.contextMenuStripFolder.Name = "contextMenuStripFolder";
            this.contextMenuStripFolder.Size = new System.Drawing.Size(118, 70);
            // 
            // renameToolStripMenuItem
            // 
            this.renameToolStripMenuItem.Name = "renameToolStripMenuItem";
            this.renameToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.renameToolStripMenuItem.Text = "Rename";
            this.renameToolStripMenuItem.Click += new System.EventHandler(this.renameToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem1
            // 
            this.deleteToolStripMenuItem1.Name = "deleteToolStripMenuItem1";
            this.deleteToolStripMenuItem1.Size = new System.Drawing.Size(117, 22);
            this.deleteToolStripMenuItem1.Text = "Delete";
            this.deleteToolStripMenuItem1.Click += new System.EventHandler(this.deleteToolStripMenuItem1_Click);
            // 
            // contextMenuStripFolderList
            // 
            this.contextMenuStripFolderList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addFolderToolStripMenuItem});
            this.contextMenuStripFolderList.Name = "contextMenuStripFolderList";
            this.contextMenuStripFolderList.Size = new System.Drawing.Size(133, 26);
            // 
            // addFolderToolStripMenuItem
            // 
            this.addFolderToolStripMenuItem.Name = "addFolderToolStripMenuItem";
            this.addFolderToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.addFolderToolStripMenuItem.Text = "Add Folder";
            this.addFolderToolStripMenuItem.Click += new System.EventHandler(this.addFolderToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(239)))), ((int)(((byte)(241)))));
            this.pictureBox1.ErrorImage = null;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(334, 81);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(412, 309);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // toolStripButtonLogin
            // 
            this.toolStripButtonLogin.AutoSize = false;
            this.toolStripButtonLogin.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonLogin.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonLogin.Image")));
            this.toolStripButtonLogin.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonLogin.Name = "toolStripButtonLogin";
            this.toolStripButtonLogin.Size = new System.Drawing.Size(38, 38);
            this.toolStripButtonLogin.Text = "Login";
            this.toolStripButtonLogin.Click += new System.EventHandler(this.toolStripButtonLogin_Click);
            // 
            // toolStripButtonNewEmail
            // 
            this.toolStripButtonNewEmail.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonNewEmail.Enabled = false;
            this.toolStripButtonNewEmail.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonNewEmail.Image")));
            this.toolStripButtonNewEmail.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonNewEmail.Name = "toolStripButtonNewEmail";
            this.toolStripButtonNewEmail.Size = new System.Drawing.Size(34, 37);
            this.toolStripButtonNewEmail.Text = "New Email";
            this.toolStripButtonNewEmail.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripButtonInbox
            // 
            this.toolStripButtonInbox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonInbox.Enabled = false;
            this.toolStripButtonInbox.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonInbox.Image")));
            this.toolStripButtonInbox.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonInbox.Name = "toolStripButtonInbox";
            this.toolStripButtonInbox.Size = new System.Drawing.Size(34, 37);
            this.toolStripButtonInbox.Text = "Inbox";
            this.toolStripButtonInbox.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // toolStripButtonSent
            // 
            this.toolStripButtonSent.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonSent.Enabled = false;
            this.toolStripButtonSent.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonSent.Image")));
            this.toolStripButtonSent.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSent.Name = "toolStripButtonSent";
            this.toolStripButtonSent.Size = new System.Drawing.Size(34, 37);
            this.toolStripButtonSent.Text = "Sent Email";
            this.toolStripButtonSent.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // toolStripButtonRefresh
            // 
            this.toolStripButtonRefresh.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButtonRefresh.AutoSize = false;
            this.toolStripButtonRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonRefresh.Enabled = false;
            this.toolStripButtonRefresh.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonRefresh.Image")));
            this.toolStripButtonRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRefresh.Name = "toolStripButtonRefresh";
            this.toolStripButtonRefresh.Size = new System.Drawing.Size(38, 38);
            this.toolStripButtonRefresh.Text = "Refresh";
            this.toolStripButtonRefresh.Click += new System.EventHandler(this.toolStripButtonRefresh_Click);
            // 
            // toolStripButtonCollection
            // 
            this.toolStripButtonCollection.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonCollection.Enabled = false;
            this.toolStripButtonCollection.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonCollection.Image")));
            this.toolStripButtonCollection.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonCollection.Name = "toolStripButtonCollection";
            this.toolStripButtonCollection.Size = new System.Drawing.Size(34, 37);
            this.toolStripButtonCollection.Text = "Collection";
            this.toolStripButtonCollection.Click += new System.EventHandler(this.toolStripButtonCollection_Click);
            // 
            // toolStripButtonImportExport
            // 
            this.toolStripButtonImportExport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonImportExport.Enabled = false;
            this.toolStripButtonImportExport.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonImportExport.Image")));
            this.toolStripButtonImportExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonImportExport.Name = "toolStripButtonImportExport";
            this.toolStripButtonImportExport.Size = new System.Drawing.Size(34, 37);
            this.toolStripButtonImportExport.Text = "Import/Export Emails";
            this.toolStripButtonImportExport.Click += new System.EventHandler(this.toolStripButtonImportExport_Click);
            // 
            // toolStripButtonSearch
            // 
            this.toolStripButtonSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonSearch.Enabled = false;
            this.toolStripButtonSearch.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonSearch.Image")));
            this.toolStripButtonSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSearch.Name = "toolStripButtonSearch";
            this.toolStripButtonSearch.Size = new System.Drawing.Size(34, 37);
            this.toolStripButtonSearch.Text = "Search";
            this.toolStripButtonSearch.Click += new System.EventHandler(this.toolStripButtonSearch_Click);
            // 
            // MailClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1102, 555);
            this.Controls.Add(this.panelLoading);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.dataGridViewEmails);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(957, 554);
            this.Name = "MailClientForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "MailClient";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MailClientForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEmails)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panelLoading.ResumeLayout(false);
            this.panelLoading.PerformLayout();
            this.contextMenuStripEmail.ResumeLayout(false);
            this.contextMenuStripFolder.ResumeLayout(false);
            this.contextMenuStripFolderList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.DataGridView dataGridViewEmails;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButtonLogin;
        public System.Windows.Forms.ToolStripButton toolStripButtonRefresh;
        private System.Windows.Forms.ToolStripButton toolStripButtonNewEmail;
        private System.Windows.Forms.ToolStripButton toolStripButtonSent;
        private System.Windows.Forms.ToolStripButton toolStripButtonInbox;
        public System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ToolStripButton toolStripButtonCollection;
        public System.Windows.Forms.Panel panelLoading;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripEmail;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButtonImportExport;
        private System.Windows.Forms.ToolStripMenuItem copyToToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripFolder;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripFolderList;
        private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem addFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moveToToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButtonSearch;
    }
}