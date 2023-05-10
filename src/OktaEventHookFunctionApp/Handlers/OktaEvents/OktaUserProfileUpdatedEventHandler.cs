﻿using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using OktaEventHookFunctionApp.Services.Okta;

namespace OktaEventHookFunctionApp.Handlers.OktaEvents
{
    public class OktaUserProfileUpdatedEventHandler : IOktaEventHandler
    {
        private const string HandlerEventType = "user.account.update_profile";

        private readonly ILogger<OktaUserProfileUpdatedEventHandler> _logger;

        private readonly IOktaUserService _oktaUserService;

        public OktaUserProfileUpdatedEventHandler(ILogger<OktaUserProfileUpdatedEventHandler> logger, IOktaUserService oktaUserService)
        {
            _logger = logger;
            _oktaUserService = oktaUserService;
        }

        public bool CanHandle(string eventType) => eventType.Equals(HandlerEventType);

        public async Task HandleAsync(OktaEvent oktaEvent)
        {
            _logger.LogInformation($"Handling event type {oktaEvent.EventType}");

            _logger.LogInformation($"{oktaEvent.Target.Count} targets found for event");
            foreach (var target in oktaEvent.Target)
            {
                _logger.LogInformation($"Retrieving user from Okta with id {target.Id}");
                var oktaUser = await _oktaUserService.GetUserAsync(target.Id);
            }
        }
    }
}
