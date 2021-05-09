using System;
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
        private const string OktaVerificationChallengeHeader = "x-okta-verification-challenge";

        private readonly ILogger<UserEventsHttpFunction> _logger;

        public UserEventsHttpFunction(ILogger<UserEventsHttpFunction> logger)
        {
            _logger = logger;
        }

        [FunctionName("UserEvents")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            [Queue("oktauserevents", Connection = "ConnectionStrings:oktauserevents")] ICollector<string> outputQueue)
        {
            _logger.LogInformation("Received a new Okta Event Hook Request");

            return HttpMethods.IsGet(req.Method) 
                ? ProcessOktaVerificationChallenge(req) 
                : await ProcessOktaEventHook(req, outputQueue);
        }

        private IActionResult ProcessOktaVerificationChallenge(HttpRequest req)
        {
            _logger.LogInformation("Request is a GET - checking for Okta Verification Challenge header");
            var verificationHeader = req.Headers[OktaVerificationChallengeHeader];

            if (!verificationHeader.Any())
            {
                _logger.LogInformation("Okta Verification Challenge header not found, returning 400 Status");
                return new BadRequestResult();
            }

            _logger.LogInformation("Okta Verification Challenge header found, extracting value and returning as response");
            var verificationValue = verificationHeader.First();

            var verification = new { verification = verificationValue };

            return new OkObjectResult(verification);
        }

        private async Task<IActionResult> ProcessOktaEventHook(HttpRequest req, ICollector<string> outputQueue)
        {
            _logger.LogInformation("Reading body from request");
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            _logger.LogInformation("Adding Okta Event Hook to output queue");
            outputQueue.Add(requestBody);
            return new OkResult();
        }
    }
}
