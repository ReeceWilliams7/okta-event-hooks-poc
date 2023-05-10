﻿using Microsoft.Extensions.Options;

using Okta.Sdk.Api;
using Okta.Sdk.Client;

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

        public IUserApi CreateUserApi()
        {
            var oktaClientConfiguration = new Configuration
            {
                AuthorizationMode = AuthorizationMode.SSWS,
                Token = _oktaApiOptions.Token,
                OktaDomain = _oktaApiOptions.Url
            };

            return new UserApi();
        }
    }
}
