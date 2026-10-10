using CXChange.BuildingBlocks.Domain;

namespace CXChange.Modules.Payment.Domain.Withdraw.DomainEvents
{
    public record WithdrawConfirmedByUserDomainEvent : IDomainEvent
    {
        public Guid WithdrawId { get; init; }
        public WithdrawConfirmedByUserDomainEvent(Withdraw withdraw)
        {
            WithdrawId = withdraw.Id;
        }

    }
}
