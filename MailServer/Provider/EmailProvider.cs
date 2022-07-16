using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
//using DarkFactorCoreNet.ConfigModel;
using MailKit.Net.Pop3;
using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;
using Microsoft.Extensions.Options;
using MailServer.Model;

using DFCommonLib.Config;

// https://dotnetcoretutorials.com/2017/01/11/sending-receiving-email-net-core/

namespace MailServer.Provider
{
    public interface IEmailProvider
    {
        void Send(EmailMessage emailMessage);
        List<EmailMessage> ReceiveEmail(int maxCount = 10);
    }
 
    public class EmailProvider : IEmailProvider
    {
        private readonly EmailConfiguration _emailConfiguration;

        public EmailProvider(IConfigurationHelper configuration)
        {
            _emailConfiguration = configuration.GetFirstCustomer() as EmailConfiguration;
        }

        public List<EmailMessage> ReceiveEmail(int maxCount = 10)
        {
            using (var emailClient = new Pop3Client())
            {
                emailClient.Connect(_emailConfiguration.PopServer, _emailConfiguration.PopPort, true);
                emailClient.AuthenticationMechanisms.Remove("XOAUTH2");
                emailClient.Authenticate(_emailConfiguration.PopUsername, _emailConfiguration.PopPassword);
 
                List<EmailMessage> emails = new List<EmailMessage>();
                for(int i=0; i < emailClient.Count && i < maxCount; i++)
                {
                    var message = emailClient.GetMessage(i);
                    var emailMessage = new EmailMessage
                    {
                        Content = !string.IsNullOrEmpty(message.HtmlBody) ? message.HtmlBody : message.TextBody,
                        Subject = message.Subject
                    };
                    emailMessage.ToAddresses.AddRange(message.To.Select(x => (MailboxAddress)x).Select(x => new EmailAddress { Email = x.Address, Name = x.Name }));
                    emailMessage.FromAddresses.AddRange(message.From.Select(x => (MailboxAddress)x).Select(x => new EmailAddress { Email = x.Address, Name = x.Name }));
                    emails.Add(emailMessage);
                }

                return emails;
            }
        }
    
        public void Send(EmailMessage emailMessage)
        {
            var message = new MimeMessage();
            message.To.AddRange(emailMessage.ToAddresses.Select(x => new MailboxAddress(x.Name, x.Email)));
            message.From.AddRange(emailMessage.FromAddresses.Select(x => new MailboxAddress(x.Name, x.Email)));
            message.Subject = emailMessage.Subject;
            message.Body = new TextPart(TextFormat.Html)
            {
                Text = emailMessage.Content
            };

            using (var emailClient = new SmtpClient())
            {
                //The last parameter here is to use SSL (Which you should!)
                emailClient.Connect(_emailConfiguration.SmtpServer, _emailConfiguration.SmtpPort, true);

                //Remove any OAuth functionality as we won't be using it. 
                emailClient.AuthenticationMechanisms.Remove("XOAUTH2");
                emailClient.Authenticate(_emailConfiguration.SmtpUsername, _emailConfiguration.SmtpPassword);
                emailClient.Send(message);
                emailClient.Disconnect(true);
            }
        }
    }
}