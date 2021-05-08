using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using OktaEventHookFunctionApp.Handlers.OktaEvents;

namespace OktaEventHookFunctionApp.Handlers.OktaEventHooks
{
    public class OktaEventHookHandler : IOktaEventHookHandler
    {
        private readonly ILogger<OktaEventHookHandler> _logger;

        private readonly IEnumerable<IOktaEventHandler> _oktaEventHandlers;

        public OktaEventHookHandler(ILogger<OktaEventHookHandler> logger, IEnumerable<IOktaEventHandler> oktaEventHandlers)
        {
            _logger = logger;
            _oktaEventHandlers = oktaEventHandlers;
        }

        public async Task HandleAsync(OktaEventHookEvent oktaEventHookEvent)
        {
            foreach (var evt in oktaEventHookEvent?.Data.Events)
            {
                _logger.LogInformation(evt.EventType);

                foreach (var handler in _oktaEventHandlers)
                {
                    if (handler.CanHandle(evt.EventType))
                    {
                        await handler.HandleAsync(evt);
                    }
                }
            }

            await Task.CompletedTask;
        }
    }
}
