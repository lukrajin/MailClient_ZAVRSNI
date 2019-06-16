using System;
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
            tbBody.Text = sentEmail.Body;

            if (!string.IsNullOrEmpty(sentEmail.ToMimeMessage().HtmlBody))
            {
                webBrowserBody.DocumentText = sentEmail.ToMimeMessage().HtmlBody;
                webBrowserBody.Visible = true;
            }

            lbDate.Text = "Sent Time: " + sentEmail.SentTime;

            _parentForm.MailReceiver.SetMessageSeen(EmailType.SentEmails, sentEmail.UniqueId);
        }
        public MailPreview(MailClientForm parentForm, CollectionEmail collectionEmail)
        {
            InitializeComponent();

            _parentForm = parentForm;
            tbFrom.Text = collectionEmail.From;
            tbTo.Text = collectionEmail.To;
            tbSubject.Text = collectionEmail.Subject;
            tbBody.Text = collectionEmail.Body;

            lbDate.Text = "Date: " + collectionEmail.Date;
        }
        public MailPreview(MailClientForm parentForm, InboxEmail inboxEmail)
        {
            InitializeComponent();

            _parentForm = parentForm;
            tbFrom.Text = inboxEmail.From;
            tbTo.Text = inboxEmail.To;
            tbSubject.Text = inboxEmail.Subject;
            tbBody.Text = inboxEmail.Body;

            if (!string.IsNullOrEmpty(inboxEmail.ToMimeMessage().HtmlBody))
            {
                webBrowserBody.DocumentText = inboxEmail.ToMimeMessage().HtmlBody;
                webBrowserBody.Visible = true;
            }

            lbDate.Text = "Arrival Time: " + inboxEmail.ArrivalTime;
            _parentForm.MailReceiver.SetMessageSeen(EmailType.Inbox, inboxEmail.UniqueId);

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
