using CXChange.BuildingBlocks.Domain;
using CXChange.Modules.Payment.Domain.Currency.ValueObjects;
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
        public void ConfirmTomanDeposit(string trackId)
        {
            if (trackId == null) { throw new ArgumentNullException(nameof(trackId)); }
            CheckRule(new DepositCurrencyShouldBeTomanOnTomanDepositConfirm(Amount.Currency));
            CheckRule(new IRTDepositShouldBeInProgressOnIRTDepositConfirmation(Status));

            Status = DepositStatus.Done;
        }
        public void SetTrackId(string trackId)
        {
            TrackId = new DepositTrackId(trackId);
            Status = DepositStatus.InProgress;
        }

    }
}
