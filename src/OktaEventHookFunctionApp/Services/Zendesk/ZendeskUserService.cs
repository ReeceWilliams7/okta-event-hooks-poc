using System;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using ZendeskApi_v2.Models.Users;

using OktaSdk = Okta.Sdk;

namespace OktaEventHookFunctionApp.Services.Zendesk
{
    public class ZendeskUserService : IZendeskUserService
    {
        private readonly IZendeskApiFactory _zendeskApiFactory;

        private readonly ILogger<ZendeskUserService> _logger;

        public ZendeskUserService(IZendeskApiFactory zendeskApiFactory, ILogger<ZendeskUserService> logger)
        {
            _zendeskApiFactory = zendeskApiFactory;
            _logger = logger;
        }

        public async Task CreateUserAsync(OktaSdk.IUser oktaUser)
        {
            var zendeskApi = _zendeskApiFactory.Create();

            var zendeskUser = new User
            {
                Email = oktaUser.Profile.Email,
                Name = $"{oktaUser.Profile.FirstName} {oktaUser.Profile.LastName}",
                ExternalId = oktaUser.Id,
                Verified = true
            };

            try
            {
                var userResponse = await zendeskApi.Users.CreateUserAsync(zendeskUser);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public Task<User> GetUserAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateUserAsync()
        {
            throw new NotImplementedException();
        }
    }
}
