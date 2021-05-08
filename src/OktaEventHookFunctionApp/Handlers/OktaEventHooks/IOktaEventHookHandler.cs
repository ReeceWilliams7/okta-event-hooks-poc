using System.Threading.Tasks;

namespace OktaEventHookFunctionApp.Handlers.OktaEventHooks
{
    public interface IOktaEventHookHandler
    {
        Task HandleAsync(OktaEventHookEvent oktaEventHookEvent);
    }
}
