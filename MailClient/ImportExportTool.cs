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

        public void ExportEmails(EmailType emailType, bool isDefaultLocation)
        {
            if (emailType == EmailType.Inbox)
            {
                var records = new List<InboxEmail>();

                foreach (var email in _parentForm.ReceivedEmails.Values)
                {
                    records.Add(email);
                }

                SaveEmails(emailType, records, isDefaultLocation);
            }
            else if (emailType == EmailType.SentEmails)
            {
                var records = new List<SentEmail>();

                foreach (var email in _parentForm.SentEmails.Values)
                {
                    records.Add(email);
                }

                SaveEmails(emailType, records, isDefaultLocation);
            }
            else
            {
                var records = new List<CollectionEmail>();

                foreach (var email in _parentForm.EmailCollection.Values)
                {
                    records.Add(email);
                }

                SaveEmails(emailType, records, isDefaultLocation);
            }
        }
        public void ExportFolderList()
        {
            var records = new List<CustomFolder>();

            foreach(var folder in _parentForm.FolderList)
            {
                records.Add(folder.Value);
            }
            var _fileName = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath) + "\\Data",
                "FolderList" + ".csv");

            if (!Directory.Exists(Path.GetDirectoryName(_fileName)))
                Directory.CreateDirectory(Path.GetDirectoryName(_fileName));

            WriteRecords(records, _fileName);

        }

        internal ConcurrentDictionary<string, CustomFolder> LoadFolderList()
        {
            var folderList = new List<CustomFolder>();


            var _fileName = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath) + "\\Data",
            "FolderList"+ ".csv");

            if (!File.Exists(_fileName))
                return null;

            using (var reader = new StreamReader(_fileName))
            using (var csv = new CsvReader(reader))
            {
                folderList = csv.GetRecords<CustomFolder>().ToList();
            }

            var folderListConcurrent = new ConcurrentDictionary<string,CustomFolder>();

            foreach (var folder in folderList)
            {
                folderListConcurrent.TryAdd(folder.FolderName, folder);
            }

            return folderListConcurrent;
        
        }

    private void SaveEmails<T>(EmailType emailType, List<T> records, bool isDefaultLocation)
    {
        if (isDefaultLocation)
        {
            var _fileName = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath) + "\\Data",
                GetEmailTypeFileName(emailType) + ".csv");

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

    public string GetEmailTypeFileName(EmailType emailType)
    {
        if (emailType == EmailType.Inbox)
        {
            return "InboxEmail";
        }
        else if (emailType == EmailType.SentEmails)
        {
            return "SentEmails";
        }
        else
        {
            return "EmailCollection";
        }
    }

    public string GetEmailsPath(EmailType emailType, bool isDefaultLocation)
    {
        string fileName = "";

        if (isDefaultLocation)
        {
            var _fileName = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath) + "\\Data",
                GetEmailTypeFileName(emailType) + ".csv");
            if (File.Exists(_fileName))
            {
                fileName = _fileName;
            }
            else
            {
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
                MessageBox.Show("Loading Failed", "File does not exists");
                return null;
            }
        }

        return fileName;
    }

    public ConcurrentDictionary<string, SentEmail> LoadSent(bool isDefaultLocation)
    {
        var sentEmails = new List<SentEmail>();

        if (GetEmailsPath(EmailType.SentEmails, isDefaultLocation) != null)
        {
            using (var reader = new StreamReader(GetEmailsPath(EmailType.SentEmails, isDefaultLocation)))
            using (var csv = new CsvReader(reader))
            {
                sentEmails = csv.GetRecords<SentEmail>().ToList();
            }

                var sentEmailsConcurrent = new ConcurrentDictionary<string, SentEmail>();
            foreach (var email in sentEmails)
            {
                sentEmailsConcurrent.TryAdd(email.Id, email);
            }

            return sentEmailsConcurrent;
        }

        return null;
    }

    public ConcurrentDictionary<string, InboxEmail> LoadInbox(bool isDefaultLocation)
    {
        var inboxEmails = new List<InboxEmail>();

        if (GetEmailsPath(EmailType.Inbox, isDefaultLocation) != null)
        {
            using (var reader = new StreamReader(GetEmailsPath(EmailType.Inbox, isDefaultLocation)))
            using (var csv = new CsvReader(reader))
            {
                inboxEmails = csv.GetRecords<InboxEmail>().ToList();
            }

                var inboxEmailsConcurrent = new ConcurrentDictionary<string, InboxEmail>();
            foreach (var email in inboxEmails)
            {
                inboxEmailsConcurrent.TryAdd(email.Id, email);
            }

            return inboxEmailsConcurrent;
        }

        return null;
    }

    public ConcurrentDictionary<string,CollectionEmail> LoadCollection(bool isDefaultLocation)
    {
        var collectionEmails = new List<CollectionEmail>();
        if (GetEmailsPath(EmailType.CollectionEmail, isDefaultLocation) != null)
        {
            using (var reader = new StreamReader(GetEmailsPath(EmailType.CollectionEmail, isDefaultLocation)))
            using (var csv = new CsvReader(reader))
            {
                collectionEmails = csv.GetRecords<CollectionEmail>().ToList();
            }

            var emailsCollectionConcurrent = new ConcurrentDictionary<string, CollectionEmail>();
            foreach (var email in collectionEmails)
            {
                emailsCollectionConcurrent.TryAdd(email.Id, email);
            }

            return emailsCollectionConcurrent;
        }

        return null;
    }
}
}