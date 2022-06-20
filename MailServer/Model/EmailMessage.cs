using System.Collections.Generic;
using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace MailServer.Model
{
    public class EmailMessage
    {
        public List<EmailAddress> ToAddresses { get; set; }
        public List<EmailAddress> FromAddresses { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }

        public EmailMessage()
        {
            ToAddresses = new List<EmailAddress>();
            FromAddresses = new List<EmailAddress>();
        }
    }

}