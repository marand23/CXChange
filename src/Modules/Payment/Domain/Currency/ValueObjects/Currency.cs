using CXChange.Modules.Payment.Domain.Currency.Enums;

namespace CXChange.Modules.Payment.Domain.Currency.ValueObjects
{
    public record Currency
    {
        private Currency() { }
        public Currency(string name, decimal precision)
        {
            Name = name;
            Precision = precision;
        }
        public string Name { get; init; }
        public string Symbol { get; init; }
        public decimal Precision { get; init; }
        public CurrencyType Type { get; init; }

    }

}
