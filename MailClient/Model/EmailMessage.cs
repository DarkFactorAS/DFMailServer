using System.Collections.Generic;
using System;
using System.Text;
using System.Linq;

namespace DarkFactor.MailClient
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

        public void AddSender(string name, string email)
        {
            FromAddresses.Add( new EmailAddress(name, email));
        }

        public void AddReceiver(string name, string email)
        {
            ToAddresses.Add( new EmailAddress(name, email));
        }
    }
}