namespace CXChange.Modules.UserAccess.Integration.IntegrationEvents
{
    public record EmailActivatedIntegrationEvent
    {
        public string Email { get; set; }
        public string Id { get; set; }
    }
}
