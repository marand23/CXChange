using CXChange.BuildingBlocks.Domain;
using CXChange.Modules.Payment.Domain.Currency.ValueObjects;
using CXChange.Modules.Payment.Domain.Wallet.Specifications;
using CXChange.Modules.Payment.Domain.Withdraw.DomainEvents;

namespace CXChange.Modules.Payment.Domain.Wallet
{
    public class Wallet : Entity<Guid>, IAggregateRoot
    {

        private Wallet() { }
        private Wallet(Guid id, Guid userId, int currencyId, decimal balance, decimal reservedBalance, string currency, decimal precision)
        {

            Balance = new MonetaryValue(balance, new Domain.Currency.ValueObjects.Currency(currency, precision));
            ReservedBalance = new MonetaryValue(reservedBalance, new Domain.Currency.ValueObjects.Currency(currency, precision));
            Currency = new Domain.Currency.ValueObjects.Currency(currency, precision);
            UserId = userId;
            Id = id;
            CurrencyId = currencyId;

        }


        public int Version { get; private set; }
        public DepositWithdrawalStatus DepositWithdrawalStatus;
        public Guid UserId { get; private set; }
        public int CurrencyId { get; private set; }
        public MonetaryValue Balance { get; private set; }
        public MonetaryValue ReservedBalance { get; private set; }
        public Currency.ValueObjects.Currency Currency { get; private set; }
        public DateTime CreatedAt { get; private set; }

        private void ReserveBalance(decimal amount)
        {
            CheckRule(new AmountToBeReservedCannotBeGreaterThanBalance(new MonetaryValue(amount, Currency), Balance));
            ReservedBalance = new MonetaryValue(amount, Currency) + ReservedBalance;
        }
        public void AddBalance(decimal amount)
        {
            Balance = new MonetaryValue(amount, Currency) + Balance;
        }
        private void DeductBalance(decimal amount)
        {
            CheckRule(new AmountToBeDeductedShouldBeLessOrEqualThanBalance(new MonetaryValue(amount, Currency), Balance));
            // CheckRule(new AmountToDeductShouldBeLessOrEqualThanReservedBalance(new MonetaryValue(amount, Currency), ReservedBalance));
            Balance = Balance - new MonetaryValue(amount, Currency);
            ReservedBalance = ReservedBalance - new MonetaryValue(amount, Currency);
        }
        public MonetaryValue GetAvailableBalance()
        {
            return Balance - ReservedBalance;
        }
        public static Wallet Create(Guid id, Guid userId, int currencyId, decimal balance, decimal reservedBalance, string currency, decimal precision)
        {
            return new Wallet(id, userId, currencyId, balance, reservedBalance, currency, precision);
        }

        public Deposit.Deposit CreateDeposit(Guid depositId, Guid userId, decimal amount, Currency.ValueObjects.Currency currency, string cardNumber)
        {

            Version += 1;
            return Deposit.Deposit.Create(depositId, amount, currency, Id, userId, new ValueObjects.DepositAddress(cardNumber));
        }
        public void ConfirmDeposit(Deposit.Deposit deposit)
        {
            deposit.ConfirmIRTDeposit();
            AddBalance(deposit.Amount.Value);
        }
        public Withdraw.Withdraw CreateIRTWithdraw(Guid withdrawId, Guid userId, decimal amount, Currency.ValueObjects.Currency currency, string shaba)
        {
            ReserveBalance(amount);
            return Withdraw.Withdraw.CreateIRTWithdraw(userId, Id, currency, "", shaba, amount);

        }
        public void SendWithdraw(Withdraw.Withdraw withdraw)
        {

            withdraw.SendWithdraw();
        }
        public void SetWithdawAsRetryable(string retryReason, Withdraw.Withdraw withdraw)
        {
            withdraw.SetRetryByAdministration(retryReason);
        }
        public void CompleteWithdraw(Withdraw.Withdraw withdraw)
        {
            DeductBalance(withdraw.Amount.Value);
            AddDomainEvent(new WithdrawCompletedDomainEvent());
        }

        public Wallet MapCurrency(string currency, decimal precision)
        {
            this.Currency = new Currency.ValueObjects.Currency(currency, precision);
            this.Balance = Balance with { Currency = this.Currency };
            this.ReservedBalance = ReservedBalance with { Currency = this.Currency };
            return this;
        }
    }

}
