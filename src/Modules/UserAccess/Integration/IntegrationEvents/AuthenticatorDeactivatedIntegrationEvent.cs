namespace CXChange.Modules.UserAccess.Integration.IntegrationEvents
{
    public record AuthenticatorDeactivatedIntegrationEvent
    {
        public string UserId { get; init; }
    }
}
