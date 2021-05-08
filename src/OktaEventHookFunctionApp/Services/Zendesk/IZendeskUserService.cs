using System.Threading.Tasks;

using OktaSdk = Okta.Sdk;

namespace OktaEventHookFunctionApp.Services.Zendesk
{
    public interface IZendeskUserService
    {
        Task CreateUserAsync(OktaSdk.IUser oktaUser);

        Task UpdateUserAsync();

        Task<ZendeskApi_v2.Models.Users.User> GetUserAsync();
    }
}
