using Okta.Sdk.Model;

using System.Threading.Tasks;

namespace OktaEventHookFunctionApp.Services.Okta
{
    public interface IOktaUserService
    {
        Task<User> GetUserAsync(string id);
    }
}
