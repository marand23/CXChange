namespace CXChange.Modules.UserAccess.Integration.IntegrationEvents
{
    public record UserTwoFactorTypeChangedIntegrationEvent
    {
        public string UserId { get; set; }
    }
}
