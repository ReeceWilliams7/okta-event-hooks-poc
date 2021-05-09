namespace OktaEventHookFunctionApp.Options
{
    public class ZendeskApiOptions
    {
        public string Url { get; set; }

        public string Username { get; set; }

        public string Token { get; set; }

        public bool MockCalls { get; set; }
    }
}
