﻿using Okta.Sdk.Api;

namespace OktaEventHookFunctionApp.Services.Okta
{
    public interface IOktaClientFactory
    {
        IUserApi CreateUserApi();
    }
}
