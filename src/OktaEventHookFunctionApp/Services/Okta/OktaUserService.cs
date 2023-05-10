﻿using Okta.Sdk.Model;

using System.Threading.Tasks;

namespace OktaEventHookFunctionApp.Services.Okta
{
    public class OktaUserService : IOktaUserService
    {
        private readonly IOktaClientFactory _oktaClientFactory;

        public OktaUserService(IOktaClientFactory oktaClientFactory)
        {
            _oktaClientFactory = oktaClientFactory;
        }

        public async Task<User> GetUserAsync(string id)
        {
            var userApi = _oktaClientFactory.CreateUserApi();

            var user = await userApi.GetUserAsync(id);

            return user;
        }
    }
}
