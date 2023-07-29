namespace CXChange.Modules.UserAccess.Integration.IntegrationEvents
{
    public class UserRegistrationConfirmedIntegrationEvent
    {
        public string Id { get; set; }
        public string PhoneNumber { get; set; }
        public string VerificationCode { get; set; }
    }
}
