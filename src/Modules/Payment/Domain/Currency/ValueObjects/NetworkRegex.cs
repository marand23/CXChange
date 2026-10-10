namespace CXChange.Modules.Payment.Domain.Currency.ValueObjects
{
    public record NetworkRegex
    {
        public NetworkRegex(string addressRegex, string memoRegex)
        {
            AddressRegex = addressRegex;
            MemoRegex = memoRegex;
        }
        public string AddressRegex { get; init; }
        public string MemoRegex { get; init; }

    }
}
