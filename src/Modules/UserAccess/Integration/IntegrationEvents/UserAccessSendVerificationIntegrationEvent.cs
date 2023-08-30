namespace CXChange.Modules.UserAccess.Integration.IntegrationEvents
{
    public class UserAccessSendVerificationIntegrationEvent
    {
        //email or phone number
        public string Destination { get; set; }
        public DestinationType DestinationType { get; set; }
        public string VerificationCode { get; set; }
    }
    public enum DestinationType
    {
        Email = 1,
        Phone
    }
}
