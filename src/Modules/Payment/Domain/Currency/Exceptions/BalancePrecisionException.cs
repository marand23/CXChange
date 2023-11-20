using CXChange.BuildingBlocks.Domain;

namespace CXChange.Modules.Payment.Domain.Currency.Exceptions
{
    internal class BalancePrecisionException : BusinessRuleException
    {
        public BalancePrecisionException(Enum code, string message) : base(code, message)
        {
        }
    }
}
