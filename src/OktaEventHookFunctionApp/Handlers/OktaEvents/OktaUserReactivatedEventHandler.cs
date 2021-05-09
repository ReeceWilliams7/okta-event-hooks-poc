﻿using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using OktaEventHookFunctionApp.Services.Zendesk;

namespace OktaEventHookFunctionApp.Handlers.OktaEvents
{
    public class OktaUserReactivatedEventHandler : IOktaEventHandler
    {
        private const string HandlerEventType = "user.lifecycle.reactivate";

        private readonly ILogger<OktaUserCreatedEventHandler> _logger;

        private readonly IZendeskUserService _zendeskUserService;

        public OktaUserReactivatedEventHandler(ILogger<OktaUserCreatedEventHandler> logger, IZendeskUserService zendeskUserService)
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
                _logger.LogInformation($"Unsuspending Zendesk user from Okta User with id {target.Id}");
                await _zendeskUserService.UnsuspendUserAsync(target.Id);
            }
        }
    }
}
