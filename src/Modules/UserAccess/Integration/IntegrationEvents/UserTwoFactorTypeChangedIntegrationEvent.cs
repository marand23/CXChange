namespace CXChange.Modules.UserAccess.Integration.IntegrationEvents
{
    public record UserTwoFactorTypeChangedIntegrationEvent
    {
        public Guid UserId { get; init; }
    }
}
