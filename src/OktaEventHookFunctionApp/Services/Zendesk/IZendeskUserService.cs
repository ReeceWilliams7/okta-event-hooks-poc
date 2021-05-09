using System.Threading.Tasks;

using OktaSdk = Okta.Sdk;

namespace OktaEventHookFunctionApp.Services.Zendesk
{
    public interface IZendeskUserService
    {
        Task CreateUserAsync(OktaSdk.IUser oktaUser);

        Task UpdateUserAsync(OktaSdk.IUser oktaUser);

        Task SuspendUserAsync(string oktaUserId);

        Task UnsuspendUserAsync(string oktaUserId);

        Task DeleteUserAsync(string oktaUserId);
    }
}
