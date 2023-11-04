namespace CXChange.Modules.UserAccess.Integration.IntegrationEvents
{
    public record EmailActivatedIntegrationEvent
    {
        public string Email { get; init; }
        public Guid Id { get; init; }
    }
}
