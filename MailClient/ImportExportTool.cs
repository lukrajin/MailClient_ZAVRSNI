using CsvHelper;
using MimeKit;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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
                        var inboxEmails = LoadMimeMessages(path);
                        foreach (var email in inboxEmails)
                        {
                            _parentForm.MailReceiver.AddMessageToFolder(EmailType.Inbox, email);
                        }
                        _parentForm.Invoke((Action)(() => _parentForm.toolStripButtonRefresh.PerformClick()));
                    }
                    break;

                case EmailType.SentEmails:
                    {
                        var sentEmails = LoadMimeMessages(path);
                        foreach (var email in sentEmails)
                        {
                            _parentForm.MailReceiver.AddMessageToFolder(EmailType.SentEmails, email);
                        }
                        _parentForm.Invoke((Action)(() => _parentForm.toolStripButtonRefresh.PerformClick()));
                    }
                    break;

                case EmailType.CollectionEmail:
                    {
                        var collection = LoadMimeMessages(path);

                        foreach (var email in collection)
                        {
                            if (_parentForm.EmailCollection.ContainsKey(email.MessageId))
                            {
                                MessageBox.Show("Item already exists in collection", "Import failed");
                            }
                            else
                            {
                                var selectedFolder = _parentForm.FolderList[destinationFolderName];
                                ++selectedFolder.ItemCount;

                                var fromAddresses = new List<string>();
                                foreach (var address in email.From)
                                {
                                    fromAddresses.Add(((MailboxAddress)address).Address);
                                }

                                var toAddresses = new List<string>();
                                foreach (var address in email.To)
                                {
                                    toAddresses.Add(((MailboxAddress)address).Address);
                                }

                                var collectionEmail = new Models.CollectionEmail
                                {
                                    Id = email.MessageId,
                                    From = string.Join(";", fromAddresses),
                                    To = string.Join(";", toAddresses),
                                    Subject = email.Subject,
                                    TextBody = email.TextBody,
                                    HtmlBody = email.HtmlBody,
                                    Date = email.Date.UtcDateTime,
                                    CustomFolderName = destinationFolderName,
                                };

                                _parentForm.EmailCollection.TryAdd(collectionEmail.Id, collectionEmail);
                            }
                        }
                    }
                    break;
            }
        }

        private List<MimeMessage> LoadMimeMessages(string folderPath)
        {
            var mimeMessages = new List<MimeMessage>();

            DirectoryInfo d = new DirectoryInfo(folderPath);
            FileInfo[] Files = d.GetFiles("*.eml");

            foreach (FileInfo file in Files)
            {
                var mimeMessage = MimeMessage.Load(file.FullName);
                mimeMessages.Add(mimeMessage);
            }

            return mimeMessages;
        }

        public void ImportEmailsFromServer(ServerInfo importFrom, EmailType sourceType,
            EmailType destinationType, string username, string password, string destinationFolderName = null)
        {

            MailReceiver mailReceiver = new MailReceiver(importFrom.ImapServer, importFrom.ImapPort, 
                true, username, password, _parentForm);

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
                        _parentForm.Invoke((Action)(() => _parentForm.toolStripButtonRefresh.PerformClick()));
                    }
                    break;

                case EmailType.SentEmails:
                    {
                        foreach (var email in messages)
                        {
                            _parentForm.MailReceiver.AddMessageToFolder(EmailType.SentEmails, email);
                        }
                        _parentForm.Invoke((Action)(() => _parentForm.toolStripButtonRefresh.PerformClick()));
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

                                var fromAddresses = new List<string>();
                                foreach (var address in email.From)
                                {
                                    fromAddresses.Add(((MailboxAddress)address).Address);
                                }

                                var toAddresses = new List<string>();
                                foreach (var address in email.To)
                                {
                                    toAddresses.Add(((MailboxAddress)address).Address);
                                }

                                var collectionEmail = new Models.CollectionEmail
                                {
                                    Id = email.MessageId,
                                    From = string.Join(";", fromAddresses),
                                    To = string.Join(";", toAddresses),
                                    Subject = email.Subject,
                                    TextBody = email.TextBody,
                                    HtmlBody = email.HtmlBody,
                                    Date = email.Date.UtcDateTime,
                                    CustomFolderName = destinationFolderName,
                                };

                                _parentForm.EmailCollection.TryAdd(collectionEmail.Id, collectionEmail);
                            }
                        }
                    }
                    break;
            }
            mailReceiver.Disconnect();
        }

        public void SaveCollectionToDb()
        {
            using (var db = new Models.DatabaseContext())
            {
                var emails = db.Emails.ToList();

                foreach (var email in emails)
                {
                    if (!_parentForm.FolderList.ContainsKey(email.Id))
                    {
                        db.Emails.Remove(email);
                    }
                }

                db.SaveChanges();

                foreach (var email in _parentForm.EmailCollection.Values)
                {
                    db.Emails.AddOrUpdate(email);
                }

                db.SaveChanges();
            }
        }

        public void SaveFolderListToDb()
        {
            using (var db = new Models.DatabaseContext())
            {
                var folders = db.Folders.ToList();

                foreach (var folder in folders)
                {
                    if (!_parentForm.FolderList.ContainsKey(folder.FolderName))
                    {
                        db.Folders.Remove(folder);
                    }
                }

                db.SaveChanges();

                foreach (var folder in _parentForm.FolderList.Values)
                {
                    db.Folders.AddOrUpdate(folder);
                }

                db.SaveChanges();
            }
        }

        internal ConcurrentDictionary<string, Models.CustomFolder> LoadFolderList()
        {
            var context = new Models.DatabaseContext();
            var folderList = context.Folders.ToList();

            var folderListConcurrent = new ConcurrentDictionary<string, Models.CustomFolder>();

            foreach (var folder in folderList)
            {
                folderListConcurrent.TryAdd(folder.FolderName, folder);
            }

            return folderListConcurrent;
        }

        public void ExportEmails(EmailType emailType, string path, string folderName = null)
        {
            if (emailType == EmailType.Inbox)
            {
                foreach (var email in _parentForm.ReceivedEmails.Values)
                {
                    var message = _parentForm.MailReceiver.GetEmail(EmailType.Inbox, email.UniqueId);

                    message.WriteTo(Path.Combine(path, email.Id + ".eml"));
                }
            }
            else if (emailType == EmailType.SentEmails)
            {
                foreach (var email in _parentForm.SentEmails.Values)
                {
                    var message = _parentForm.MailReceiver.GetEmail(EmailType.SentEmails, email.UniqueId);

                    message.WriteTo(Path.Combine(path, email.Id + ".eml"));
                }
            }
            else
            {
                foreach (var email in _parentForm.EmailCollection.Values)
                {
                    if (email.CustomFolderName == folderName)
                    {
                        var message = new MimeMessage();

                        foreach (var address in email.From.Split(';'))
                        {
                            message.From.Add(new MailboxAddress(address));
                        }

                        foreach (var address in email.To.Split(';'))
                        {
                            message.To.Add(new MailboxAddress(address));
                        }

                        var bodyBuilder = new BodyBuilder();

                        if (email.TextBody == null)
                        {
                            bodyBuilder.TextBody = "";
                        }
                        else
                        {
                            bodyBuilder.TextBody = email.TextBody;
                        }

                        bodyBuilder.HtmlBody = email.HtmlBody;

                        message.Body = bodyBuilder.ToMessageBody();

                        message.Subject = email.Subject;

                        message.Date = email.Date;
                        message.MessageId = email.Id;

                        message.WriteTo(Path.Combine(path, email.Id + ".eml"));
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

        public ConcurrentDictionary<string, Models.CollectionEmail> LoadCollection()
        {
            var context = new Models.DatabaseContext();
            var collectionEmails = context.Emails.ToList();

            var emailsCollectionConcurrent = new ConcurrentDictionary<string, Models.CollectionEmail>();

            foreach (var email in collectionEmails)
            {
                emailsCollectionConcurrent.TryAdd(email.Id, email);
            }

            return emailsCollectionConcurrent;
        }

        public enum ServerImportType { FromGmail, FromYandex, CustomServer };
    }
}