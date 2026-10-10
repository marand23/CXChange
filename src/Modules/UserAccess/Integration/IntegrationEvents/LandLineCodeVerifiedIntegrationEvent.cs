namespace CXChange.Modules.UserAccess.Integration.IntegrationEvents
{
    public record LandLineCodeVerifiedIntegrationEvent
    {
        public Guid UserId { get; set; }
        public string LandLine { get; set; }

    }
}
