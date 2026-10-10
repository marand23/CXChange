using CXChange.BuildingBlocks.Domain;
using CXChange.Modules.Payment.Domain.Currency.ValueObjects;
using CXChange.Modules.Payment.Domain.Deposit.DomainEvents;
using CXChange.Modules.Payment.Domain.Deposit.Enums;
using CXChange.Modules.Payment.Domain.Deposit.Specifications;
using CXChange.Modules.Payment.Domain.Deposit.ValueObjects;
using CXChange.Modules.Payment.Domain.ValueObjects;

namespace CXChange.Modules.Payment.Domain.Deposit
{
    public class Deposit : Entity<Guid>, IAggregateRoot
    {
        private Deposit() { }
        private Deposit(Guid id, decimal amount, Currency.ValueObjects.Currency currency, Guid walletId, Guid userId, DepositAddress address)
        {

            Amount = new MonetaryValue(amount, currency);
            WalletId = walletId;
            UserId = userId;
            Id = id;
            Status = DepositStatus.InProgress;
            Address = address;
            TrackId = new DepositTrackId("");

        }

        public MonetaryValue Amount { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; }
        public Guid WalletId { get; private set; }
        public Guid UserId { get; private set; }
        public DepositTrackId TrackId { get; private set; }
        public DepositStatus Status { get; private set; }
        public DepositAddress Address { get; private set; }
        public static Deposit Create(Guid id, decimal amount, Currency.ValueObjects.Currency currency, Guid walletId, Guid userId, DepositAddress address)
        {
            return new Deposit(id, amount, currency, walletId, userId, address);
        }
        public void ConfirmIRTDeposit()
        {
            CheckRule(new DepositCurrencyShouldBeIRTOnIRTDepositConfirm(Amount.Currency));
            CheckRule(new IRTDepositShouldBeInProgressOnIRTDepositConfirmation(Status));
            Status = DepositStatus.Done;
            AddDomainEvent(new IRTDepositConfirmed { Amount = this.Amount.Value, UserId = UserId });
        }
        public void RejectIRTDeposit(string reason)
        {
            CheckRule(new IRTDepositShouldBEInProgressOnDepositRejection(Status));
            Status = DepositStatus.Failed;
            AddDomainEvent(new IRTDepositFailed { Amount = Amount.Value, Reason = reason, UserId = UserId });
        }
        public void SetTrackId(string trackId)
        {
            TrackId = new DepositTrackId(trackId);

        }

    }

}
