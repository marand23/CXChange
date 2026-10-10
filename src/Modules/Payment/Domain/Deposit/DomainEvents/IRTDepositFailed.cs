using CXChange.BuildingBlocks.Domain;

namespace CXChange.Modules.Payment.Domain.Deposit.DomainEvents
{
    internal class IRTDepositFailed : IDomainEvent
    {
        public Guid UserId { get; init; }
        public decimal Amount { get; init; }
        public string Reason { get; init; }
    }
}
