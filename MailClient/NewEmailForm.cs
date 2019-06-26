using Bogus;
using MimeKit;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MailClient
{
    public partial class NewEmailForm : Form
    {
        private string _username;
        private string _password;
        private MailClientForm _parentForm;

        public string Attachments { get; private set; }

        public NewEmailForm(MailClientForm parentForm)
        {
            _parentForm = parentForm;
  
            _username = _parentForm.MailReceiver.Login;
            _password = _parentForm.MailReceiver.Password;
            InitializeComponent();
      
            if (!_username.Contains("@"))
            {
                tbFrom.Text = _username + _parentForm.ServerInfo.LoginSuffix;
            }
            else
            {
                tbFrom.Text = _username;
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
                    var mailSender = new MailSender(_parentForm.ServerInfo.SmtpServer, _parentForm.ServerInfo.SmtpPort
                        , true, _username, _password);
                    mailSender.Connect();
                    var sentEmail = mailSender.Send(tbFrom.Text, tbTo.Text, tbSubject.Text, tbBody.Text, Attachments);

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

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;

            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {
                for(int i=0; i<openFileDialog.FileNames.Length;++i)
                {
                    if (i == 0)
                    {
                        if (tbAttach.Text != "")
                        {
                            tbAttach.Text += ";";
                            Attachments += ";";
                        }
                    }

                    if ((i+1) == openFileDialog.FileNames.Length)
                    {
                        Attachments += openFileDialog.FileNames[i];
                        tbAttach.Text += Path.GetFileName(openFileDialog.FileNames[i]);
                    }
                    else
                    {
                        Attachments += openFileDialog.FileNames[i] + ";";
                        tbAttach.Text += Path.GetFileName(openFileDialog.FileNames[i]) + ";";
                    }
                }
            }
        }
    }
}