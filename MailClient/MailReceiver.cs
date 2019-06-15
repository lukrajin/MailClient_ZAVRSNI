using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using MimeKit;

namespace MailClient
{
    public class MailReceiver
    {
        static string[] CommonSentFolderNames = {"sent", "Sent",
            "Sent Items", "Sent Mail" ,"Sent Email", "Poslano", "sent-items", "sent-email", "sent-mail" };
        ImapClient client = new ImapClient();
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

            var items = inbox.Fetch(UniqueIdRange.All, MessageSummaryItems.Flags);

            foreach (var item in items)
            {
 
                var message = inbox.GetMessage(item.UniqueId);
                mailMessages.TryAdd(message.MessageId, new InboxEmail
                {
                    Id= message.MessageId,
                    ArrivalTime = message.Date.UtcDateTime,
                    From = ((MailboxAddress)message.From[0]).Address,
                    To = ((MailboxAddress)message.To[0]).Address,
                    Subject = message.Subject,
                    Body =message.TextBody,
                    UniqueId = item.UniqueId,
                    IsRead = item.Flags.Value.HasFlag(MessageFlags.Seen)

                });
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
            var items = SentFolder.Fetch(UniqueIdRange.All, MessageSummaryItems.Flags);
  

            foreach (var item in items)
            {
                var message = SentFolder.GetMessage(item.UniqueId);
                mailMessages.TryAdd(message.MessageId, new SentEmail
                {
                    Id = message.MessageId,
                    SentTime = message.Date.UtcDateTime,
                    From = ((MailboxAddress)message.From[0]).Address,
                    To = ((MailboxAddress)message.To[0]).Address,
                    Subject = message.Subject,
                    Body = message.TextBody,
                    UniqueId = item.UniqueId,
                    IsRead = item.Flags.Value.HasFlag(MessageFlags.Seen)
                });
            }

            return mailMessages;
        }

        public IMailFolder GetSentEmailsFolder()
        {
            var personal = client.GetFolder(client.PersonalNamespaces[0]);
            var sentFolder = personal.GetSubfolders(false).FirstOrDefault(x => CommonSentFolderNames.Contains(x.Name));
            return sentFolder;
        }

        public void DeleteEmail(EmailType emailType, UniqueId uniqueId)
        {
            if(emailType== EmailType.Inbox)
            {
                if (!client.Inbox.IsOpen)
                    client.Inbox.Open(FolderAccess.ReadWrite);

                client.Inbox.AddFlags(uniqueId, MessageFlags.Deleted, true);
                client.Inbox.Expunge();
            }
            else if(emailType == EmailType.SentEmails)
            {
                if (!SentFolder.IsOpen)
                    SentFolder.Open(FolderAccess.ReadWrite);

                SentFolder.AddFlags(uniqueId, MessageFlags.Deleted, true);
                SentFolder.Expunge();
            }
        }
        public void SetMessageSeen(EmailType emailType, UniqueId uniqueId)
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

        public void AddMessageToFolder(EmailType emailType, MimeMessage mimeMessage)
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
        public MimeMessage GetEmail(EmailType emailType, UniqueId uniqueId)
        {
            if (emailType == EmailType.Inbox)
            {
                if (!client.Inbox.IsOpen)
                    client.Inbox.Open(FolderAccess.ReadWrite);

                return client.Inbox.GetMessage(uniqueId);
            }
            else if(emailType == EmailType.SentEmails)
            {
                if (!SentFolder.IsOpen)
                    SentFolder.Open(FolderAccess.ReadWrite);

                return SentFolder.GetMessage(uniqueId);
            }

            return null;
        }
    }
}
