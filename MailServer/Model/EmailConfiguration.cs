using System;
using DFCommonLib.Config;

namespace MailServer.Model
{
    public class EMailServer
    {
        public string SmtpServer { get; set; }
        public int SmtpPort { get; set; }
        public string SmtpUsername { get; set; }
        public string SmtpPassword { get; set; }

        public string PopServer { get; set; }
        public int PopPort { get; set; }
        public string PopUsername { get; set; }
        public string PopPassword { get; set; }
    }

    public class EmailConfiguration : AppSettings
    {
        public EMailServer MailServer { get; set; }
    }
}