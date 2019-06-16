using CsvHelper;
using MimeKit;
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

        public void ImportEmailsFromFile(EmailType destinationType, string path, string destinationFolderName = null)
        {
            switch (destinationType)
            {
                case EmailType.Inbox:
                    {
                        var inboxEmails = LoadInbox(path);
                        foreach (var email in inboxEmails.Values)
                        {
                            var message = new MimeMessage();

                            message.From.Add(new MailboxAddress(email.From));
                            message.To.Add(new MailboxAddress(email.To));
                            message.Subject = email.Subject;
                            message.Body = new TextPart("plain") { Text = email.Body };
                            message.Date = email.ArrivalTime;

                            _parentForm.MailReceiver.AddMessageToFolder(EmailType.Inbox, message);
                        }
                        _parentForm.toolStripButtonRefresh.PerformClick();
                    }
                    break;

                case EmailType.SentEmails:
                    {
                        var sentEmails = LoadSent(path);
                        foreach (var email in sentEmails.Values)
                        {
                            var message = new MimeMessage();

                            message.From.Add(new MailboxAddress(email.From));
                            message.To.Add(new MailboxAddress(email.To));
                            message.Subject = email.Subject;
                            message.Body = new TextPart("plain") { Text = email.Body };
                            message.Date = email.SentTime;

                            _parentForm.MailReceiver.AddMessageToFolder(EmailType.SentEmails, message);
                        }
                        _parentForm.toolStripButtonRefresh.PerformClick();
                    }
                    break;

                case EmailType.CollectionEmail:
                    {
                        var collection = LoadCollection(path);

                        foreach (var email in collection.Values)
                        {
                            if (_parentForm.EmailCollection.ContainsKey(email.Id))
                            {
                                MessageBox.Show("Item already exists in collection", "Import failed");
                            }
                            else
                            {
                                var selectedFolder = _parentForm.FolderList[destinationFolderName];
                                ++selectedFolder.ItemCount;

                                email.CustomFolderName = destinationFolderName;
                                _parentForm.EmailCollection.TryAdd(email.Id, email);
                            }
                        }
                    }
                    break;
            }
        }

        public void ImportEmailsFromServer(ServerImportType importFrom, EmailType sourceType,
            EmailType destinationType, string username, string password, string destinationFolderName = null)
        {
            string host;
            int port;

            if (importFrom == ServerImportType.FromGmail)
            {
                host = ServerInfo.Gmail.ImapServer;
                port = ServerInfo.Gmail.ImapPort;
            }
            else
            {
                host = ServerInfo.Yandex.ImapServer;
                port = ServerInfo.Yandex.ImapPort;
            }

            MailReceiver mailReceiver = new MailReceiver(host, port, true, username, password, _parentForm);

            try
            {
                mailReceiver.Connect();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Login Failed");
                return;
            }

            List<MimeMessage> messages = new List<MimeMessage>();
            if (sourceType == EmailType.Inbox)
            {
                messages = mailReceiver.GetInboxMimeMessages();
            }
            else if (sourceType == EmailType.SentEmails)
            {
                messages = mailReceiver.GetSentMimeMessages();
            }

            switch (destinationType)
            {
                case EmailType.Inbox:
                    {
                        foreach (var email in messages)
                        {
                            _parentForm.MailReceiver.AddMessageToFolder(EmailType.Inbox, email);
                        }
                        _parentForm.toolStripButtonRefresh.PerformClick();
                    }
                    break;

                case EmailType.SentEmails:
                    {
                        foreach (var email in messages)
                        {
                            _parentForm.MailReceiver.AddMessageToFolder(EmailType.SentEmails, email);
                        }
                        _parentForm.toolStripButtonRefresh.PerformClick();
                    }
                    break;

                case EmailType.CollectionEmail:
                    {
                        foreach (var email in messages)
                        {
                            if (_parentForm.EmailCollection.ContainsKey(email.MessageId))
                            {
                                MessageBox.Show("Item already exists in collection", "Import failed");
                            }
                            else
                            {
                                var selectedFolder = _parentForm.FolderList[destinationFolderName];
                                ++selectedFolder.ItemCount;

                                _parentForm.EmailCollection.TryAdd(email.MessageId,
                                    new CollectionEmail
                                    {
                                        Id = email.MessageId,
                                        From = ((MailboxAddress)email.From[0]).Address,
                                        To = ((MailboxAddress)email.To[0]).Address,
                                        Subject = email.Subject,
                                        Body = email.TextBody,
                                        Date = email.Date.UtcDateTime,
                                        CustomFolderName = destinationFolderName,
                                        EmailType = sourceType,
                                    });
                            }
                        }
                    }
                    break;
            }
            mailReceiver.Disconnect();
        }

        public void ExportEmails(EmailType emailType)
        {
            if (emailType == EmailType.Inbox)
            {
                var records = new List<InboxEmail>();

                foreach (var email in _parentForm.ReceivedEmails.Values)
                {
                    records.Add(email);
                }

                SaveEmails(emailType, records);
            }
            else if (emailType == EmailType.SentEmails)
            {
                var records = new List<SentEmail>();

                foreach (var email in _parentForm.SentEmails.Values)
                {
                    records.Add(email);
                }

                SaveEmails(emailType, records);
            }
            else
            {
                var records = new List<CollectionEmail>();

                foreach (var email in _parentForm.EmailCollection.Values)
                {
                    records.Add(email);
                }

                SaveEmails(emailType, records);
            }
        }

        public void ExportFolderList()
        {
            var records = new List<CustomFolder>();

            foreach (var folder in _parentForm.FolderList)
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
            "FolderList" + ".csv");

            if (!File.Exists(_fileName))
                return null;

            using (var reader = new StreamReader(_fileName))
            using (var csv = new CsvReader(reader))
            {
                folderList = csv.GetRecords<CustomFolder>().ToList();
            }

            var folderListConcurrent = new ConcurrentDictionary<string, CustomFolder>();

            foreach (var folder in folderList)
            {
                folderListConcurrent.TryAdd(folder.FolderName, folder);
            }

            return folderListConcurrent;
        }

        private void SaveEmails<T>(EmailType emailType, List<T> records)
        {
            var _fileName = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath) + "\\Data",
                GetEmailTypeFileName(emailType) + ".csv");

            if (!Directory.Exists(Path.GetDirectoryName(_fileName)))
                Directory.CreateDirectory(Path.GetDirectoryName(_fileName));

            WriteRecords(records, _fileName);
        }
        public void ExportEmails(EmailType emailType, string path, string folderName = null)
        {
            if (emailType == EmailType.Inbox)
            {
                foreach (var email in _parentForm.ReceivedEmails.Values)
                {
                    var message = new MimeMessage();

                    message.From.Add(new MailboxAddress(email.From));
                    message.To.Add(new MailboxAddress(email.To));
                    message.Subject = email.Subject;
                    if (email.Body == null)
                    {
                        message.Body = new TextPart("plain")
                        {
                            Text = ""
                        };
                    }
                    else
                    {
                        message.Body = new TextPart("plain") { Text = email.Body };
                    }
                    message.Date = email.ArrivalTime;
                    message.MessageId = email.Id;

                    message.WriteTo(Path.Combine(path, email.Id + ".msg"));
                }
            }
            else if (emailType == EmailType.SentEmails)
            {
                foreach (var email in _parentForm.SentEmails.Values)
                {
                    var message = new MimeMessage();

                    message.From.Add(new MailboxAddress(email.From));
                    message.To.Add(new MailboxAddress(email.To));
                    message.Subject = email.Subject;
                    if (email.Body == null)
                    {
                        message.Body = new TextPart("plain")
                        {
                            Text = ""
                        };
                    }
                    else
                    {
                        message.Body = new TextPart("plain") { Text = email.Body };
                    }
                    message.Date = email.SentTime;
                    message.MessageId = email.Id;

                    message.WriteTo(Path.Combine(path, email.Id + ".msg"));
                }
            }
            else
            {
                foreach (var email in _parentForm.EmailCollection.Values)
                {
                    if (email.CustomFolderName == folderName)
                    {
                        var message = new MimeMessage();

                        message.From.Add(new MailboxAddress(email.From));
                        message.To.Add(new MailboxAddress(email.To));
                        message.Subject = email.Subject;
                        if (email.Body == null)
                        {
                            message.Body = new TextPart("plain")
                            {
                                Text = ""
                            };
                        }
                        else
                        {
                            message.Body = new TextPart("plain") { Text = email.Body };
                        }
                        message.Date = email.Date;
                        message.MessageId = email.Id;

                        message.WriteTo(Path.Combine(path, email.Id + ".msg"));
                    }
                }
            }
        }
        public void WriteRecords<T>(List<T> records, string path)
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

        public string GetEmailsPath(EmailType emailType)
        {
            string fileName = "";

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

            return fileName;
        }

        public ConcurrentDictionary<string, SentEmail> LoadSent(string path)
        {
            var sentEmails = new List<SentEmail>();

            if (path != null)
            {
                using (var reader = new StreamReader(path))
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

        public ConcurrentDictionary<string, InboxEmail> LoadInbox(string path)
        {
            var inboxEmails = new List<InboxEmail>();

            if (path != null)
            {
                using (var reader = new StreamReader(path))
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

        public ConcurrentDictionary<string, CollectionEmail> LoadCollection(string path)
        {
            var collectionEmails = new List<CollectionEmail>();
            if (path != null)
            {
                using (var reader = new StreamReader(path))
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

        public enum ServerImportType { FromGmail, FromYandex };
    }
}