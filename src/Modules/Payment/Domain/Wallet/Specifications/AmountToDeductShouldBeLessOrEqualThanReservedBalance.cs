using CXChange.BuildingBlocks.Domain.Specifications;
using CXChange.Modules.Payment.Domain.Currency.ValueObjects;

namespace CXChange.Modules.Payment.Domain.Wallet.Specifications
{
    internal class AmountToDeductShouldBeLessOrEqualThanReservedBalance : IBussinessRuleSpecification
    {
        public string Message => "reserved balance is less than amount to deduct";

        public Enum Code => throw new NotImplementedException();
        MonetaryValue _balanceToDeduct;
        MonetaryValue _reservedBalance;
        public AmountToDeductShouldBeLessOrEqualThanReservedBalance(MonetaryValue balanceToDeduct, MonetaryValue reservedBalance)
        {
            _balanceToDeduct = balanceToDeduct;
            _reservedBalance = reservedBalance;
        }
        public bool isSatisfiedBy()
        {
            return _reservedBalance >= _balanceToDeduct;
        }
    }
}
