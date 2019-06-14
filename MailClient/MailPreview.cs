using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MailClient
{
    public partial class MailPreview : Form
    {
        private MailClientForm _parentForm;

        public MailPreview(MailClientForm parentForm, string from, string to, string subject, string body, string time, EmailFolder folderType)
        {
            InitializeComponent();

            _parentForm = parentForm;
            tbFrom.Text = from;
            tbTo.Text = to;
            tbSubject.Text = subject;
            tbBody.Text = body;

            if(folderType == EmailFolder.Inbox)
                lbArrivalTime.Text = "Arrival Time: " + time;
            else if (folderType == EmailFolder.SentEmails)
                lbArrivalTime.Text = "Sent Time: " + time;
        }

        private void buttonForward_Click(object sender, EventArgs e)
        {

            var newEmailForm = new NewEmailForm(_parentForm, _parentForm.MailReceiver.Login, _parentForm.MailReceiver.Password);
            newEmailForm.LoadFromMailPreview("", "FWD: " + tbSubject.Text, tbBody.Text);
            newEmailForm.StartPosition = FormStartPosition.Manual;
            newEmailForm.Location = this.Location;
            this.Close();
            newEmailForm.Show();

        }

        private void buttonReply_Click(object sender, EventArgs e)
        {
            var newEmailForm = new NewEmailForm(_parentForm, _parentForm.MailReceiver.Login, _parentForm.MailReceiver.Password);
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
