using CXChange.BuildingBlocks.Domain.Specifications;

namespace CXChange.Modules.Payment.Domain.Deposit.Specifications
{
    internal class DepositCurrencyShouldBeTomanOnTomanDepositConfirm : IBussinessRuleSpecification
    {
        public string Message => "this method can be used just for toman deposit confirmation";

        public Enum Code => BusinessRuleErrorEnum.IRTConfirmationUsedForCrypto;
        Currency.ValueObjects.Currency _currency;
        public DepositCurrencyShouldBeTomanOnTomanDepositConfirm(Currency.ValueObjects.Currency currency)
        {
            _currency = currency;
        }
        public bool isSatisfiedBy()
        {
            return _currency.Name == "IRT";
        }
    }
}
