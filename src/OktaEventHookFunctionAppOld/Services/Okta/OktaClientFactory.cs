using Microsoft.Extensions.Options;

using Okta.Sdk;
using Okta.Sdk.Configuration;

using OktaEventHookFunctionApp.Options;

namespace OktaEventHookFunctionApp.Services.Okta
{
    public class OktaClientFactory : IOktaClientFactory
    {
        private readonly OktaApiOptions _oktaApiOptions;

        public OktaClientFactory(IOptions<OktaApiOptions> oktaApiOptions)
        {
            _oktaApiOptions = oktaApiOptions.Value;
        }

        public IOktaClient Create()
        {
            var oktaClientConfiguration = new OktaClientConfiguration
            {
                AuthorizationMode = AuthorizationMode.SSWS,
                Token = _oktaApiOptions.Token,
                OktaDomain = _oktaApiOptions.Url
            };

            return new OktaClient(oktaClientConfiguration);
        }
    }
}
