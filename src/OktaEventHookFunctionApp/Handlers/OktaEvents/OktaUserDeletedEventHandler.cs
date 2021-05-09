using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using OktaEventHookFunctionApp.Services.Zendesk;

namespace OktaEventHookFunctionApp.Handlers.OktaEvents
{
    public class OktaUserDeletedEventHandler : IOktaEventHandler
    {
        private const string HandlerEventType = "user.lifecycle.delete.initiated";

        private readonly ILogger<OktaUserCreatedEventHandler> _logger;

        private readonly IZendeskUserService _zendeskUserService;

        public OktaUserDeletedEventHandler(ILogger<OktaUserCreatedEventHandler> logger, IZendeskUserService zendeskUserService)
        {
            _logger = logger;
            _zendeskUserService = zendeskUserService;
        }

        public bool CanHandle(string eventType) => eventType.Equals(HandlerEventType);

        public async Task HandleAsync(OktaEvent oktaEvent)
        {
            _logger.LogInformation($"Handling event type {oktaEvent.EventType}");

            _logger.LogInformation($"{oktaEvent.Target.Count} targets found for event");
            foreach (var target in oktaEvent.Target)
            {
                _logger.LogInformation($"Deleting Zendesk user for deleted Okta User with id {target.Id}");
                await _zendeskUserService.SuspendUserAsync(target.Id);
            }
        }
    }
}
