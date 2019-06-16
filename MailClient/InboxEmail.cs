using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit;

namespace MailClient
{
    public class InboxEmail
    {
        public string Id { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime ArrivalTime { get; set; }
        public bool IsRead { get; set; }
        public UniqueId UniqueId { get; internal set; }
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
