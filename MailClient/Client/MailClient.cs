
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using DFCommonLib.HttpApi;
using Newtonsoft.Json;

using MailClientModule.Model;
using MailClientModule.RestClient;

namespace MailClientModule.Client
{
    public interface IMailClient : IDFClient
    {
        Task<WebAPIData> SendEmail(EmailMessage message);
    }

    public class MailClient : DFClient, IMailClient
    {
        IMailRestClient _restClient;

        public MailClient(IMailRestClient restClient) : base(restClient)
        {
            _restClient = restClient;
        }

        public Task<WebAPIData> SendEmail(EmailMessage mailMessage)
        {
            return _restClient.SendEmail(mailMessage);
        }

        public static void SetupService( IServiceCollection services )
        {
            services.AddTransient<IMailRestClient, MailRestClient>();
            services.AddTransient<IMainClient, MainClient>();
        }
    }
}