using CXChange.BuildingBlocks.Domain;

namespace CXChange.Modules.Payment.Domain.Deposit.DomainEvents
{
    public record IRTDepositConfirmed : IDomainEvent
    {
        public Guid UserId { get; init; }
        public decimal Amount { get; init; }

    }
}
