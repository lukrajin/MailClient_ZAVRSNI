using MimeKit;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MailClient
{
    public partial class NewEmailForm : Form
    {
        private string _username;
        private string _password;
        private MailClientForm _parentForm;

        public NewEmailForm(MailClientForm parentForm, string username, string password)
        {
            _username = username;
            _password = password;
            InitializeComponent();
          
            if (!username.Contains("@"))
            {
                if (parentForm.MailReceiver.Host.Contains("yandex"))
                    tbFrom.Text = username + "@yandex.ru";
                else
                {
                    tbFrom.Text = username + "@gmail.com";
                }
            }
            else
            {
                tbFrom.Text = username;
            }

            _parentForm = parentForm;
        }
        public void LoadFromMailPreview(string to, string subject, string body)
        {
            tbTo.Text = to;
            tbSubject.Text = subject;
            tbBody.Text = body;
        }
        private void btnSend_Click(object sender, EventArgs e)
        {
            btnSend.Enabled = false;
            btnSend.Text = "Sending";
            pictureBoxLoading.Visible = true;
            Task.Run(() =>
            {
                try
                {
                    var mailSender = new MailSender("smtp.yandex.com", 465, true, _username, _password);
                    mailSender.Connect();
                    var sentEmail = mailSender.Send(tbFrom.Text, tbTo.Text, tbSubject.Text, tbBody.Text);

                    mailSender.Disconnect();

                    _parentForm.MailReceiver.SentFolder.Append(sentEmail);
                }
                catch (Exception ex)
                {
                    _parentForm.Invoke((Action)(() => MessageBox.Show(ex.Message, "Sending failed")));
                    this.Invoke((Action)(() => pictureBoxLoading.Visible = false));
                }

                this.Invoke((Action)(() => pictureBoxLoading.Visible = false));
                this.Invoke((Action)(() => this.Close()));
            });
        }

        private void tbTo_TextChanged(object sender, EventArgs e)
        {
            if(tbTo.Text != "")
            {
                btnSend.Enabled = true;
            }
            else
            {
                btnSend.Enabled = false;
            }
        }
    }
}