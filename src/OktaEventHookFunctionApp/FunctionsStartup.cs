using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using OktaEventHookFunctionApp;
using OktaEventHookFunctionApp.Handlers.OktaEventHooks;
using OktaEventHookFunctionApp.Handlers.OktaEvents;
using OktaEventHookFunctionApp.Options;
using OktaEventHookFunctionApp.Services.Okta;
using OktaEventHookFunctionApp.Services.Zendesk;

[assembly: FunctionsStartup(typeof(Startup))]

namespace OktaEventHookFunctionApp
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var configuration = builder.GetContext().Configuration;

            builder.Services.AddOptions();

            var zendeskApiOptions = configuration.GetSection(nameof(ZendeskApiOptions)).Get<ZendeskApiOptions>();
            var oktaApiOptions = configuration.GetSection(nameof(OktaApiOptions)).Get<OktaApiOptions>();

            builder.Services.Configure<OktaApiOptions>(configuration.GetSection(nameof(OktaApiOptions)));

            builder.Services.Configure<ZendeskApiOptions>(configuration.GetSection(nameof(ZendeskApiOptions)));

            builder.Services.AddScoped<IOktaEventHookHandler, OktaEventHookHandler>();

            builder.Services.AddScoped<IOktaEventHandler, OktaUserCreatedEventHandler>();
            builder.Services.AddScoped<IOktaEventHandler, OktaUserProfileUpdatedEventHandler>();

            builder.Services.AddScoped<IOktaClientFactory, OktaClientFactory>();
            builder.Services.AddScoped<IOktaUserService, OktaUserService>();

            builder.Services.AddScoped<IZendeskApiFactory, ZendeskApiFactory>();
            builder.Services.AddScoped<IZendeskUserService, ZendeskUserService>();
        }
    }
}
