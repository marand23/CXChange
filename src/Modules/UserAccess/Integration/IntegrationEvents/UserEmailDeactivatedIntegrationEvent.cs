namespace CXChange.Modules.UserAccess.Integration.IntegrationEvents
{
    public record UserEmailDeactivatedIntegrationEvent
    {
        public string UserId { get; init; }
    }
}
