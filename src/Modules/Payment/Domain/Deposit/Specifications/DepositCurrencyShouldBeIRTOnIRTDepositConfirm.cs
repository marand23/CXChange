using CXChange.BuildingBlocks.Domain.Specifications;

namespace CXChange.Modules.Payment.Domain.Deposit.Specifications
{
    internal class DepositCurrencyShouldBeIRTOnIRTDepositConfirm : IBussinessRuleSpecification
    {
        public string Message => "this method can be used just for toman deposit confirmation";

        public Enum Code => BusinessRuleErrorEnum.IRTConfirmationUsedForCrypto;
        Currency.ValueObjects.Currency _currency;
        public DepositCurrencyShouldBeIRTOnIRTDepositConfirm(Currency.ValueObjects.Currency currency)
        {
            _currency = currency;
        }
        public bool isSatisfiedBy()
        {
            return _currency.Name == "IRT";
        }
    }
}
