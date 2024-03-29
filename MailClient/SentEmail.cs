﻿using System;
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
        public DateTime SentTime { get; set; }
        public UniqueId UniqueId { get; internal set; }
        public bool IsRead { get; internal set; }
        public string PreviewText { get; set; }
    }


}
