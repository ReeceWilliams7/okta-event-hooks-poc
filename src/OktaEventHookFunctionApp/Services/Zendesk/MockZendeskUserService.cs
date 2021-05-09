using System;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using Okta.Sdk;

namespace OktaEventHookFunctionApp.Services.Zendesk
{
    public class MockZendeskUserService : IZendeskUserService
    {
        private readonly ILogger<MockZendeskUserService> _logger;

        public MockZendeskUserService(ILogger<MockZendeskUserService> logger)
        {
            _logger = logger;
        }

        public Task CreateUserAsync(IUser oktaUser)
        {
            _logger.LogInformation("Mocking Create User call");
            return Task.CompletedTask;
        }

        public Task DeleteUserAsync(string oktaUserId)
        {
            _logger.LogInformation("Mocking Delete User call");
            return Task.CompletedTask;
        }

        public Task SuspendUserAsync(string oktaUserId)
        {
            _logger.LogInformation("Mocking Suspend User call");
            return Task.CompletedTask;
        }

        public Task UnsuspendUserAsync(string oktaUserId)
        {
            _logger.LogInformation("Mocking Unsuspend User call");
            return Task.CompletedTask;
        }

        public Task UpdateUserAsync(IUser oktaUser)
        {
            _logger.LogInformation("Mocking Update User call");
            return Task.CompletedTask;
        }
    }
}
