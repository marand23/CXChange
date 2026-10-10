using CXChange.BuildingBlocks.Domain.Specifications;
using CXChange.Modules.Payment.Domain.Currency.ValueObjects;

namespace CXChange.Modules.Payment.Domain.Wallet.Specifications
{
    internal class AmountToBeDeductedShouldBeLessOrEqualThanBalance : IBussinessRuleSpecification
    {
        public string Message => "amount to deduct is less";

        public Enum Code => throw new NotImplementedException();
        MonetaryValue _amountToDeduct;
        MonetaryValue _balance;
        public AmountToBeDeductedShouldBeLessOrEqualThanBalance(MonetaryValue amountToDeduct, MonetaryValue balance)
        {
            _amountToDeduct = amountToDeduct;
            _balance = balance;
        }
        public bool isSatisfiedBy()
        {
            return _balance >= _amountToDeduct;
        }
    }
}
