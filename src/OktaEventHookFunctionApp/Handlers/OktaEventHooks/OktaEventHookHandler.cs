using System.Collections.Generic;
using System.Linq;
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
            _logger.LogInformation("Processing events");

            if (oktaEventHookEvent == null
                || oktaEventHookEvent.Data == null
                || oktaEventHookEvent.Data.Events == null
                || !oktaEventHookEvent.Data.Events.Any())
            {
                _logger.LogWarning("oktaEventHookEvent is null, has no data or has no events");
                return;
            }

            foreach (var evt in oktaEventHookEvent.Data.Events)
            {
                _logger.LogInformation($"Looking for handlers to process event type {evt.EventType}");

                var handlerCount = 0;
                foreach (var handler in _oktaEventHandlers)
                {
                    if (handler.CanHandle(evt.EventType))
                    {
                        _logger.LogInformation($"Found handler {handler.GetType().Name} for event type {evt.EventType}");
                        handlerCount++;
                        await handler.HandleAsync(evt);
                    }
                }

                if (handlerCount == 0)
                {
                    _logger.LogInformation($"No handlers found that can process event type {evt.EventType}");
                }
            }

            await Task.CompletedTask;
        }
    }
}
