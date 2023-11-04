namespace CXChange.Modules.Kyc.Integration.IntegrationEvents
{
    public record KycRejectedIntegrationEvent
    {
        public Guid UserId { get; init; }
    }
}
