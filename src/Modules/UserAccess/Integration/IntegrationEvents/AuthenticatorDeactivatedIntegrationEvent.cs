namespace CXChange.Modules.UserAccess.Integration.IntegrationEvents
{
    public record AuthenticatorDeactivatedIntegrationEvent
    {
        public Guid UserId { get; init; }
    }
}
