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

            builder.Services.AddScoped<IZendeskApiFactory, ZendeskApiFactory>();
            var zendeskApiOptions = configuration.GetSection(nameof(ZendeskApiOptions)).Get<ZendeskApiOptions>();
            if (zendeskApiOptions.MockCalls)
            {
                builder.Services.AddScoped<IZendeskUserService, MockZendeskUserService>();
            }
            else
            {
                builder.Services.AddScoped<IZendeskUserService, ZendeskUserService>();
            }

            
            
        }
    }
}
