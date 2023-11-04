namespace CXChange.Modules.UserAccess.Integration.IntegrationEvents
{
    public record UserLoginConfirmedIntegrationEvent
    {
        public Guid Id { get; init; }
        public string PhoneNumber { get; init; }
    }
}
