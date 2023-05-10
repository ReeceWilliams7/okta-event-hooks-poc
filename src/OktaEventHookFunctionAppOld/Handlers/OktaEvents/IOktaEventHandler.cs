using System.Threading.Tasks;

namespace OktaEventHookFunctionApp.Handlers.OktaEvents
{
    public interface IOktaEventHandler
    {
        bool CanHandle(string eventType);

        Task HandleAsync(OktaEvent oktaEvent); 
    }
}
