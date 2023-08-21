namespace CXChange.Modules.UserAccess.Integration.IntegrationEvents
{
    public record UserLoginConfirmedIntegrationEvent
    {
        public string Id { get; set; }
        public string PhoneNumber { get; set; }
    }
}
