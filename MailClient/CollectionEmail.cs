using MailKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailClient
{
    public class CollectionEmail
    {
        public string Id { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
  
        public DateTime Date { get; set; }
        public EmailType EmailType { get; set; }
        public string CustomFolderName { get; set; }
        public UniqueId UniqueId { get; set; }
    }
}
