using System;
using System.Linq;
using System.Windows.Forms;

namespace MailClient
{
    public partial class FolderEdit : Form
    {
        private MailClientForm _parentForm;

        public FolderEdit(MailClientForm parentForm , string foldername)
        {
            InitializeComponent();
            IsEdit = true;
            OldFolderName = foldername;
            labelFolderAction.Text = "Enter new name";
            this.Text = "Rename Folder";
            buttonCreate.Text = "OK";
            _parentForm = parentForm;
        }

        public FolderEdit(MailClientForm parentForm)
        {
            InitializeComponent();
            _parentForm = parentForm;
        }

        public string FolderName { get; set; }
        public bool IsEdit { get; private set; }
        public string OldFolderName { get; private set; }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            FolderName = textBoxFolderName.Text;

            if (IsEdit == true)
            {
                if (_parentForm.FolderList.Values.FirstOrDefault(x => x.FolderName == FolderName) == null)
                {
                    var folder = _parentForm.FolderList.Values.FirstOrDefault(x => x.FolderName == OldFolderName);
                    Models.CustomFolder customFolder;
                    _parentForm.FolderList.TryRemove(OldFolderName, out customFolder);

                    foreach (var email in _parentForm.EmailCollection.Values)
                    {
                        if(email.CustomFolderName == OldFolderName)
                        {
                            email.CustomFolderName = FolderName;
                        }
                    }

                    folder.FolderName = FolderName;
                    _parentForm.FolderList.TryAdd(folder.FolderName, folder);
                }
                else
                {
                    MessageBox.Show("Folder name already exists", "Failed");
                }

                _parentForm.toolStripButtonRefresh.PerformClick();
                this.Close();
                return;
            }


            if (_parentForm.FolderList.Values.FirstOrDefault(x => x.FolderName == FolderName) == null)
            {
                _parentForm.FolderList.TryAdd(FolderName, new Models.CustomFolder
                {
                    FolderName = FolderName,
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now
                });
            }
            else
            {
                MessageBox.Show("Folder name already exists", "Failed");
            }

            _parentForm.toolStripButtonRefresh.PerformClick();
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}