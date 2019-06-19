using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MailClient
{
    public partial class MailPreview : Form
    {
        private MailClientForm _parentForm;

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
    }
}