namespace CXChange.Modules.UserAccess.Integration.IntegrationEvents
{
    public record UserEmailDeactivatedIntegrationEvent
    {
        public Guid UserId { get; init; }
    }
}
