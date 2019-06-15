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
    public partial class ImportExportEmailsForm : Form
    {
        private MailClientForm _parentForm;

        public ImportExportEmailsForm(MailClientForm parentForm)
        {
            InitializeComponent();
            _parentForm = parentForm;
        }

        private void comboBoxImportFrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBoxImportFrom.Text == "Gmail" || comboBoxImportFrom.Text == "Yandex")
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
    }
}
