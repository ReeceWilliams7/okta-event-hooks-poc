
using Microsoft.Extensions.Options;

using OktaEventHookFunctionApp.Options;

using ZendeskApi_v2;

namespace OktaEventHookFunctionApp.Services.Zendesk
{
    public class ZendeskApiFactory : IZendeskApiFactory
    {
        private readonly ZendeskApiOptions _zendeskApiOptions;

        public ZendeskApiFactory(IOptions<ZendeskApiOptions> zendeskApiOptions)
        {
            _zendeskApiOptions = zendeskApiOptions.Value;
        }

        public IZendeskApi Create()
        {
            return new ZendeskApi(
                _zendeskApiOptions.Url,
                _zendeskApiOptions.Username,
                _zendeskApiOptions.Token,
                "en-GB");
        }
    }
}
