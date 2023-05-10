﻿using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

using OktaEventHookFunctionApp;
using OktaEventHookFunctionApp.Handlers.OktaEventHooks;
using OktaEventHookFunctionApp.Handlers.OktaEvents;
using OktaEventHookFunctionApp.Options;
using OktaEventHookFunctionApp.Services.Okta;

[assembly: FunctionsStartup(typeof(Startup))]

namespace OktaEventHookFunctionApp
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var configuration = builder.GetContext().Configuration;

            builder.Services.AddOptions();

            builder.Services.Configure<OktaApiOptions>(configuration.GetSection(nameof(OktaApiOptions)));
            builder.Services.Configure<ZendeskApiOptions>(configuration.GetSection(nameof(ZendeskApiOptions)));

            builder.Services.AddScoped<IOktaEventHookHandler, OktaEventHookHandler>();

            builder.Services.Scan(
                x => x.FromAssemblyOf<IOktaEventHandler>()
                .AddClasses(c => c.AssignableTo<IOktaEventHandler>())
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            builder.Services.AddScoped<IOktaClientFactory, OktaClientFactory>();
            builder.Services.AddScoped<IOktaUserService, OktaUserService>();          
        }
    }
}
