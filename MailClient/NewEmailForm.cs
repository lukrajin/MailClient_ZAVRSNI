using Bogus;
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
        private string _host;
        private int _port;

        public NewEmailForm(MailClientForm parentForm)
        {
            _parentForm = parentForm;

            _username = _parentForm.MailReceiver.Login;
            _password = _parentForm.MailReceiver.Password;
            InitializeComponent();
      
            if (!_username.Contains("@"))
            {
                if (parentForm.MailReceiver.Host.Contains("yandex"))
                {
                    tbFrom.Text = _username + "@yandex.ru";
                }
                else
                {
                    tbFrom.Text = _username + "@gmail.com";
                }
            }
            else
            {
                tbFrom.Text = _username;
            }

            if (parentForm.MailReceiver.Host.Contains("yandex"))
            {
                _host = ServerInfo.Yandex.SmtpServer;
                _port = ServerInfo.Yandex.SmtpPort;
            }
            else
            {
                _host = ServerInfo.Gmail.SmtpServer;
                _port = ServerInfo.Gmail.SmtpPort;
            }

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
                    var mailSender = new MailSender(_host, _port, true, _username, _password);
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

        private void buttonFillRandom_Click(object sender, EventArgs e)
        {
            Faker faker = new Bogus.Faker();
            tbBody.Text = faker.Lorem.Sentences(new Random().Next(10, 50));
            tbSubject.Text = faker.Lorem.Sentence();
        }
    }
}