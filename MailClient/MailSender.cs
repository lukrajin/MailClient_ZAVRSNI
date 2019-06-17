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
    public class MailSender
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
        public MimeMessage Send(string from, string to, string subject, string body)
        {
            var message = new MimeMessage();

            foreach (var address in from.Split(';'))
            {
                message.From.Add(new MailboxAddress(address));
            }

            foreach(var address in to.Split(';'))
            {
                message.To.Add(new MailboxAddress(address));
            }

            message.Subject = subject;
            message.Body = new TextPart { Text = body };

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
