namespace CXChange.Modules.Payment.Domain.BankCard.ValueObjects
{
    public record Bank
    {
        private Bank() { }
        public Bank(string name)
        {
            Name = name;
        }
        public string Name { get; init; }
    }
}
