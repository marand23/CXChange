namespace CXChange.Modules.UserAccess.Integration.IntegrationEvents
{
    public record PasswordResetIntegrationEvent
    {
        public string UserId { get; init; }
    }
}
