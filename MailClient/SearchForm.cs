using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MailClient
{
    public partial class SearchForm : Form
    {
        private MailClientForm _parentForm;

        public SearchForm(MailClientForm parentForm)
        {
            InitializeComponent();
            _parentForm = parentForm;
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            var rowsToDelete = new List<DataGridViewRow>();

            Task.Run(() =>
            {
                foreach (DataGridViewRow row in _parentForm.dataGridViewEmails.Rows)
                {
                    bool containsText = false;
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        var cellValue = cell.Value.ToString().ToLower();
                        if (cellValue != null)
                        {
                            if (cellValue.Contains(tbSearchText.Text.ToLower()))
                            {
                                containsText = true;
                            }
                        }
                    }

                    if (!containsText)
                    {
                        rowsToDelete.Add(row);
                    }
                }
                foreach(DataGridViewRow row in rowsToDelete)
                {
                    _parentForm.Invoke((Action)(() => _parentForm.dataGridViewEmails.Rows.RemoveAt(row.Index)));
                }
            });
        }
    }
}