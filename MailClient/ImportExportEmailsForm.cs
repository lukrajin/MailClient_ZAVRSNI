using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MailClient
{
    public partial class ImportExportEmailsForm : Form
    {
        private MailClientForm _parentForm;
        private ImportExportTool _importExportTool;

        public ImportExportEmailsForm(MailClientForm parentForm)
        {
            InitializeComponent();
            _parentForm = parentForm;

            foreach (var folder in _parentForm.FolderList.Values)
            {
                comboBoxImportTo.Items.Add(folder.FolderName);
                comboBoxExportFrom.Items.Add(folder.FolderName);
            }

            comboBoxImportTo.Items.Add("Inbox");
            comboBoxImportTo.Items.Add("Sent Emails");

            comboBoxExportFrom.Items.Add("Inbox");
            comboBoxExportFrom.Items.Add("Sent Emails");

            if (parentForm.ServerInfo.Preset != ServerInfo.ServerPreset.Gmail)
            {
                comboBoxImportFrom.Items.Add("Gmail");
            }
            if (parentForm.ServerInfo.Preset != ServerInfo.ServerPreset.Yandex)
            {
                comboBoxImportFrom.Items.Add("Yandex");
            }

            comboBoxImportFrom.Items.Add("Custom Server");
            comboBoxImportFrom.Items.Add("Browse on PC");
        }

        private void comboBoxImportFrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxImportFrom.Text == "Gmail" || comboBoxImportFrom.Text == "Yandex")
            {
                labelFolder.Visible = true;
                comboBoxServerFolder.Visible = true;
                groupBoxCredentials.Visible = true;
                labelImap.Visible = false;
                labelImapPort.Visible = false;
                textBoxImap.Visible = false;
                textBoxImapPort.Visible = false;
            }
            else if(comboBoxImportFrom.Text == "Custom Server")
            {
                labelFolder.Visible = true;
                comboBoxServerFolder.Visible = true;
                groupBoxCredentials.Visible = true;
                labelImap.Visible = true;
                labelImapPort.Visible = true;
                textBoxImap.Visible = true;
                textBoxImapPort.Visible = true;
            }
            else
            {
                labelFolder.Visible = false;
                comboBoxServerFolder.Visible = false;
                groupBoxCredentials.Visible = false;
                labelImap.Visible = false;
                labelImapPort.Visible = false;
                textBoxImap.Visible = false;
                textBoxImapPort.Visible = false;
            }

         
        }

        private void buttonImport_Click(object sender, EventArgs e)
        {
            if (comboBoxImportFrom.Text == "" || comboBoxImportTo.Text == "")
            {
                MessageBox.Show("Please provide valid parameters.", "Failed");
                return;
            }
            if (comboBoxImportFrom.Text == "Gmail" || comboBoxImportFrom.Text == "Yandex" || comboBoxImportFrom.Text == "Custom Folder")
            {
                if (comboBoxServerFolder.Text == "" || tbUsername.Text == "" || tbPassword.Text == "")
                {
                    MessageBox.Show("Please provide valid parameters.", "Failed");
                    return;
                }
            }
            if(comboBoxImportFrom.Text == "Custom Folder")
            {
                if (textBoxImap.Text == "" || textBoxImapPort.Text == "")
                {
                    MessageBox.Show("Please provide valid parameters.", "Failed");
                    return;
                }
            }

            EmailType destinationType = new EmailType();
            string destinationFolderName = comboBoxImportTo.Text;
            _importExportTool = new ImportExportTool(_parentForm);
            buttonImport.Enabled = false;
            buttonImport.Text = "Importing...";

            switch (comboBoxImportTo.Text)
            {
                case "Inbox":
                    {
                        destinationType = EmailType.Inbox;
                    }
                    break;

                case "Sent Emails":
                    {
                        destinationType = EmailType.SentEmails;
                    }
                    break;

                default:
                    {
                        destinationType = EmailType.CollectionEmail;
                    }
                    break;
            }

            EmailType sourceType = new EmailType();
            if (comboBoxServerFolder.Text == "Inbox")
            {
                sourceType = EmailType.Inbox;
            }
            else if (comboBoxServerFolder.Text == "Sent Emails")
            {
                sourceType = EmailType.SentEmails;
            }

            if (comboBoxImportFrom.Text == "Gmail")
            {
                Task.Run(() =>
                {
                    this.Invoke((Action)(() => pictureBoxLoading.Visible = true));

                    _importExportTool.ImportEmailsFromServer(
                        new ServerInfo(ServerInfo.ServerPreset.Gmail), sourceType, destinationType,
                        tbUsername.Text, tbPassword.Text, destinationFolderName);

                    this.Invoke((Action)(() => pictureBoxLoading.Visible = false));
                    this.Invoke((Action)(() => this.Close()));
                });
            }
            else if (comboBoxImportFrom.Text == "Yandex")
            {
                Task.Run(() =>
                {
                    this.Invoke((Action)(() => pictureBoxLoading.Visible = true));

                    _importExportTool.ImportEmailsFromServer(
                        new ServerInfo(ServerInfo.ServerPreset.Yandex), sourceType, destinationType,
                        tbUsername.Text, tbPassword.Text, destinationFolderName);

                    this.Invoke((Action)(() => pictureBoxLoading.Visible = false));
                    this.Invoke((Action)(() => this.Close()));
                });
            }
            else if(comboBoxImportFrom.Text == "Custom Server")
            {
                if(!Regex.IsMatch(textBoxImapPort.Text, @"^\d+$"))
                {
                    MessageBox.Show("IMAP port must be numeric value", "Failed");
                    return;
                }

                var serverInfo = new ServerInfo(textBoxImap.Text, Convert.ToInt32(textBoxImapPort.Text));

                Task.Run(() =>
                {
                    this.Invoke((Action)(() => pictureBoxLoading.Visible = true));

                    _importExportTool.ImportEmailsFromServer(
                        serverInfo, sourceType, destinationType,
                        tbUsername.Text, tbPassword.Text, destinationFolderName);

                    this.Invoke((Action)(() => pictureBoxLoading.Visible = false));
                    this.Invoke((Action)(() => this.Close()));
                });
            }
            else
            {
                FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();

                folderBrowserDialog.Description = "Browse for folder that contains .eml files";
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {

                    Task.Run(() =>
                    {
                        this.Invoke((Action)(() => pictureBoxLoading.Visible = true));

                        _importExportTool.ImportEmailsFromFile(destinationType, folderBrowserDialog.SelectedPath, destinationFolderName);

                        this.Invoke((Action)(() => pictureBoxLoading.Visible = false));
                        this.Invoke((Action)(() => this.Close()));
                    });
                }
            }
        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
            if (comboBoxExportFrom.Text == "")
            {
                MessageBox.Show("Please provide valid parameters.", "Failed");
                return;
            }

            EmailType sourceType = new EmailType();
            string sourceFolderName = comboBoxExportFrom.Text;
            _importExportTool = new ImportExportTool(_parentForm);
            buttonImport.Enabled = false;
            buttonImport.Text = "Exporting...";

            var folderBrowserDialog1 = new FolderBrowserDialog();

            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result != DialogResult.OK)
            {
                return;
            }

            switch (comboBoxExportFrom.Text)
            {
                case "Inbox":
                    {
                        sourceType = EmailType.Inbox;

                        Task.Run(() =>
                        {
                            this.Invoke((Action)(() => pictureBoxLoading.Visible = true));
                            _importExportTool.ExportEmails(sourceType, folderBrowserDialog1.SelectedPath);
                            this.Invoke((Action)(() => pictureBoxLoading.Visible = false));
                            this.Invoke((Action)(() => this.Close()));
                        });
                    }
                    break;

                case "Sent Emails":
                    {
                        sourceType = EmailType.SentEmails;

                        Task.Run(() =>
                        {
                            this.Invoke((Action)(() => pictureBoxLoading.Visible = true));
                            _importExportTool.ExportEmails(sourceType, folderBrowserDialog1.SelectedPath);
                            this.Invoke((Action)(() => pictureBoxLoading.Visible = false));
                            this.Invoke((Action)(() => this.Close()));
                        });
                    }
                    break;

                default:
                    {
                        sourceType = EmailType.CollectionEmail;
                        _importExportTool.ExportEmails(sourceType, folderBrowserDialog1.SelectedPath, sourceFolderName);
                        this.Close();
                    }
                    break;
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonCancel2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}