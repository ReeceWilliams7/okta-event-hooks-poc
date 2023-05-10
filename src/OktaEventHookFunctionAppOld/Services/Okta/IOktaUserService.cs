using System.Threading.Tasks;

using OktaSdk = Okta.Sdk;

namespace OktaEventHookFunctionApp.Services.Okta
{
    public interface IOktaUserService
    {
        Task<OktaSdk.IUser> GetUserAsync(string id);
    }
}
