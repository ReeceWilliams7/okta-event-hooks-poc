using System;
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
        public async Task Run([QueueTrigger("oktaeventhooks", Connection = "ConnectionStrings:oktaeventhooks-queue")]string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");

            try
            {
                var oktaEventHookEvent = JsonConvert.DeserializeObject<OktaEventHookEvent>(myQueueItem);

                await _oktaEventHookHander.HandleAsync(oktaEventHookEvent);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
