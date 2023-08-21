namespace CXChange.Modules.UserAccess.Integration.IntegrationEvents
{
    public record UserRegisteredIntegrationEvent
    {
        public string Id { get; set; }
        public string PhoneNumber { get; set; }
        public string VerificationCode { get; set; }
    }
}
