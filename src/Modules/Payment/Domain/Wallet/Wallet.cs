using CXChange.BuildingBlocks.Domain;
using CXChange.Modules.Payment.Domain.Currency.ValueObjects;
using CXChange.Modules.Payment.Domain.Wallet.Specifications;

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
            CheckRule(new AmountToDeductShouldBeLessOrEqualThanReservedBalance(new MonetaryValue(amount, Currency), ReservedBalance));
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
            return Deposit.Deposit.Create(depositId, amount, currency, Id, userId, new ValueObjects.DepositAddress(cardNumber));
        }
        public void ConfirmDeposit(Deposit.Deposit deposit)
        {
            deposit.ConfirmTomanDeposit("TrackId");
            AddBalance(deposit.Amount.Value);
        }
        public void AddDepositAccount()
        {



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
