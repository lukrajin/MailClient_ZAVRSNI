using HtmlAgilityPack;
using MailKit;
using MailKit.Net.Imap;
using MimeKit;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MailClient
{
    public interface IMailReceiver
    {
        void Connect();
        void Disconnect();
        ConcurrentDictionary<string, InboxEmail> GetInboxEmailList();
        ConcurrentDictionary<string, SentEmail> GetSentEmailList();
        MimeMessage GetEmail(EmailType emailType, UniqueId uniqueId);
        void DeleteEmail(EmailType emailType, UniqueId uniqueId);
        void SetMessageSeen(EmailType emailType, UniqueId uniqueId);
        void AddMessageToFolder(EmailType emailType, MimeMessage mimeMessage);

    }
    public class MailReceiver: IMailReceiver
    {
        private static string[] CommonSentFolderNames = {"sent", "Sent",
            "Sent Items", "Sent Mail","Sent Email", "Poslano", "sent-items", "sent-email", "sent-mail"};

        private ImapClient client = new ImapClient();
        private string mailServer;

        public string Host
        {
            get
            {
                return mailServer;
            }
        }

        private int port;
        private bool ssl;
        public string Login { get; set; }
        public string Password { get; set; }

        private MailClientForm _mailClientForm;

        public IMailFolder SentFolder { get; private set; }

        public MailReceiver(string mailServer, int port, bool ssl, string login, string password, MailClientForm mailClientForm)
        {
            this.mailServer = mailServer;
            this.port = port;
            this.ssl = ssl;
            this.Login = login;
            this.Password = password;

            _mailClientForm = mailClientForm;
        }
        public MailReceiver(string mailServer, int port, bool ssl, string login, string password)
        {
            this.mailServer = mailServer;
            this.port = port;
            this.ssl = ssl;
            this.Login = login;
            this.Password = password;
        }

        public void Connect()
        {
            client.Connect(mailServer, port, ssl);
            client.AuthenticationMechanisms.Remove("XOAUTH2");

            client.Authenticate(Login, Password);
        }

        public void Disconnect()
        {
            client.Disconnect(true);
        }

        public ConcurrentDictionary<string, InboxEmail> GetInboxEmailList()
        {
            var mailMessages = new ConcurrentDictionary<string, InboxEmail>();
            var inbox = client.Inbox;
            inbox.Open(FolderAccess.ReadWrite);

            var summary = inbox.Fetch(0, -1, MessageSummaryItems.Full |
                MessageSummaryItems.UniqueId | MessageSummaryItems.PreviewText);

            foreach (var item in summary)
            {

                var inboxEmail = GetInboxEmailFromSummary(item);

                mailMessages.TryAdd(inboxEmail.Id, inboxEmail);
            }

            return mailMessages;
        }
        InboxEmail GetInboxEmailFromSummary(IMessageSummary item)
        {
            var fromAddresses = new List<string>();
            foreach (var address in item.Envelope.From)
            {
                fromAddresses.Add(((MailboxAddress)address).Address);
            }

            var toAddresses = new List<string>();
            foreach (var address in item.Envelope.To)
            {
                toAddresses.Add(((MailboxAddress)address).Address);
            }

            var inboxEmail = new InboxEmail
            {
                Id = item.Envelope.MessageId,
                ArrivalTime = item.Date.UtcDateTime,
                From = string.Join(";", fromAddresses),
                To = string.Join(";", toAddresses),
                Subject = item.Envelope.Subject,
                PreviewText = HtmlUtility.ConvertToPlainText(item.PreviewText),
                UniqueId = item.UniqueId,
                IsRead = item.Flags.Value.HasFlag(MessageFlags.Seen)
            };


            return inboxEmail;
        }
        public List<MimeMessage> GetInboxMimeMessages()
        {
            var mailMessages = new List<MimeMessage>();
            var inbox = client.Inbox;
            inbox.Open(FolderAccess.ReadWrite);

            var items = inbox.Fetch(UniqueIdRange.All, MessageSummaryItems.Flags);

            foreach (var item in items)
            {
                var message = inbox.GetMessage(item.UniqueId);
                mailMessages.Add(message);
            }

            return mailMessages;
        }
        public List<MimeMessage> GetSentMimeMessages()
        {
            var mailMessages = new List<MimeMessage>();

            SentFolder = GetSentEmailsFolder();

            if (SentFolder == null)
                SentFolder = client.GetFolder(SpecialFolder.Sent);
            if (SentFolder == null)
                return mailMessages;

            SentFolder.Open(FolderAccess.ReadWrite);

            var items = SentFolder.Fetch(UniqueIdRange.All, MessageSummaryItems.Flags);

            foreach (var item in items)
            {
                var message = SentFolder.GetMessage(item.UniqueId);
                mailMessages.Add(message);
            }

            return mailMessages;
        }

        public ConcurrentDictionary<string, SentEmail> GetSentEmailList()
        {
            var mailMessages = new ConcurrentDictionary<string, SentEmail>();

            SentFolder = GetSentEmailsFolder();

            if (SentFolder == null)
                SentFolder = client.GetFolder(SpecialFolder.Sent);
            if (SentFolder == null)
                return mailMessages;

            SentFolder.Open(FolderAccess.ReadWrite);

            var summary = SentFolder.Fetch(0, -1, MessageSummaryItems.Full
                | MessageSummaryItems.UniqueId | MessageSummaryItems.PreviewText);

            foreach (var item in summary)
            {
                var sentEmail = GetSentEmailFromSummary(item);
                mailMessages.TryAdd(sentEmail.Id, sentEmail);
            }

            return mailMessages;
        }
        SentEmail GetSentEmailFromSummary(IMessageSummary item)
        {
            var fromAddresses = new List<string>();
            foreach (var address in item.Envelope.From)
            {
                fromAddresses.Add(((MailboxAddress)address).Address);
            }

            var toAddresses = new List<string>();
            foreach (var address in item.Envelope.To)
            {
                toAddresses.Add(((MailboxAddress)address).Address);
            }

            var sentEmail = new SentEmail
            {
                Id = item.Envelope.MessageId,
                SentTime = item.Date.UtcDateTime,
                From = string.Join(";", fromAddresses),
                To = string.Join(";", toAddresses),
                Subject = item.Envelope.Subject,
                UniqueId = item.UniqueId,
                PreviewText = HtmlUtility.ConvertToPlainText(item.PreviewText),
                IsRead = item.Flags.Value.HasFlag(MessageFlags.Seen)
            };

            return sentEmail;
        }
        public IMailFolder GetSentEmailsFolder()
        {
            var personal = client.GetFolder(client.PersonalNamespaces[0]);
            var sentFolder = personal.GetSubfolders(false).FirstOrDefault(x => CommonSentFolderNames.Contains(x.Name));
            return sentFolder;
        }

        public void DeleteEmail(EmailType emailType, UniqueId uniqueId)
        {
            lock (this)
            {
                if (emailType == EmailType.Inbox)
                {
                    if (!client.Inbox.IsOpen)
                        client.Inbox.Open(FolderAccess.ReadWrite);

                    client.Inbox.AddFlags(uniqueId, MessageFlags.Deleted, true);
                    client.Inbox.Expunge();
                }
                else if (emailType == EmailType.SentEmails)
                {
                    if (!SentFolder.IsOpen)
                        SentFolder.Open(FolderAccess.ReadWrite);

                    SentFolder.AddFlags(uniqueId, MessageFlags.Deleted, true);
                    SentFolder.Expunge();
                }
            }
        }

        public void SetMessageSeen(EmailType emailType, UniqueId uniqueId)
        {
            lock (this)
            {
                if (emailType == EmailType.Inbox)
                {
                    if (!client.Inbox.IsOpen)
                        client.Inbox.Open(FolderAccess.ReadWrite);

                    client.Inbox.AddFlags(uniqueId, MessageFlags.Seen, true);
                }
                else if (emailType == EmailType.SentEmails)
                {
                    if (!SentFolder.IsOpen)
                        SentFolder.Open(FolderAccess.ReadWrite);

                    SentFolder.AddFlags(uniqueId, MessageFlags.Seen, true);
                }
            }
        }

        public void AddMessageToFolder(EmailType emailType, MimeMessage mimeMessage)
        {
            lock (this)
            {
                if (emailType == EmailType.Inbox)
                {
                    if (!client.Inbox.IsOpen)
                        client.Inbox.Open(FolderAccess.ReadWrite);

                    client.Inbox.Append(mimeMessage);
                }
                else if (emailType == EmailType.SentEmails)
                {
                    if (!SentFolder.IsOpen)
                        SentFolder.Open(FolderAccess.ReadWrite);

                    SentFolder.Append(mimeMessage);
                }
            }
        }

        public MimeMessage GetEmail(EmailType emailType, UniqueId uniqueId)
        {
            lock (this)
            {
                if (emailType == EmailType.Inbox)
                {
                    if (!client.Inbox.IsOpen)
                        client.Inbox.Open(FolderAccess.ReadWrite);

                    return client.Inbox.GetMessage(uniqueId);
                }
                else if (emailType == EmailType.SentEmails)
                {
                    if (!SentFolder.IsOpen)
                        SentFolder.Open(FolderAccess.ReadWrite);

                    return SentFolder.GetMessage(uniqueId);
                }

                return null;
            }
        }
        public MailReceiver GetMailReceiverInstance()
        {
            MailReceiver mailReceiver = new MailReceiver(mailServer, port, ssl, Login, Password);

            mailReceiver.Connect();

            return mailReceiver; 
        }

    }
}