using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using ZendeskApi_v2;
using ZendeskApi_v2.Models.Users;

using OktaSdk = Okta.Sdk;

namespace OktaEventHookFunctionApp.Services.Zendesk
{
    public class ZendeskUserService : IZendeskUserService
    {
        private readonly ILogger<ZendeskUserService> _logger;

        private readonly IZendeskApiFactory _zendeskApiFactory;

        public ZendeskUserService(ILogger<ZendeskUserService> logger, IZendeskApiFactory zendeskApiFactory)
        {
            _logger = logger;
            _zendeskApiFactory = zendeskApiFactory;
        }

        public async Task CreateUserAsync(OktaSdk.IUser oktaUser)
        {
            var zendeskApi = CreateZendeskApiClient();

            _logger.LogDebug("Building Zendesk User type from Okta User");
            var zendeskUser = new User
            {
                Email = oktaUser.Profile.Email,
                Name = $"{oktaUser.Profile.FirstName} {oktaUser.Profile.LastName}",
                ExternalId = oktaUser.Id,
                Verified = true
            };

            _logger.LogInformation($"Calling Zendesk API to create User with Okta id {oktaUser.Id}");
            var userResponse = await zendeskApi.Users.CreateUserAsync(zendeskUser);

            if (userResponse != null || userResponse.User.Id.HasValue)
            {
                _logger.LogInformation($"Zendesk User successfully created for Okta user with id {oktaUser.Id}");
            }
            else
            {
                _logger.LogWarning($"Failed to create Zendesk User for Okta user with id {oktaUser.Id}");
            }
        }

        public async Task UpdateUserAsync(OktaSdk.IUser oktaUser)
        {
            var zendeskApi = CreateZendeskApiClient();

            var zendeskUser = await GetUserAsync(oktaUser.Id);

            zendeskUser.Email = oktaUser.Profile.Email;
            zendeskUser.Name = $"{oktaUser.Profile.FirstName} {oktaUser.Profile.LastName}";

            _logger.LogInformation($"Calling Zendesk API to update User with Okta id {oktaUser.Id}");

            var userResponse = await zendeskApi.Users.UpdateUserAsync(zendeskUser);

            if (userResponse != null || userResponse.User.Id.HasValue)
            {
                _logger.LogInformation($"Zendesk User successfully updated for Okta user with id {oktaUser.Id}");
            }
            else
            {
                _logger.LogWarning($"Failed to update Zendesk User for Okta user with id {oktaUser.Id}");
            }
        }

        public async Task SuspendUserAsync(string oktaUserId)
        {
            var zendeskApi = CreateZendeskApiClient();

            var zendeskUser = await GetUserAsync(oktaUserId);

            await zendeskApi.Users.SuspendUserAsync(zendeskUser.Id.Value);
        }

        public async Task UnsuspendUserAsync(string oktaUserId)
        {
            var zendeskApi = CreateZendeskApiClient();

            var zendeskUser = await GetUserAsync(oktaUserId);
            zendeskUser.Suspended = false;

            await zendeskApi.Users.UpdateUserAsync(zendeskUser);
        }

        public async Task DeleteUserAsync(string oktaUserId)
        {
            var zendeskApi = CreateZendeskApiClient();

            var zendeskUser = await GetUserAsync(oktaUserId);

            await zendeskApi.Users.DeleteUserAsync(zendeskUser.Id.Value);
        }

        private IZendeskApi CreateZendeskApiClient()
        {
            _logger.LogDebug("Creating ZendeskApi client");
            return _zendeskApiFactory.Create();
        }

        private async Task<User> GetUserAsync(string oktaUserId)
        {
            var zendeskApi = CreateZendeskApiClient();

            var foundUsers = await zendeskApi.Users.SearchByExternalIdAsync(oktaUserId);
            return foundUsers.Users.First();
        }
    }
}
