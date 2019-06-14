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
        public ConcurrentBag<SentEmail> SentEmails { get; set; }
        public ConcurrentBag<InboxEmail> ReceivedEmails { get; set; }

        public ConcurrentBag<CollectionEmail> EmailCollection { get; set; }

        private LoginForm loginForm;
        private MailPreview mailPreview;
        private NewEmailForm newEmailForm;
        private DataGridViewRow previousHoveredRow;
        private bool buttonRefreshClicked;

        private EmailFolder OpenedFolder { get; set; }

        public MailClientForm()
        {
            InitializeComponent();
            ImportExportTool = new ImportExportTool(this);

            EmailCollection = ImportExportTool.LoadCollection(true);
            if (EmailCollection == null)
                EmailCollection = new ConcurrentBag<CollectionEmail>();

            Type dgvType = dataGridViewEmails.GetType();
            PropertyInfo pi = dgvType.GetProperty("DoubleBuffered",
                BindingFlags.Instance | BindingFlags.NonPublic);

            pi.SetValue(dataGridViewEmails, true, null);
            dataGridViewEmails.MouseDown += dataGridViewEmails_MouseDown;
            dataGridViewEmails.RowTemplate.Height = 30;
            dataGridViewEmails.RowTemplate.Resizable = DataGridViewTriState.False;
            dataGridViewEmails.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewEmails.ColumnHeadersHeight = 30;

            dataGridViewEmails.DefaultCellStyle.SelectionBackColor = Color.LightSkyBlue;
            dataGridViewEmails.DefaultCellStyle.SelectionForeColor = Color.Black;
            dataGridViewEmails.DefaultCellStyle.Font = new Font("Segoe UI", 15F, GraphicsUnit.Pixel);
            loginForm = new LoginForm(this);
        }

        public void LoadEmails(EmailFolder folderType)
        {
            lock (this)
            {
                OpenedFolder = folderType;

                DataTable messages = new DataTable("Messages");

                if (folderType == EmailFolder.Inbox)
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
                    messages.Columns.Add("Arrival Time", typeof(string));
                    messages.Columns.Add("From", typeof(string));
                    messages.Columns.Add("To", typeof(string));
                    messages.Columns.Add("Subject", typeof(string));
                    messages.Columns.Add("Body", typeof(string));

                    foreach (var email in ReceivedEmails)
                    {
                        messages.Rows.Add(email.Id, email.ArrivalTime, email.From, email.To,
                            email.Subject, CreateBodyPreview(email.Body, 30)
                            );
                    }
                }
                else if(folderType == EmailFolder.SentEmails)
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
                    messages.Columns.Add("Sent Time", typeof(string));
                    messages.Columns.Add("From", typeof(string));
                    messages.Columns.Add("To", typeof(string));
                    messages.Columns.Add("Subject", typeof(string));
                    messages.Columns.Add("Body", typeof(string));

                    foreach (var email in SentEmails)
                    {
                        messages.Rows.Add(email.Id, email.SentTime, email.From, email.To,
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
                    messages.Columns.Add("Date", typeof(string));
                    messages.Columns.Add("Folder Type", typeof(string));
                    messages.Columns.Add("From", typeof(string));
                    messages.Columns.Add("To", typeof(string));
                    messages.Columns.Add("Subject", typeof(string));
                    messages.Columns.Add("Body", typeof(string));

                    foreach (var email in EmailCollection)
                    {
                        messages.Rows.Add(email.Id, email.Date, email.FolderType.ToString(), email.From, email.To,
                            email.Subject, CreateBodyPreview(email.Body, 30));
                    }
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
                    if (OpenedFolder == EmailFolder.Inbox)
                    {
                        ReceivedEmails = MailReceiver.GetInboxEmailList();
                    }
                    else if(OpenedFolder == EmailFolder.SentEmails)
                    {
                        SentEmails = MailReceiver.GetSentEmailList();
                    }

                    LoadEmails(OpenedFolder);
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
            var hti = dataGridViewEmails.HitTest(e.X, e.Y);

            var hitRow = dataGridViewEmails.Rows[hti.RowIndex];

            dataGridViewEmails.ClearSelection();

            hitRow.Selected = true;

            PreviewEmail(hitRow);
        }
        private void PreviewEmail(DataGridViewRow hitRow)
        {
            if (OpenedFolder == EmailFolder.Inbox)
            {
                var selectedEmail = ReceivedEmails.FirstOrDefault(x => x.Id == ((string)hitRow.Cells[0].Value));

                mailPreview = new MailPreview(this,
                     selectedEmail.From
                    , selectedEmail.To,
                    selectedEmail.Subject, selectedEmail.Body, selectedEmail.ArrivalTime, OpenedFolder);

                mailPreview.StartPosition = FormStartPosition.Manual;
                mailPreview.Location = MousePosition;
                mailPreview.Show();
            }
            else if (OpenedFolder == EmailFolder.SentEmails)
            {
                var selectedEmail = SentEmails.FirstOrDefault(x => x.Id == ((string)hitRow.Cells[0].Value));

                mailPreview = new MailPreview(this,
                    selectedEmail.From
                    , selectedEmail.To,
                     selectedEmail.Subject, selectedEmail.Body, selectedEmail.SentTime, OpenedFolder);

                mailPreview.StartPosition = FormStartPosition.Manual;
                mailPreview.Location = MousePosition;
                mailPreview.Show();
            }
            else
            {
                var selectedEmail = EmailCollection.FirstOrDefault(x => x.Id == ((string)hitRow.Cells[0].Value));

                mailPreview = new MailPreview(this,
                    selectedEmail.From
                    , selectedEmail.To,
                     selectedEmail.Subject, selectedEmail.Body, selectedEmail.Date, selectedEmail.FolderType);

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
                LoadEmails(EmailFolder.SentEmails);
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (MailReceiver != null)
                LoadEmails(EmailFolder.Inbox);
        }

        private void dataGridViewEmails_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (previousHoveredRow != null)
                previousHoveredRow.DefaultCellStyle.BackColor = Color.White;

            if (e.RowIndex > 0)
            {
                dataGridViewEmails.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.AliceBlue;
                previousHoveredRow = dataGridViewEmails.Rows[e.RowIndex];
            }
        }
        private void dataGridViewEmails_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var hti = dataGridViewEmails.HitTest(e.X, e.Y);

                var hitRow = dataGridViewEmails.Rows[hti.RowIndex];

                if (!hitRow.Selected)
                {
                    dataGridViewEmails.ClearSelection();

                    hitRow.Selected = true;
                }

                contextMenuStripEmail.Show(MousePosition);
            }
        }
        private void MailClientForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MailReceiver != null)
            {
                ImportExportTool.Export(EmailFolder.EmailCollection, true);
            }
        }

        private void toolStripButtonCollection_Click(object sender, EventArgs e)
        {
            if (MailReceiver != null)
                LoadEmails(EmailFolder.EmailCollection);
        }

        private void viewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach(DataGridViewRow hitRow in dataGridViewEmails.SelectedRows)
                PreviewEmail(hitRow);
        }

        private void copyToCollectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
 
            foreach(DataGridViewRow row in dataGridViewEmails.SelectedRows)
            {

                if (OpenedFolder == EmailFolder.Inbox)
                {
                    var selectedEmail = ReceivedEmails.FirstOrDefault(x => x.Id == ((string)row.Cells[0].Value));
                    CollectionEmail collectionEmail = new CollectionEmail
                    {
                        Id = selectedEmail.Id,
                        From = selectedEmail.From,
                        To=selectedEmail.To,
                        Subject=selectedEmail.Subject,
                        Body=selectedEmail.Body,
                        Date=selectedEmail.ArrivalTime,
                        FolderType = EmailFolder.Inbox
                    };

                    EmailCollection.Add(collectionEmail);
                }
                else if (OpenedFolder == EmailFolder.SentEmails)
                {
                    var selectedEmail = SentEmails.FirstOrDefault(x => x.Id == ((string)row.Cells[0].Value));
                    CollectionEmail collectionEmail = new CollectionEmail
                    {
                        Id = selectedEmail.Id,
                        From = selectedEmail.From,
                        To = selectedEmail.To,
                        Subject = selectedEmail.Subject,
                        Body = selectedEmail.Body,
                        Date = selectedEmail.SentTime,
                        FolderType = EmailFolder.SentEmails
                    };

                    EmailCollection.Add(collectionEmail);
                }
               
            }
        }

        private void contextMenuStripEmail_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(OpenedFolder == EmailFolder.EmailCollection)
            {
                copyToCollectionToolStripMenuItem.Visible = false;
                deleteToolStripMenuItem.Visible = true;
            }
            else
            {
                copyToCollectionToolStripMenuItem.Visible = true;
                deleteToolStripMenuItem.Visible = false;
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach(DataGridViewRow row in dataGridViewEmails.SelectedRows)
            {
                dataGridViewEmails.Rows.RemoveAt(row.Index);
            }
        }

        private void toolStripButtonImportExport_Click(object sender, EventArgs e)
        {
            ImportExportEmailsForm importExportEmailsForm = new ImportExportEmailsForm();
            importExportEmailsForm.StartPosition = FormStartPosition.Manual;
            importExportEmailsForm.Location = MousePosition;
            importExportEmailsForm.Show();
        }
    }

    public enum EmailFolder
    { Inbox, SentEmails, EmailCollection };
}