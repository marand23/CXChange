namespace CXChange.Modules.UserAccess.Integration.IntegrationEvents
{
    public record PasswordResetIntegrationEvent
    {
        public Guid UserId { get; init; }
    }
}
