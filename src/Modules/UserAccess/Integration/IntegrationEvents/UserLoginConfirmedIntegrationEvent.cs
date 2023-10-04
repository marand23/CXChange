namespace CXChange.Modules.UserAccess.Integration.IntegrationEvents
{
    public record UserLoginConfirmedIntegrationEvent
    {
        public string Id { get; init; }
        public string PhoneNumber { get; init; }
    }
}
