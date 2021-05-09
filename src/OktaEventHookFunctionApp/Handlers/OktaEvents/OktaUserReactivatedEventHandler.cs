using System;
using System.Threading.Tasks;

namespace OktaEventHookFunctionApp.Handlers.OktaEvents
{
    public class OktaUserReactivatedEventHandler : IOktaEventHandler
    {
        public bool CanHandle(string eventType)
        {
            throw new NotImplementedException();
        }

        public Task HandleAsync(OktaEvent oktaEvent)
        {
            throw new NotImplementedException();
        }
    }
}
