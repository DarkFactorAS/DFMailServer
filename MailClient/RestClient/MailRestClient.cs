
using System.Threading.Tasks;
using DFCommonLib.HttpApi;
using DFCommonLib.Logger;
using MailClientModule.Model;

namespace MailClientModule.RestClient
{
    public interface IMailRestClient : IDFRestClient
    {
        Task<WebAPIData> SendEmail(EmailMessage message);
    }

    public class MailRestClient : DFRestClient, IMailRestClient
    {
        private const int POST_SENDMAIL = 1;

        public MailRestClient(IDFLogger<DFRestClient> logger ) : base(logger)
        {
        }

        override protected string GetModule()
        {
            return "MailRestClient";
        }

        public async Task<WebAPIData> SendEmail(EmailMessage message)
        {
            var response = await PutJsonData(POST_SENDMAIL,"SendEmail", message);
            return response;
        }
    }
}