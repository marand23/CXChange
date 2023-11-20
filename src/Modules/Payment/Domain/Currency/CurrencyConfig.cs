using CXChange.BuildingBlocks.Domain;
using CXChange.Modules.Payment.Domain.Currency.ValueObjects;

namespace CXChange.Modules.Payment.Domain.Currency
{
    public class CurrencyConfig : Entity<int>, IAggregateRoot
    {
        private CurrencyConfig() { }
        public Currency.ValueObjects.Currency Currency { get; private set; }
        public DepositWithdrawalStatus DepositWithdrawStatus { get; private set; }
    }
}
