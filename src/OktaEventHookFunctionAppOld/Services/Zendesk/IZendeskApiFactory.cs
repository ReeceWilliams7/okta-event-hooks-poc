
using ZendeskApi_v2;

namespace OktaEventHookFunctionApp.Services.Zendesk
{
    public interface IZendeskApiFactory
    {
        IZendeskApi Create();
    }
}
