namespace CXChange.Modules.Kyc.Integration.IntegrationEvents
{
    public record KycApprovedIntegrationEvent
    {
        public Guid UserId { get; init; }

    }
}
