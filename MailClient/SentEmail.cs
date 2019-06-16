using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit;

namespace MailClient
{
    public class SentEmail
    {
        public string Id { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime SentTime { get; set; }
        public UniqueId UniqueId { get; internal set; }
        public bool IsRead { get; internal set; }
        private MimeKit.MimeMessage _mimeMessage;
        public MimeKit.MimeMessage ToMimeMessage()
        {
            return _mimeMessage;
        }
        public void AttachMimeMessage(MimeKit.MimeMessage mimeMessage)
        {
            _mimeMessage = mimeMessage;
        }
    }
}
