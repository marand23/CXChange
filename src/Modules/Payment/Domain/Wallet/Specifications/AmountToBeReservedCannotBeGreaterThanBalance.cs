using CXChange.BuildingBlocks.Domain.Specifications;
using CXChange.Modules.Payment.Domain.Currency.ValueObjects;

namespace CXChange.Modules.Payment.Domain.Wallet.Specifications
{
    internal class AmountToBeReservedCannotBeGreaterThanBalance : IBussinessRuleSpecification
    {
        public string Message => "cannot reserve balance , the reserved balance is more that balance";

        public Enum Code => throw new NotImplementedException();
        MonetaryValue _balanceToReserve;
        MonetaryValue _balance;
        public AmountToBeReservedCannotBeGreaterThanBalance(MonetaryValue balanceToReserve, MonetaryValue balance)
        {
            _balanceToReserve = balanceToReserve;
            _balance = balance;
        }
        public bool isSatisfiedBy()
        {
            return _balance >= _balanceToReserve;
        }
    }
}
