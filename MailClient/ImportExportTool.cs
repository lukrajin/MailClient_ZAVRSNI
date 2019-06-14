using CsvHelper;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace MailClient
{
    public class ImportExportTool
    {
        private MailClientForm _parentForm;

        public ImportExportTool(MailClientForm parentForm)
        {
            _parentForm = parentForm;
        }

        public void Export(EmailFolder folderType, bool isDefaultLocation)
        {
            if (folderType == EmailFolder.Inbox)
            {
                var records = new List<InboxEmail>();

                foreach (var email in _parentForm.ReceivedEmails)
                {
                    records.Add(email);
                }

                Save(folderType, records, isDefaultLocation);
            }
            else if (folderType == EmailFolder.SentEmails)
            {
                var records = new List<SentEmail>();

                foreach (var email in _parentForm.SentEmails)
                {
                    records.Add(email);
                }

                Save(folderType, records, isDefaultLocation);
            }
            else
            {
                var records = new List<CollectionEmail>();

                foreach (var email in _parentForm.EmailCollection)
                {
                    records.Add(email);
                }

                Save(folderType, records, isDefaultLocation);
            }
        }

        private void Save<T>(EmailFolder folderType, List<T> records, bool isDefaultLocation)
        {
            if (isDefaultLocation)
            {
                var _fileName = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath) + "\\Data",
                    GetEmailTypeFileName(folderType) + ".csv");

                if (!Directory.Exists(Path.GetDirectoryName(_fileName)))
                    Directory.CreateDirectory(Path.GetDirectoryName(_fileName));

                WriteRecords(records, _fileName);

                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = "SentEmails_" + DateTime.Now.ToString("dd_MM_yyyy hh_mm_ss") + ".csv";
            saveFileDialog.Filter = "CSV files (*.csv)|*.txt|All files (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                WriteRecords(records, saveFileDialog.FileName);
            }
        }

        private void WriteRecords<T>(List<T> records, string path)
        {
            using (var writer = new StreamWriter(path))
            using (var csv = new CsvWriter(writer))
            {
                csv.WriteRecords(records);
            }
        }

        public string GetEmailTypeFileName(EmailFolder folderType)
        {
            if (folderType == EmailFolder.Inbox)
            {
                return "InboxEmail";
            }
            else if (folderType == EmailFolder.SentEmails)
            {
                return "SentEmails";
            }
            else
            {
                return "EmailCollection";
            }
        }

        public string GetEmailsPath(EmailFolder folderType, bool isDefaultLocation)
        {
            string fileName = "";

            if (isDefaultLocation)
            {
                var _fileName = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath) + "\\Data",
                    GetEmailTypeFileName(folderType) + ".csv");
                if (File.Exists(_fileName))
                {
                    fileName = _fileName;
                }
                else
                {
                    MessageBox.Show("Loading Failed", "File does not exists");
                    return null;
                }
            }
            else
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "CSV files (*.csv)|*.txt|All files (*.*)|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    fileName = openFileDialog.FileName;
                }
                else
                {
                    return null;
                }
            }

            return fileName;
        }

        public ConcurrentBag<SentEmail> LoadSent(bool isDefaultLocation)
        {
            var sentEmails = new List<SentEmail>();

            if (GetEmailsPath(EmailFolder.SentEmails, isDefaultLocation) != null)
            {
                using (var reader = new StreamReader(GetEmailsPath(EmailFolder.SentEmails, isDefaultLocation)))
                using (var csv = new CsvReader(reader))
                {
                    sentEmails = csv.GetRecords<SentEmail>().ToList();
                }

                var sentEmailsConcurrent = new ConcurrentBag<SentEmail>();
                foreach (var email in sentEmails)
                {
                    sentEmailsConcurrent.Add(email);
                }

                return sentEmailsConcurrent;
            }

            return null;
        }

        public ConcurrentBag<InboxEmail> LoadInbox(bool isDefaultLocation)
        {
            var inboxEmails = new List<InboxEmail>();

            if (GetEmailsPath(EmailFolder.Inbox, isDefaultLocation) != null)
            {
                using (var reader = new StreamReader(GetEmailsPath(EmailFolder.Inbox, isDefaultLocation)))
                using (var csv = new CsvReader(reader))
                {
                    inboxEmails = csv.GetRecords<InboxEmail>().ToList();
                }

                var inboxEmailsConcurrent = new ConcurrentBag<InboxEmail>();
                foreach (var email in inboxEmails)
                {
                    inboxEmailsConcurrent.Add(email);
                }

                return inboxEmailsConcurrent;
            }

            return null;
        }

        public ConcurrentBag<CollectionEmail> LoadCollection(bool isDefaultLocation)
        {
            var collectionEmails = new List<CollectionEmail>();
            if (GetEmailsPath(EmailFolder.SentEmails, isDefaultLocation) != null)
            {
                using (var reader = new StreamReader(GetEmailsPath(EmailFolder.EmailCollection, isDefaultLocation)))
                using (var csv = new CsvReader(reader))
                {
                    collectionEmails = csv.GetRecords<CollectionEmail>().ToList();
                }

                var emailsCollectionConcurrent = new ConcurrentBag<CollectionEmail>();
                foreach (var email in collectionEmails)
                {
                    emailsCollectionConcurrent.Add(email);
                }

                return emailsCollectionConcurrent;
            }

            return null;
        }
    }
}