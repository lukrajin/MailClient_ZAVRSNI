using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MailClient
{
    public partial class LoginForm : Form
    {
        private MailClientForm _parentForm;
        public LoginForm(MailClientForm parentForm)
        {
            InitializeComponent();
            _parentForm = parentForm;
            comboBoxServer.Text = "Gmail";
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string server;
            int port;

            if(comboBoxServer.Text == "Gmail")
            {
                server = ServerInfo.Gmail.ImapServer;
                port = ServerInfo.Gmail.ImapPort;
            }
            else
            {
                server = ServerInfo.Yandex.ImapServer;
                port = ServerInfo.Yandex.ImapPort;
            }

            btnLogin.Enabled = false;
            tbUsername.Enabled = false;
            tbPassword.Enabled = false;
            comboBoxServer.Enabled = false;
            pictureBoxLoading.Visible = true;

            Task.Run(() =>
                    {
                        try
                        {
                            var mailReceiver = new MailReceiver(server, port, true, tbUsername.Text, tbPassword.Text, _parentForm);
                            mailReceiver.Connect();

                            this.Invoke((Action)(() => this.Hide()));

                            if (!mailReceiver.Login.Contains("@"))
                            {
                                if (mailReceiver.Host.Contains("yandex"))
                                    _parentForm.toolStripStatusLabel.Text = "Logged In - " + mailReceiver.Login + "@yandex.ru";
                                else
                                {
                                    _parentForm.toolStripStatusLabel.Text = "Logged In - " + mailReceiver.Login + "@gmail.com";
                                }
                            }
                            else
                            {
                                _parentForm.toolStripStatusLabel.Text = "Logged In - " + mailReceiver.Login;
                            }

                            _parentForm.Invoke((Action)(() => _parentForm.panelLoading.Visible = true));
                           
                            _parentForm.MailReceiver = mailReceiver;
                            _parentForm.ReceivedEmails = mailReceiver.GetInboxEmailList();
                            _parentForm.SentEmails = mailReceiver.GetSentEmailList();
                            _parentForm.LoadEmails(EmailView.Inbox);

                            this.Invoke((Action)(() => btnLogout.Enabled = true));

                  
                            _parentForm.Invoke((Action)(() => _parentForm.SetEnableToolbarButtons(true)));
                            _parentForm.Invoke((Action)(() => _parentForm.panelLoading.Visible = false));
                            this.Invoke((Action)(() => pictureBoxLoading.Visible = false));
                        }
                        catch (Exception ex)
                        {
                            this.Invoke((Action)(() => btnLogin.Enabled = true));
                            this.Invoke((Action)(() => tbUsername.Enabled = true));
                            this.Invoke((Action)(() => tbPassword.Enabled = true));
                            this.Invoke((Action)(() => comboBoxServer.Enabled = true));
                            this.Invoke((Action)(() => pictureBoxLoading.Visible = false));
                            this.Invoke((Action)(() => MessageBox.Show(ex.Message, "Login Failed")));
                            _parentForm.Invoke((Action)(() => _parentForm.panelLoading.Visible = false));
                        }
                    });
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            btnLogout.Enabled = false;
            Task.Run(() => {
                _parentForm.Invoke((Action)(() => _parentForm.SetEnableToolbarButtons(false)));

                _parentForm.MailReceiver.Disconnect();

                this.Invoke((Action)(() => tbUsername.Enabled = true));
                this.Invoke((Action)(() => tbPassword.Enabled = true));
                this.Invoke((Action)(() => btnLogin.Enabled = true));
                this.Invoke((Action)(() => comboBoxServer.Enabled = true));
                _parentForm.Invoke((Action)(() => _parentForm.toolStripStatusLabel.Text = "Logged Out"));
                _parentForm.Invoke((Action)(() => _parentForm.dataGridViewEmails.DataSource = null));
            });

        }

        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }
    }
}