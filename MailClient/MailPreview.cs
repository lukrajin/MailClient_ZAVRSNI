using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MailClient
{
    public partial class MailPreview : Form
    {
        private MailClientForm _parentForm;
        private Dictionary<string, MimeEntity> _attachments;

        public MailPreview(MailClientForm parentForm, SentEmail sentEmail)
        {
            InitializeComponent();

            _parentForm = parentForm;
            tbFrom.Text = sentEmail.From;
            tbTo.Text = sentEmail.To;
            tbSubject.Text = sentEmail.Subject;

            pictureBoxLoading.Visible = true;

            Task.Run(() =>
            {
                var message = _parentForm.MailReceiver.GetEmail(EmailType.SentEmails, sentEmail.UniqueId);
                GetAttachments(message);

                if (!string.IsNullOrEmpty(message.HtmlBody))
                {
                    this.Invoke((Action)(() => webBrowserBody.DocumentText = message.HtmlBody));
                    this.Invoke((Action)(() => webBrowserBody.Visible = true));
                }
                else if (!string.IsNullOrEmpty(message.TextBody))
                {
                    this.Invoke((Action)(() => tbBody.Text = message.TextBody));
                }

                this.Invoke((Action)(() => pictureBoxLoading.Visible = false));
                this.Invoke((Action)(() => pictureBoxLoading.Enabled = false));

                _parentForm.MailReceiver.SetMessageSeen(EmailType.SentEmails, sentEmail.UniqueId);
            });

            lbDate.Text = "Sent Time: " + sentEmail.SentTime;
        }

        public MailPreview(MailClientForm parentForm, Models.CollectionEmail collectionEmail)
        {
            InitializeComponent();

            _parentForm = parentForm;

            tbFrom.Text = collectionEmail.From;
            tbTo.Text = collectionEmail.To;
            tbSubject.Text = collectionEmail.Subject;
            tbBody.Text = collectionEmail.TextBody;

            if (!string.IsNullOrEmpty(collectionEmail.HtmlBody))
            {
                webBrowserBody.DocumentText = collectionEmail.HtmlBody;
                webBrowserBody.Visible = true;
            }
            else if (!string.IsNullOrEmpty(collectionEmail.TextBody))
            {
                tbBody.Text = collectionEmail.TextBody;
            }

            lbDate.Text = "Date: " + collectionEmail.Date;
        }

        public MailPreview(MailClientForm parentForm, InboxEmail inboxEmail)
        {
            InitializeComponent();

            _parentForm = parentForm;
            tbFrom.Text = inboxEmail.From;
            tbTo.Text = inboxEmail.To;
            tbSubject.Text = inboxEmail.Subject;

            pictureBoxLoading.Visible = true;

            Task.Run(() =>
            {
                var message = _parentForm.MailReceiver.GetEmail(EmailType.Inbox, inboxEmail.UniqueId);
                GetAttachments(message);

                if (!string.IsNullOrEmpty(message.HtmlBody))
                {
                    this.Invoke((Action)(() => webBrowserBody.DocumentText = message.HtmlBody));
                    this.Invoke((Action)(() => webBrowserBody.Visible = true));
                }
                else if (!string.IsNullOrEmpty(message.TextBody))
                {
                    this.Invoke((Action)(() => tbBody.Text = message.TextBody));
                }

                this.Invoke((Action)(() => pictureBoxLoading.Visible = false));
                this.Invoke((Action)(() => pictureBoxLoading.Enabled = false));

                _parentForm.MailReceiver.SetMessageSeen(EmailType.Inbox, inboxEmail.UniqueId);
            });

            lbDate.Text = "Arrival Time: " + inboxEmail.ArrivalTime;
        }

        private void buttonForward_Click(object sender, EventArgs e)
        {
            var newEmailForm = new NewEmailForm(_parentForm);
            newEmailForm.LoadFromMailPreview("", "FWD: " + tbSubject.Text, tbBody.Text);
            newEmailForm.StartPosition = FormStartPosition.Manual;
            newEmailForm.Location = this.Location;
            this.Close();
            newEmailForm.Show();
        }

        private void buttonReply_Click(object sender, EventArgs e)
        {
            var newEmailForm = new NewEmailForm(_parentForm);
            var body = tbBody.Text;
            newEmailForm.LoadFromMailPreview(
                tbTo.Text, "Reply: " + tbSubject.Text, "---ORIGINAL MESSAGE----\n" + body + "---ORIGINAL MESSAGE----\n");

            newEmailForm.StartPosition = FormStartPosition.Manual;
            newEmailForm.Location = this.Location;
            this.Close();
            newEmailForm.Show();
        }

        private void GetAttachments(MimeMessage message)
        {
            _attachments = new Dictionary<string, MimeEntity>();
            foreach (MimeEntity attachment in message.Attachments)
            {
                var fileName = attachment.ContentDisposition?.FileName ?? attachment.ContentType.Name;

                if (!_attachments.ContainsKey(fileName))
                {
                    _attachments.Add(fileName, attachment);

                    LinkLabel linkLabel = new LinkLabel();
                    linkLabel.Text = fileName;
                    linkLabel.Click += LinkLabel_Click;

                    if (panel1.Controls.Count != 0)
                    {
                        var lastLink=panel1.Controls[panel1.Controls.Count - 1];
     
                        linkLabel.Location = new System.Drawing.Point(
                            lastLink.Location.X + lastLink.Width, lastLink.Location.Y);

                        this.Invoke((Action)(() => panel1.Controls.Add(linkLabel)));
                    }
                    else
                    {
                        this.Invoke((Action)(() => panel1.Controls.Add(linkLabel)));
                    }
          
                }
            }
        }

        private void LinkLabel_Click(object sender, EventArgs e)
        {
            var filename = ((LinkLabel)sender).Text;
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = filename;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                SaveAttachment(saveFileDialog.FileName, filename);
            }
        }

        public void SaveAttachment(string path, string filename)
        {
            var attachment = _attachments[filename];
            using (var stream = File.Create(path))
            {
                if (attachment is MessagePart)
                {
                    var rfc822 = (MessagePart)attachment;

                    rfc822.Message.WriteTo(stream);
                }
                else
                {
                    var part = (MimePart)attachment;

                    part.Content.DecodeTo(stream);
                }
            }
        }
    }
}