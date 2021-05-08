using System.Threading.Tasks;

using Okta.Sdk;

namespace OktaEventHookFunctionApp.Services.Okta
{
    public class OktaUserService : IOktaUserService
    {
        private readonly IOktaClientFactory _oktaClientFactory;

        public OktaUserService(IOktaClientFactory oktaClientFactory)
        {
            _oktaClientFactory = oktaClientFactory;
        }

        public async Task<IUser> GetUserAsync(string id)
        {
            var oktaClient = _oktaClientFactory.Create();

            var user = await oktaClient.Users.GetUserAsync(id);

            return user;
        }
    }
}
