using MimeKit;
using System;
using System.Collections.Concurrent;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MailClient
{
    public partial class MailClientForm : Form
    {
        private NewEmailForm _newEmailForm;
        private ImportExportTool ImportExportTool { get; set; }
        public MailReceiver MailReceiver { get; set; }
        private MimeMessage NewEmail;
        public ConcurrentDictionary<string, SentEmail> SentEmails { get; set; }
        public ConcurrentDictionary<string, InboxEmail> ReceivedEmails { get; set; }

        public ConcurrentDictionary<string, CollectionEmail> EmailCollection { get; set; }
        public ConcurrentDictionary<string, CustomFolder> FolderList { get; set; }
        private LoginForm loginForm;
        private MailPreview mailPreview;
        private NewEmailForm newEmailForm;
        private DataGridViewRow previousHoveredRow;
        private bool buttonRefreshClicked;

        private EmailView CurrentView { get; set; }
        public string OpenedCustomFolderName { get; private set; }

        public MailClientForm()
        {
            InitializeComponent();
            ImportExportTool = new ImportExportTool(this);

            EmailCollection = ImportExportTool.LoadCollection(true);
            if (EmailCollection == null)
                EmailCollection = new ConcurrentDictionary<string, CollectionEmail>();

            FolderList = ImportExportTool.LoadFolderList();
            if (FolderList == null)
            {
                FolderList = new ConcurrentDictionary<string, CustomFolder>();
            }

            Type dgvType = dataGridViewEmails.GetType();
            PropertyInfo pi = dgvType.GetProperty("DoubleBuffered",
                BindingFlags.Instance | BindingFlags.NonPublic);

            pi.SetValue(dataGridViewEmails, true, null);
            dataGridViewEmails.MouseDown += dataGridViewEmails_MouseDown;
            dataGridViewEmails.RowTemplate.Height = 45;
            dataGridViewEmails.RowTemplate.Resizable = DataGridViewTriState.False;
            dataGridViewEmails.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewEmails.ColumnHeadersHeight = 30;
            dataGridViewEmails.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 12F, GraphicsUnit.Pixel);

            dataGridViewEmails.DefaultCellStyle.SelectionBackColor = Color.LightSkyBlue;
            dataGridViewEmails.DefaultCellStyle.SelectionForeColor = Color.Black;
            dataGridViewEmails.DefaultCellStyle.Font = new Font("Segoe UI", 15F, GraphicsUnit.Pixel);
            loginForm = new LoginForm(this);
        }

        public void LoadEmails(EmailView folderType)
        {
            lock (this)
            {
                CurrentView = folderType;

                DataTable messages = new DataTable("Messages");

                if (folderType == EmailView.Inbox)
                {
                    if (this.InvokeRequired)
                    {
                        this.Invoke((Action)(() => this.Text = "MailClient - Inbox"));
                    }
                    else
                    {
                        this.Text = "MailClient - Inbox";
                    }

                    messages.Columns.Add("Id", typeof(string));
                    messages.Columns.Add("  ", typeof(Image));
                    messages.Columns.Add("Arrival Time", typeof(DateTime));
                    messages.Columns.Add("From", typeof(string));
                    messages.Columns.Add("To", typeof(string));
                    messages.Columns.Add("Subject", typeof(string));
                    messages.Columns.Add("Body", typeof(string));
                    var emails = ReceivedEmails.Values.OrderByDescending(x => x.ArrivalTime);

                    foreach (var email in emails)
                    {
                        Bitmap image = MailClient.Properties.Resources.icons8_secured_letter_40;
                        if (email.IsRead)
                            image = MailClient.Properties.Resources.read_message_40px;

                        messages.Rows.Add(email.Id, image
                             ,
                            email.ArrivalTime, email.From, email.To,
                            email.Subject, CreateBodyPreview(email.Body, 30)
                            );
                    }
                }
                else if (folderType == EmailView.SentEmails)
                {
                    if (this.InvokeRequired)
                    {
                        this.Invoke((Action)(() => this.Text = "MailClient - Sent Emails"));
                    }
                    else
                    {
                        this.Text = "MailClient - Sent Emails";
                    }

                    messages.Columns.Add("Id", typeof(string));
                    messages.Columns.Add("  ", typeof(Image));
                    messages.Columns.Add("Sent Time", typeof(DateTime));
                    messages.Columns.Add("From", typeof(string));
                    messages.Columns.Add("To", typeof(string));
                    messages.Columns.Add("Subject", typeof(string));
                    messages.Columns.Add("Body", typeof(string));
                    var emails = SentEmails.Values.OrderByDescending(x => x.SentTime);

                    foreach (var email in emails)
                    {
                        Bitmap image = MailClient.Properties.Resources.icons8_secured_letter_40;
                        if (email.IsRead)
                            image = MailClient.Properties.Resources.read_message_40px;

                        messages.Rows.Add(email.Id,
                            image,
                            email.SentTime, email.From, email.To,
                            email.Subject, CreateBodyPreview(email.Body, 30));
                    }
                }
                else
                {
                    if (this.InvokeRequired)
                    {
                        this.Invoke((Action)(() => this.Text = "MailClient - Collection"));
                    }
                    else
                    {
                        this.Text = "MailClient - Collection";
                    }

                    messages.Columns.Add("Id", typeof(string));
                    messages.Columns.Add("  ", typeof(Image));
                    messages.Columns.Add("Folder name", typeof(string));
                    messages.Columns.Add("Item Count", typeof(string));
                    messages.Columns.Add("Date Modified", typeof(DateTime));
                    messages.Columns.Add("Date Created", typeof(DateTime));

                    foreach (var folder in FolderList.Values)
                    {
                        messages.Rows.Add(folder.FolderName,
                            MailClient.Properties.Resources.icons8_opened_folder_40, folder.FolderName,
                            folder.ItemCount, folder.DateModified,
                            folder.DateCreated);
                    }
                }

                if (dataGridViewEmails.InvokeRequired)
                {
                    dataGridViewEmails.Invoke((Action)(() => dataGridViewEmails.DataSource = messages));
                    dataGridViewEmails.Invoke((Action)(() => dataGridViewEmails.Columns[0].Visible = false));
                    dataGridViewEmails.Invoke((Action)(() => dataGridViewEmails.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.None));
                    dataGridViewEmails.Invoke((Action)(() => dataGridViewEmails.Columns[1].Width = 50));
                }
                else
                {
                    dataGridViewEmails.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    dataGridViewEmails.Columns[1].Width = 50;
                    dataGridViewEmails.DataSource = messages;
                    dataGridViewEmails.Columns[0].Visible = false;
                }
            }
        }

        public void LoadCustomFolderEmails(string foldername)
        {
            OpenedCustomFolderName = foldername;
            DataTable messages = new DataTable("Messages");

            var currentCustomFolderItemList = GetCustomFolderList(foldername);
            var currentFolder = FolderList[foldername];

            if (this.InvokeRequired)
            {
                this.Invoke((Action)(() => this.Text = "MailClient - " + currentFolder.FolderName));
            }
            else
            {
                this.Text = "MailClient - " + currentFolder.FolderName;
            }

            messages.Columns.Add("Id", typeof(string));
            messages.Columns.Add("Date", typeof(string));
            messages.Columns.Add("Email Type", typeof(string));
            messages.Columns.Add("From", typeof(string));
            messages.Columns.Add("To", typeof(string));
            messages.Columns.Add("Subject", typeof(string));
            messages.Columns.Add("Body", typeof(string));

            foreach (var email in currentCustomFolderItemList.Values)
            {
                messages.Rows.Add(email.Id, email.Date, email.EmailType.ToString(), email.From, email.To,
                    email.Subject, CreateBodyPreview(email.Body, 30));
            }

            if (dataGridViewEmails.InvokeRequired)
            {
                dataGridViewEmails.Invoke((Action)(() => dataGridViewEmails.DataSource = messages));
                dataGridViewEmails.Invoke((Action)(() => dataGridViewEmails.Columns[0].Visible = false));
            }
            else
            {
                dataGridViewEmails.DataSource = messages;
                dataGridViewEmails.Columns[0].Visible = false;
            }
        }

        private ConcurrentDictionary<string, CollectionEmail> GetCustomFolderList(string foldername)
        {
            var customFolderList = new ConcurrentDictionary<string, CollectionEmail>();

            CurrentView = EmailView.CustomFolder;

            foreach (var email in EmailCollection.Values)
            {
                if (email.CustomFolderName == foldername)
                {
                    customFolderList.TryAdd(email.Id, email);
                }
            }

            return customFolderList;
        }

        internal void SetEnableToolbarButtons(bool enabled)
        {
            toolStripButtonInbox.Enabled = enabled;
            toolStripButtonSent.Enabled = enabled;
            toolStripButtonRefresh.Enabled = enabled;
            toolStripButtonNewEmail.Enabled = enabled;
            toolStripButtonCollection.Enabled = enabled;
            toolStripButtonImportExport.Enabled = enabled;
        }

        private int GetColumnIndexByName(string name)
        {
            foreach (DataGridViewColumn column in dataGridViewEmails.Columns)
            {
                if (column.Name == name)
                {
                    return column.Index;
                }
            }

            return -1;
        }

        private void toolStripButtonRefresh_Click(object sender, EventArgs e)
        {
            if (buttonRefreshClicked == true)
                return;

            if (MailReceiver != null)
            {
                buttonRefreshClicked = true;
                toolStripButtonRefresh.Image = MailClient.Properties.Resources.loading;
                Task.Run(() =>
                {
                    if (CurrentView == EmailView.Inbox)
                    {
                        ReceivedEmails = MailReceiver.GetInboxEmailList();
                    }
                    else if (CurrentView == EmailView.SentEmails)
                    {
                        SentEmails = MailReceiver.GetSentEmailList();
                    }
                    if (CurrentView == EmailView.CustomFolder)
                    {
                        LoadCustomFolderEmails(OpenedCustomFolderName);
                        toolStripButtonRefresh.Owner.Invoke((Action)(() => toolStripButtonRefresh.Image = MailClient.Properties.Resources.Refresh));
                        buttonRefreshClicked = false;
                        return;
                    }
                    LoadEmails(CurrentView);
                    toolStripButtonRefresh.Owner.Invoke((Action)(() => toolStripButtonRefresh.Image = MailClient.Properties.Resources.Refresh));
                    buttonRefreshClicked = false;
                });
            }
        }

        private void toolStripButtonLogin_Click(object sender, EventArgs e)
        {
            if (loginForm == null)
                loginForm = new LoginForm(this);

            loginForm.StartPosition = FormStartPosition.Manual;
            loginForm.Location = MousePosition;

            loginForm.Show();
        }

        private void dataGridViewEmails_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (buttonRefreshClicked)
                return;

            var hti = dataGridViewEmails.HitTest(e.X, e.Y);

            if (hti.RowIndex == -1)
                return;

            var hitRow = dataGridViewEmails.Rows[hti.RowIndex];

            dataGridViewEmails.ClearSelection();

            hitRow.Selected = true;

            if (CurrentView != EmailView.FolderList)
                PreviewEmail(hitRow);
            else
                LoadCustomFolderEmails((string)hitRow.Cells[0].Value);
        }

        private void PreviewEmail(DataGridViewRow hitRow)
        {
            if (CurrentView == EmailView.Inbox)
            {
                var selectedEmail = ReceivedEmails[(string)hitRow.Cells[0].Value];

                mailPreview = new MailPreview(this,
                     selectedEmail);

                mailPreview.StartPosition = FormStartPosition.Manual;
                mailPreview.Location = MousePosition;
                mailPreview.Show();
                hitRow.Cells[1].Value = MailClient.Properties.Resources.read_message_40px;
            }
            else if (CurrentView == EmailView.SentEmails)
            {
                var selectedEmail = SentEmails[(string)hitRow.Cells[0].Value];

                mailPreview = new MailPreview(this,
                    selectedEmail);

                mailPreview.StartPosition = FormStartPosition.Manual;
                mailPreview.Location = MousePosition;
                mailPreview.Show();
                hitRow.Cells[1].Value = MailClient.Properties.Resources.read_message_40px;
            }
            else
            {
                var selectedEmail = EmailCollection[(string)hitRow.Cells[0].Value];

                mailPreview = new MailPreview(this, selectedEmail);

                mailPreview.StartPosition = FormStartPosition.Manual;
                mailPreview.Location = MousePosition;
                mailPreview.Show();
            }
        }

        public static string CreateBodyPreview(string body, int maxLength)
        {
            if (string.IsNullOrEmpty(body))
                return body;

            var shortBody = body.Substring(0, Math.Min(body.Length, maxLength));

            if (shortBody.Count() != body.Count())
                return shortBody.Substring(0, shortBody.Length - 3) + "...";
            else
                return body;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (MailReceiver != null)
            {
                newEmailForm = new NewEmailForm(this, MailReceiver.Login, MailReceiver.Password);
                newEmailForm.StartPosition = FormStartPosition.Manual;
                newEmailForm.Location = MousePosition;
                newEmailForm.Show();
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (MailReceiver != null)
            {
                LoadEmails(EmailView.SentEmails);
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (MailReceiver != null)
                LoadEmails(EmailView.Inbox);
        }

        private void dataGridViewEmails_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (previousHoveredRow != null)
                previousHoveredRow.DefaultCellStyle.BackColor = Color.White;

            if (e.RowIndex >= 0)
            {
                dataGridViewEmails.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.AliceBlue;
                previousHoveredRow = dataGridViewEmails.Rows[e.RowIndex];
            }
        }

        private void dataGridViewEmails_MouseDown(object sender, MouseEventArgs e)
        {
            if (buttonRefreshClicked)
                return;

            if (dataGridViewEmails.DataSource == null)
                return;

            if (e.Button == MouseButtons.Right)
            {
                var hti = dataGridViewEmails.HitTest(e.X, e.Y);

                DataGridViewRow hitRow = new DataGridViewRow();

                if (hti.RowIndex != -1)
                {
                    hitRow = dataGridViewEmails.Rows[hti.RowIndex];

                    if (!hitRow.Selected)
                    {
                        dataGridViewEmails.ClearSelection();

                        hitRow.Selected = true;
                    }

                    contextMenuStripEmail.Show(MousePosition);
                }

                if (CurrentView == EmailView.FolderList)
                {
                    if (hti.RowIndex == -1)
                    {
                        contextMenuStripFolderList.Show(MousePosition);
                    }
                    else
                    {
                        contextMenuStripFolder.Show(MousePosition);
                    }
                }
            }
        }

        private void MailClientForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MailReceiver != null)
            {
                ImportExportTool.ExportEmails(EmailType.CollectionEmail, true);
                ImportExportTool.ExportFolderList();
            }
        }

        private void toolStripButtonCollection_Click(object sender, EventArgs e)
        {
            if (MailReceiver != null)
                LoadEmails(EmailView.FolderList);
        }

        private void viewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow hitRow in dataGridViewEmails.SelectedRows)
                PreviewEmail(hitRow);
        }

        private void contextMenuStripEmail_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            copyToToolStripMenuItem.DropDownItems.Clear();
            moveToToolStripMenuItem1.DropDownItems.Clear();


            foreach (var folder in FolderList)
            {
                if (CurrentView == EmailView.CustomFolder)
                {
                    if (folder.Value.FolderName != OpenedCustomFolderName)
                    {
                        moveToToolStripMenuItem1.DropDownItems.Add(folder.Value.FolderName);
                    }
                }
                else
                {
                    copyToToolStripMenuItem.DropDownItems.Add(folder.Value.FolderName);
                    moveToToolStripMenuItem1.DropDownItems.Add(folder.Value.FolderName);
                }
            }

            if (CurrentView == EmailView.CustomFolder)
            {
                copyToToolStripMenuItem.DropDownItems.Add("Inbox");
                moveToToolStripMenuItem1.DropDownItems.Add("Inbox");
                copyToToolStripMenuItem.DropDownItems.Add("Sent Emails");
                moveToToolStripMenuItem1.DropDownItems.Add("Sent Emails");
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                if (CurrentView == EmailView.CustomFolder)
                {
                    foreach (DataGridViewRow row in dataGridViewEmails.SelectedRows)
                    {
                        CollectionEmail collectionEmail;
                        var selectedEmail = EmailCollection[(string)row.Cells[0].Value];
                        --FolderList[OpenedCustomFolderName].ItemCount;
                        EmailCollection.TryRemove(selectedEmail.Id, out collectionEmail);
                    }
                    toolStripButtonRefresh.PerformClick();
                }
                else
                {
                    foreach (DataGridViewRow row in dataGridViewEmails.SelectedRows)
                    {
                        if (CurrentView == EmailView.Inbox)
                        {
                            InboxEmail inboxEmail;
                            var selectedEmail = ReceivedEmails[(string)row.Cells[0].Value];
                            MailReceiver.DeleteEmail(EmailType.Inbox, selectedEmail.UniqueId);
                            ReceivedEmails.TryRemove(selectedEmail.Id, out inboxEmail);
                        }
                        else
                        {
                            SentEmail sentEmail;
                            var selectedEmail = SentEmails[(string)row.Cells[0].Value];
                            MailReceiver.DeleteEmail(EmailType.SentEmails, selectedEmail.UniqueId);
                            SentEmails.TryRemove(selectedEmail.Id, out sentEmail);
                        }
                    }
                    toolStripButtonRefresh.PerformClick();
                }
            });
        }

        private void toolStripButtonImportExport_Click(object sender, EventArgs e)
        {
            ImportExportEmailsForm importExportEmailsForm = new ImportExportEmailsForm(this);
            importExportEmailsForm.StartPosition = FormStartPosition.Manual;
            importExportEmailsForm.Location = MousePosition;
            importExportEmailsForm.Show();
        }

        private void addFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var newFolderForm = new FolderEdit(this);
            newFolderForm.StartPosition = FormStartPosition.Manual;
            newFolderForm.Location = MousePosition;
            newFolderForm.Show();
        }

        private void renameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selectedFolder = FolderList[(string)dataGridViewEmails.SelectedRows[0].Cells[0].Value];
            var editFolderForm = new FolderEdit(this, selectedFolder.FolderName);

            editFolderForm.StartPosition = FormStartPosition.Manual;
            editFolderForm.Location = MousePosition;
            editFolderForm.Show();
        }

        private void copyToToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            Task.Run(() =>
            {
                foreach (DataGridViewRow row in dataGridViewEmails.SelectedRows)
                {
                    if (CurrentView == EmailView.Inbox)
                    {
                        var selectedEmail = ReceivedEmails[(string)row.Cells[0].Value];
                        var selectedFolderId = FolderList.FirstOrDefault(x => x.Value.FolderName == e.ClickedItem.Text).Value.FolderName;
                        var selectedFolder = FolderList[selectedFolderId];

                        if (EmailCollection.ContainsKey(selectedEmail.Id))
                        {
                            MessageBox.Show("This email already exists in collection.", "Copying failed");
                            return;
                        }

                        ++selectedFolder.ItemCount;
                        EmailCollection.TryAdd(selectedEmail.Id,
                            new CollectionEmail
                            {
                                Id = selectedEmail.Id,
                                From = selectedEmail.From,
                                To = selectedEmail.To,
                                Subject = selectedEmail.Subject,
                                Body = selectedEmail.Body,
                                Date = selectedEmail.ArrivalTime,
                                CustomFolderName = selectedFolderId,
                                EmailType = EmailType.Inbox,
                                UniqueId = selectedEmail.UniqueId
                            });
                    }
                    else if (CurrentView == EmailView.SentEmails)
                    {
                        var selectedEmail = SentEmails[(string)row.Cells[0].Value];
                        var selectedFolderId = FolderList.FirstOrDefault(x => x.Value.FolderName == e.ClickedItem.Text).Value.FolderName;
                        var selectedFolder = FolderList[selectedFolderId];

                        ++selectedFolder.ItemCount;
                        EmailCollection.TryAdd(selectedEmail.Id,
                            new CollectionEmail
                            {
                                Id = selectedEmail.Id,
                                From = selectedEmail.From,
                                To = selectedEmail.To,
                                Subject = selectedEmail.Subject,
                                Body = selectedEmail.Body,
                                Date = selectedEmail.SentTime,
                                CustomFolderName = selectedFolderId,
                                EmailType = EmailType.SentEmails,
                                UniqueId = selectedEmail.UniqueId
                            });
                    }
                    else
                    {
                        if (e.ClickedItem.Text == "Sent Emails")
                        {
                            var selectedEmail = EmailCollection[(string)row.Cells[0].Value];
                            var message = new MimeMessage();

                            message.From.Add(new MailboxAddress(selectedEmail.From));
                            message.To.Add(new MailboxAddress(selectedEmail.To));
                            message.Subject = selectedEmail.Subject;
                            message.Body = new TextPart("plain") { Text = selectedEmail.Body };
                            message.Date = selectedEmail.Date;

                            MailReceiver.AddMessageToFolder(EmailType.SentEmails, message);
                        }
                        else if (e.ClickedItem.Text == "Inbox")
                        {
                            var selectedEmail = EmailCollection[(string)row.Cells[0].Value];
                            var message = new MimeMessage();

                            message.From.Add(new MailboxAddress(selectedEmail.From));
                            message.To.Add(new MailboxAddress(selectedEmail.To));
                            message.Subject = selectedEmail.Subject;
                            message.Body = new TextPart("plain") { Text = selectedEmail.Body };
                            message.Date = selectedEmail.Date;

                            MailReceiver.AddMessageToFolder(EmailType.Inbox, message);
                        }
                    }
                }

                toolStripButtonRefresh.PerformClick();
            });
        }

        private void deleteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridViewEmails.SelectedRows)
            {
                CustomFolder folder;
                FolderList.TryRemove((string)row.Cells[0].Value, out folder);

                foreach (var email in EmailCollection.Values)
                {
                    CollectionEmail collectionEmail;
                    if (email.CustomFolderName == (string)row.Cells[0].Value)
                        EmailCollection.TryRemove(email.Id, out collectionEmail);
                }
                dataGridViewEmails.Rows.RemoveAt(row.Index);
            }
        }

        private void moveToToolStripMenuItem1_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            Task.Run(() =>
            {
                if (CurrentView == EmailView.CustomFolder)
                {
                    foreach (DataGridViewRow row in dataGridViewEmails.SelectedRows)
                    {
                        var selectedEmail = GetCustomFolderList(OpenedCustomFolderName)[(string)row.Cells[0].Value];

                        var oldFolder = FolderList[selectedEmail.CustomFolderName];
                        --oldFolder.ItemCount;

                        if (e.ClickedItem.Text == "Sent Emails")
                        {
                            var message = new MimeMessage();

                            message.From.Add(new MailboxAddress(selectedEmail.From));
                            message.To.Add(new MailboxAddress(selectedEmail.To));
                            message.Subject = selectedEmail.Subject;
                            message.Body = new TextPart("plain") { Text = selectedEmail.Body };
                            message.Date = selectedEmail.Date;

                            MailReceiver.AddMessageToFolder(EmailType.SentEmails, message);

                            CollectionEmail collectionEmail;
                            EmailCollection.TryRemove(selectedEmail.Id, out collectionEmail);
                        }
                        else if (e.ClickedItem.Text == "Inbox")
                        {
                            var message = new MimeMessage();

                            message.From.Add(new MailboxAddress(selectedEmail.From));
                            message.To.Add(new MailboxAddress(selectedEmail.To));
                            message.Subject = selectedEmail.Subject;
                            message.Body = new TextPart("plain") { Text = selectedEmail.Body };
                            message.Date = selectedEmail.Date;

                            MailReceiver.AddMessageToFolder(EmailType.Inbox, message);

                            CollectionEmail collectionEmail;
                            EmailCollection.TryRemove(selectedEmail.Id, out collectionEmail);
                        }
                        else
                        {
                            var selectedFolderId = FolderList.FirstOrDefault(x => x.Value.FolderName == e.ClickedItem.Text).Value.FolderName;
                            var selectedFolder = FolderList[selectedFolderId];

                            ++selectedFolder.ItemCount;
                            selectedEmail.CustomFolderName = selectedFolderId;
                        }
                    }
                }
                else
                {
                    foreach (DataGridViewRow row in dataGridViewEmails.SelectedRows)
                    {
                        if (CurrentView == EmailView.Inbox)
                        {
                            var selectedEmail = ReceivedEmails[(string)row.Cells[0].Value];

                            var selectedFolderName = FolderList.FirstOrDefault(x => x.Value.FolderName == e.ClickedItem.Text).Value.FolderName;
                            var selectedFolder = FolderList[selectedFolderName];

                            if (EmailCollection.ContainsKey(selectedEmail.Id))
                            {
                                MessageBox.Show("This email already exists in collection.", "Moving failed");
                                return;
                            }

                            ++selectedFolder.ItemCount;
                            EmailCollection.TryAdd(selectedEmail.Id,
                                new CollectionEmail
                                {
                                    Id = selectedEmail.Id,
                                    From = selectedEmail.From,
                                    To = selectedEmail.To,
                                    Subject = selectedEmail.Subject,
                                    Body = selectedEmail.Body,
                                    Date = selectedEmail.ArrivalTime,
                                    CustomFolderName = selectedFolderName,
                                    EmailType = EmailType.Inbox,
                                    UniqueId = selectedEmail.UniqueId
                                });
                            MailReceiver.DeleteEmail(EmailType.Inbox, selectedEmail.UniqueId);
                        }
                        else if (CurrentView == EmailView.SentEmails)
                        {
                            var selectedEmail = SentEmails[(string)row.Cells[0].Value];
                            var selectedFolderName = FolderList.FirstOrDefault(x => x.Value.FolderName == e.ClickedItem.Text).Value.FolderName;
                            var selectedFolder = FolderList[selectedFolderName];

                            if (EmailCollection.ContainsKey(selectedEmail.Id))
                            {
                                MessageBox.Show("This email already exists in collection.", "Moving failed");
                                return;
                            }

                            ++selectedFolder.ItemCount;
                            EmailCollection.TryAdd(selectedEmail.Id,
                                new CollectionEmail
                                {
                                    Id = selectedEmail.Id,
                                    From = selectedEmail.From,
                                    To = selectedEmail.To,
                                    Subject = selectedEmail.Subject,
                                    Body = selectedEmail.Body,
                                    Date = selectedEmail.SentTime,
                                    CustomFolderName = selectedFolderName,
                                    EmailType = EmailType.Inbox,
                                    UniqueId = selectedEmail.UniqueId
                                });
                            MailReceiver.DeleteEmail(EmailType.SentEmails, selectedEmail.UniqueId);
                        }
                    }
                }

                toolStripButtonRefresh.PerformClick();
            });
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selectedRow = dataGridViewEmails.Rows[dataGridViewEmails.SelectedRows[0].Index];

            LoadCustomFolderEmails((string)selectedRow.Cells[0].Value);
        }
    }

    public enum EmailType
    { Inbox, SentEmails, CollectionEmail };

    public enum EmailView { Inbox, SentEmails, CustomFolder, FolderList }
}