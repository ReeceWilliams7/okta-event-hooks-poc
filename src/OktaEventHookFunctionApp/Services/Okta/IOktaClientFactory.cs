
using Okta.Sdk;

namespace OktaEventHookFunctionApp.Services.Okta
{
    public interface IOktaClientFactory
    {
        IOktaClient Create();
    }
}
