using System.Threading.Tasks;

using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

using OktaEventHookFunctionApp.Handlers.OktaEventHooks;

namespace OktaEventHookFunctionApp
{
    public class UserEventsQueueFunction
    {
        private readonly ILogger<UserEventsHttpFunction> _logger;

        private readonly IOktaEventHookHandler _oktaEventHookHander;

        public UserEventsQueueFunction(ILogger<UserEventsHttpFunction> logger, IOktaEventHookHandler oktaEventHookHander)
        {
            _logger = logger;
            _oktaEventHookHander = oktaEventHookHander;
        }

        [FunctionName("UserEventsQueue")]
        public async Task Run([QueueTrigger("oktauserevents", Connection = "ConnectionStrings:oktauserevents")] string myQueueItem)
        {
            _logger.LogInformation($"Processing a new message from oktauserevents queue");

            _logger.LogInformation($"Attempting to deserialise message to an instance of {nameof(OktaEventHookEvent)}");
            var oktaEventHookEvent = JsonConvert.DeserializeObject<OktaEventHookEvent>(myQueueItem);

            _logger.LogInformation($"Passing message to registered {nameof(IOktaEventHookHandler)} implementation to begin processing");
            await _oktaEventHookHander.HandleAsync(oktaEventHookEvent);
        }
    }
}
