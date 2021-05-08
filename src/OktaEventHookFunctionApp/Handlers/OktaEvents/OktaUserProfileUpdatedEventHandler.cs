using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

namespace OktaEventHookFunctionApp.Handlers.OktaEvents
{
    public class OktaUserProfileUpdatedEventHandler : IOktaEventHandler
    {
        private const string HandlerEventType = "user.account.update_profile";

        private readonly ILogger<OktaUserProfileUpdatedEventHandler> _logger;

        public OktaUserProfileUpdatedEventHandler(ILogger<OktaUserProfileUpdatedEventHandler> logger)
        {
            _logger = logger;
        }

        public bool CanHandle(string eventType) => eventType.Equals(HandlerEventType);

        public async Task HandleAsync(OktaEvent oktaEvent)
        {
            _logger.LogInformation(oktaEvent.Target.First().Id);

            await Task.CompletedTask;
        }
    }
}
