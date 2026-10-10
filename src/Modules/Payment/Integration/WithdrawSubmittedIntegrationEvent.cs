namespace CXChange.Modules.Payment.Integration
{
    public record WithdrawSubmittedIntegrationEvent
    {
        public Guid WithdrawId { get; init; }
    }
}
