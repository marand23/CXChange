namespace CXChange.Modules.UserAccess.Integration.IntegrationEvents
{
    public class UserAccessSendVerificationIntegrationEvent
    {
        //email or phone number
        public string Destination { get; init; }
        public DestinationType DestinationType { get; init; }
        public string VerificationCode { get; init; }
    }
    public enum DestinationType
    {
        Email = 1,
        Phone
    }
}
