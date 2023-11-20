using CXChange.BuildingBlocks.Domain;

namespace CXChange.Modules.Payment.Domain.Currency.Exceptions
{
    internal class BalanceNegativeException : BusinessRuleException
    {
        public BalanceNegativeException(Enum code, string message = null) : base(code, message)
        {
        }



    }
}
