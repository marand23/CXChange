namespace CXChange.Modules.Payment.Domain.ValueObjects
{
    public record WithdrawAddress
    {
        public WithdrawAddress(string address)
        {
            Address = address;
        }
        public string Address { get; init; }
    }
}
