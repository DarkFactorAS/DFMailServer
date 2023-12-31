using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DFCommonLib.Config;
using DFCommonLib.HttpApi;
using MailServer.Model;
using MailServer.Provider;

namespace MailServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MailController : ControllerBase
    {
        IEmailProvider _emailProvider;

        public MailController(
            IEmailProvider emailProvider 
            //,IConfigurationHelper configuration
            )
        {
                _emailProvider = emailProvider;

            // var customer = configuration.GetFirstCustomer() as BotCustomer;
            // if ( customer != null )
            // {
            //     _accountClient.SetEndpoint(customer.AccountServer);
            // }
        }

        [HttpPut]
        [Route("SendEmail")]
        public WebAPIData SendEmail(EmailMessage emailMessage)
        {
            return _emailProvider.Send(emailMessage);
        }

        [HttpGet]
        [Route("Ping")]
        public string Ping(string message)
        {
            return "PONG";
        }
    }
}
