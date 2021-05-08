using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace OktaEventHookFunctionApp
{
    public class UserEventsHttpFunction
    {
        private readonly ILogger<UserEventsHttpFunction> _logger;

        public UserEventsHttpFunction(ILogger<UserEventsHttpFunction> logger)
        {
            _logger = logger;
        }

        [FunctionName("UserEvents")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            [Queue("oktaeventhooks", Connection = "ConnectionStrings:oktaeventhooks-queue")] ICollector<string> outputQueue)
        {
            if (HttpMethods.IsGet(req.Method))
            {
                var verificationHeader = req.Headers["x-okta-verification-challenge"];

                if (!verificationHeader.Any())
                {
                    return new BadRequestResult();
                }

                var verificationValue = verificationHeader.First();

                var verification = new { verification = verificationValue };

                return new OkObjectResult(verification);
            }

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            outputQueue.Add(requestBody);
            return new OkResult();
        }
    }
}
