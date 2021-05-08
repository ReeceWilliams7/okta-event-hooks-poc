using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using OktaEventHookFunctionApp.Services.Okta;
using OktaEventHookFunctionApp.Services.Zendesk;

namespace OktaEventHookFunctionApp.Handlers.OktaEvents
{
    public class OktaUserCreatedEventHandler : IOktaEventHandler
    {
        private const string HandlerEventType = "user.lifecycle.create";

        private readonly ILogger<OktaUserCreatedEventHandler> _logger;

        private readonly IOktaUserService _oktaUserService;

        private readonly IZendeskUserService _zendeskUserService;

        public OktaUserCreatedEventHandler(ILogger<OktaUserCreatedEventHandler> logger, IOktaUserService oktaUserService, IZendeskUserService zendeskUserService)
        {
            _logger = logger;
            _oktaUserService = oktaUserService;
            _zendeskUserService = zendeskUserService;
        }

        public bool CanHandle(string eventType) => eventType.Equals(HandlerEventType);

        public async Task HandleAsync(OktaEvent oktaEvent)
        {
            foreach (var target in oktaEvent.Target)
            {
                var oktaUser = await _oktaUserService.GetUserAsync(target.Id);

                await _zendeskUserService.CreateUserAsync(oktaUser);
            }

           

            await Task.CompletedTask;
        }
    }
}
