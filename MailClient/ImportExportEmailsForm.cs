using System;
using System.IO;
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
        }

        private void comboBoxImportFrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxImportFrom.Text == "Gmail" || comboBoxImportFrom.Text == "Yandex")
            {
                labelFolder.Visible = true;
                comboBoxServerFolder.Visible = true;
                groupBoxCredentials.Visible = true;
            }
            else
            {
                labelFolder.Visible = false;
                comboBoxServerFolder.Visible = false;
                groupBoxCredentials.Visible = false;
            }
        }

        private void buttonImport_Click(object sender, EventArgs e)
        {
            if (comboBoxImportFrom.Text == "" || comboBoxImportTo.Text == "")
            {
                MessageBox.Show("Please provide valid parameters.", "Failed");
                return;
            }
            if(comboBoxImportFrom.Text =="Gmail" || comboBoxImportFrom.Text == "Yandex")
            {
                if(comboBoxServerFolder.Text == "" || tbUsername.Text==""|| tbPassword.Text =="")
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
                        ImportExportTool.ServerImportType.FromGmail, sourceType, destinationType,
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
                        ImportExportTool.ServerImportType.FromYandex, sourceType, destinationType,
                        tbUsername.Text, tbPassword.Text, destinationFolderName);

                    this.Invoke((Action)(() => pictureBoxLoading.Visible = false));
                    this.Invoke((Action)(() => this.Close()));
                });
            }
            else
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "CSV files (*.csv)|*.txt|All files (*.*)|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    if(!File.Exists(openFileDialog.FileName))
                    {
                        MessageBox.Show("Loading Failed", "File does not exists");
                        return;
                    }

                    Task.Run(() =>
                    {
                        this.Invoke((Action)(() => pictureBoxLoading.Visible = true));

                        _importExportTool.ImportEmailsFromFile(destinationType, openFileDialog.FileName, destinationFolderName);

                        this.Invoke((Action)(() => pictureBoxLoading.Visible = false));
                        this.Invoke((Action)(() => this.Close()));
                    });
                }
    
            }
        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
            if(comboBoxExportFrom.Text == "")
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
                        _importExportTool.ExportEmails(sourceType, folderBrowserDialog1.SelectedPath);
                    }
                    break;

                case "Sent Emails":
                    {
                        sourceType = EmailType.SentEmails;
                        _importExportTool.ExportEmails(sourceType, folderBrowserDialog1.SelectedPath);
                    }
                    break;

                default:
                    {
                        sourceType = EmailType.CollectionEmail;
                        _importExportTool.ExportEmails(sourceType, folderBrowserDialog1.SelectedPath, sourceFolderName);
                    }
                    break;
            }
            this.Close();
        }
    }
}