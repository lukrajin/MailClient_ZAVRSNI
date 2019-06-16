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
            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = body;
            bodyBuilder.TextBody = body;

            message.From.Add(new MailboxAddress(from));
            message.To.Add(new MailboxAddress(to));
            message.Subject = subject;
            message.Body = bodyBuilder.ToMessageBody();

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
