using System;
using System.Text.RegularExpressions;
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
            var serverInfo = new ServerInfo(ServerInfo.ServerPreset.Gmail);

            if (radioButtonChoosePreset.Checked)
            {
                if (comboBoxServer.Text == "Yandex")
                {
                    serverInfo = new ServerInfo(ServerInfo.ServerPreset.Yandex);
                }
            }
            else if (radioButtonCustomServer.Checked)
            {
                if(!Regex.IsMatch(textBoxImapPort.Text, @"^\d+$") || !Regex.IsMatch(textBoxSmtpPort.Text, @"^\d+$"))
                {
                    MessageBox.Show("IMAP and SMTP ports must be numberic value.", "Failed");
                    return;
                }

                serverInfo = new ServerInfo(textBoxImap.Text, 
                    Convert.ToInt32(textBoxImapPort.Text), textBoxSmtp.Text, Convert.ToInt32(textBoxSmtpPort.Text));
            }

            btnLogin.Enabled = false;
            tbUsername.Enabled = false;
            tbPassword.Enabled = false;
            groupBox1.Enabled = false;
            pictureBoxLoading.Visible = true;

            Task.Run(() =>
                    {
                        try
                        {
                            var mailReceiver = new MailReceiver(serverInfo.ImapServer, serverInfo.ImapPort, true, tbUsername.Text, tbPassword.Text, _parentForm);
                            mailReceiver.Connect();

                            this.Invoke((Action)(() => this.Hide()));

                            if (!mailReceiver.Login.Contains("@"))
                            {
                                _parentForm.Invoke((Action)(() => 
                                    _parentForm.toolStripStatusLabel.Text = "Logged In - " + serverInfo.LoginSuffix));
          
                            }
                            else
                            {
                                _parentForm.Invoke((Action)(() =>
                                     _parentForm.toolStripStatusLabel.Text = "Logged In - " + mailReceiver.Login));
                            }

                            _parentForm.ServerInfo = serverInfo;
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
                            this.Invoke((Action)(() => groupBox1.Enabled = true));
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
                this.Invoke((Action)(() => groupBox1.Enabled = true));
                _parentForm.Invoke((Action)(() => _parentForm.toolStripStatusLabel.Text = "Logged Out"));
                _parentForm.Invoke((Action)(() => _parentForm.dataGridViewEmails.DataSource = null));
            });

        }

        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void radioButtonCustomServer_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButtonCustomServer.Checked)
            {
                labelSmtp.Enabled = true;
                labelSmtpPort.Enabled = true;
                labelImap.Enabled = true;
                labelImapPort.Enabled = true;

                textBoxSmtp.Enabled = true;
                textBoxSmtpPort.Enabled = true;
                textBoxImap.Enabled = true;
                textBoxImapPort.Enabled = true;

                textBoxSmtp.Text = "";
                textBoxSmtpPort.Text = "";
                textBoxImap.Text = "";
                textBoxImapPort.Text = "";
            }
            else
            {
                labelSmtp.Enabled = false;
                labelSmtpPort.Enabled = false;
                labelImap.Enabled = false;
                labelImapPort.Enabled = false;

                textBoxSmtp.Enabled = false;
                textBoxSmtpPort.Enabled = false;
                textBoxImap.Enabled = false;
                textBoxImapPort.Enabled = false;

                comboBoxServer_SelectedIndexChanged(sender, e);

            }
        }

        private void comboBoxServer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxServer.Text == "Gmail")
            {
                var serverInfo = new ServerInfo(ServerInfo.ServerPreset.Gmail);

                textBoxImap.Text = serverInfo.ImapServer;
                textBoxImapPort.Text = serverInfo.ImapPort.ToString();
                textBoxSmtp.Text = serverInfo.SmtpServer;
                textBoxSmtpPort.Text = serverInfo.SmtpPort.ToString();
            }
            else if (comboBoxServer.Text == "Yandex")
            {
                var serverInfo = new ServerInfo(ServerInfo.ServerPreset.Yandex);

                textBoxImap.Text = serverInfo.ImapServer;
                textBoxImapPort.Text = serverInfo.ImapPort.ToString();
                textBoxSmtp.Text = serverInfo.SmtpServer;
                textBoxSmtpPort.Text = serverInfo.SmtpPort.ToString();
            }
        }
    }
}