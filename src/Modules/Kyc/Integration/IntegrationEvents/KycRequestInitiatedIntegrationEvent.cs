namespace CXChange.Modules.Kyc.Integration.IntegrationEvents
{
    public record KycRequestInitiatedIntegrationEvent
    {
        public Guid UserId { get; init; }

    }
}
