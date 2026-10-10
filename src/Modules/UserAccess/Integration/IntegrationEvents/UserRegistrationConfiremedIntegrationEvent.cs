namespace CXChange.Modules.UserAccess.Integration.IntegrationEvents
{
    public record UserRegistrationConfiremedIntegrationEvent
    {
        public Guid UserId { get; init; }
        public string PhoneNumber { get; init; }

    }
}
