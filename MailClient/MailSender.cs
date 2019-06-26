using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit;
using MailKit.Net.Smtp;
using MimeKit;

namespace MailClient
{
    public interface IMailSender
    {
        void Connect();
        void Disconnect();
        MimeMessage Send(string from, string to, string subject, string body, string attachments);

    }
    public class MailSender: IMailSender
    {
        SmtpClient client = new SmtpClient();
        private string mailServer;
        private int port;
        private bool ssl;
        private string login;
        private string password;
        public MailSender(string mailServer, int port, bool ssl, string login, string password)
        {
            this.mailServer = mailServer;
            this.port = port;
            this.ssl = ssl;
            this.login = login;
            this.password = password;
        }
        public MimeMessage Send(string from, string to, string subject, string body, string attachments)
        {
            var message = new MimeMessage();

            foreach (var address in from.Split(';'))
            {
                message.From.Add(new MailboxAddress(address));
            }

            foreach (var address in to.Split(';'))
            {
                message.To.Add(new MailboxAddress(address));
            }

            message.Subject = subject;

            var builder = new BodyBuilder();

            // Set the plain-text version of the message text
            builder.TextBody = body;

            if (!string.IsNullOrEmpty(attachments))
            {
                foreach (var attachment in attachments.Split(';'))
                {
                    builder.Attachments.Add(attachment);
                }
            }
            // Now we just need to set the message body and we're done
            message.Body = builder.ToMessageBody();

            client.Send(message);

            return message;


        }

        public void Connect()
        {
            client.Connect(mailServer, port, ssl);
            client.AuthenticationMechanisms.Remove("XOAUTH2");

            client.Authenticate(login, password);
        }
        public void Disconnect()
        {
            client.Disconnect(true);
        }
    }
}
