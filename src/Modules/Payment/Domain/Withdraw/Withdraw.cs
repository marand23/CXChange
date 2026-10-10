using CXChange.BuildingBlocks.Domain;
using CXChange.Modules.Payment.Domain.Currency.Enums;
using CXChange.Modules.Payment.Domain.Currency.ValueObjects;
using CXChange.Modules.Payment.Domain.Withdraw.DomainEvents;
using CXChange.Modules.Payment.Domain.Withdraw.Enums;
using CXChange.Modules.Payment.Domain.Withdraw.Specifications;
using CXChange.Modules.Payment.Domain.Withdraw.ValueObjects;

namespace CXChange.Modules.Payment.Domain.Withdraw
{
    public class Withdraw : Entity<Guid>, IAggregateRoot
    {
        private Withdraw() { }
        private Withdraw(Guid userId, Guid walletId, Currency.ValueObjects.Currency currency, string address, string networkName, decimal amount)
        {
            UserId = userId;
            WalletId = walletId;
            Currency = currency;
            Address = new WithdrawDestination(address);
            Amount = new MonetaryValue(amount, currency);
            NetworkName = new NetworkName { Name = networkName };
            Status = WithdrawStatus.ConfirmedByUser;
            AddDomainEvent(new WithdrawConfirmedByUserDomainEvent(this));

        }
        public Guid UserId { get; private set; }
        public Guid WalletId { get; private set; }
        public Currency.ValueObjects.Currency Currency { get; private set; }
        public WithdrawDestination Address { get; private set; }
        public MonetaryValue Amount { get; private set; }
        public WithdrawTransactionId TXID { get; private set; }
        public WithdrawStatus Status { get; private set; }
        public CurrencyType CurrencyType { get; private set; }
        public NetworkName NetworkName { get; private set; }
        public string RetryReason { get; private set; }
        public static Withdraw CreateIRTWithdraw(Guid userId, Guid walletId, Currency.ValueObjects.Currency currency, string address, string networkname, decimal amount)
        {

            return new Withdraw(userId, walletId, currency, address, networkname, amount);
        }
        public void SendWithdraw()
        {
            CheckRule(new WithdrawStatusShouldBeConfirmedByUseronSendingWithdraw(Status));
            Status = WithdrawStatus.Sent;
        }
        public void CompleteWithdraw()
        {

            CheckRule(new WithdrawStatusShouldBeSentOnComplitingWithdraw(Status));
            Status = WithdrawStatus.Completed;
        }
        public void SetRetryByApplication(string reason)
        {
            RetryReason = reason;
            //Status = WithdrawStatus.RetryByApplication;
        }
        public void SetRetryByAdministration(string reason)
        {
            RetryReason = reason;
            // Status = WithdrawStatus.RetryByAdmin;
        }
    }
}
