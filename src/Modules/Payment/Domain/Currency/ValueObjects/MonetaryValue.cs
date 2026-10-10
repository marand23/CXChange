using CXChange.Modules.Payment.Domain.Currency.Exceptions;

namespace CXChange.Modules.Payment.Domain.Currency.ValueObjects
{
    public record MonetaryValue
    {
        private MonetaryValue() { }//efcore
        public MonetaryValue(decimal value, Currency currency)
        {
            CheckValue(value, currency);
            Value = value;
            Currency = currency;
        }
        public decimal Value { get; init; }
        public Currency Currency { get; init; }
        private void CheckValue(decimal value, Currency currency)
        {
            if (value < 0)
            {
                throw new BalanceNegativeException(BusinessRuleErrorEnum.UserCantHaveMoreThanFourCards, "");
            }
            if (value % currency.Precision != 0)
            {
                throw new BalancePrecisionException(BusinessRuleErrorEnum.UserCantHaveMoreThanFourCards, "");
            }
        }


        public static MonetaryValue operator +(MonetaryValue left, MonetaryValue right)
        {
            if (left.Currency != right.Currency)
            {
                throw new Exception();
            }
            return new MonetaryValue(left.Value + right.Value, left.Currency);
        }
        public static MonetaryValue operator -(MonetaryValue left, MonetaryValue right)
        {
            if (left.Value < right.Value)
            {
                throw new Exception();
            }
            return new MonetaryValue(left.Value - right.Value, left.Currency);
        }
        public static bool operator >=(MonetaryValue left, MonetaryValue right)
        {
            return left.Value > right.Value || left.Equals(right);
        }
        public static bool operator <=(MonetaryValue left, MonetaryValue right)
        {
            return left.Value < right.Value || left.Equals(right);
        }
        public override string ToString()
        {
            return string.Format("{0} {1}", Value, Currency.Name);
        }

    }
}






