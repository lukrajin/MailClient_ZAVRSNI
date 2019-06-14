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
        private int port;
        private bool ssl;
        public string Login { get; set; }
        public string Password { get; set; }
        public IMailFolder SentFolder { get; private set; }

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
        public ConcurrentBag<InboxEmail> GetInboxEmailList()
        {
            var mailMessages = new ConcurrentBag<InboxEmail>();
            var inbox = client.Inbox;
            inbox.Open(FolderAccess.ReadOnly);

            var results = inbox.Search(SearchQuery.All);

            foreach (var uniqueId in results)
            {
                var message = inbox.GetMessage(uniqueId);
                mailMessages.Add(new InboxEmail
                {
                    Id= message.MessageId,
                    ArrivalTime = message.Date.ToString(),
                    From = ((MailboxAddress)message.From[0]).Address,
                    To = ((MailboxAddress)message.To[0]).Address,
                    Subject = message.Subject,
                    Body =message.TextBody 
                });
            }

            return mailMessages;
        }
        public ConcurrentBag<SentEmail> GetSentEmailList()
        {
            var mailMessages = new ConcurrentBag<SentEmail>();

            SentFolder = GetSentEmailsFolder();
            SentFolder.Open(FolderAccess.ReadOnly);

            var results = SentFolder.Search(SearchQuery.All);

            foreach (var uniqueId in results)
            {
                var message = SentFolder.GetMessage(uniqueId);
                mailMessages.Add(new SentEmail
                {
                    Id = message.MessageId,
                    SentTime = message.Date.ToString(),
                    From = ((MailboxAddress)message.From[0]).Address,
                    To = ((MailboxAddress)message.To[0]).Address,
                    Subject = message.Subject,
                    Body = message.TextBody
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
    }
}
