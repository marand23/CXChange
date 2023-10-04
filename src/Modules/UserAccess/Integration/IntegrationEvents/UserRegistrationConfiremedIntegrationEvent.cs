namespace CXChange.Modules.UserAccess.Integration.IntegrationEvents
{
    public record UserRegistrationConfiremedIntegrationEvent
    {
        public string PhoneNumber { get; init; }

    }
}
