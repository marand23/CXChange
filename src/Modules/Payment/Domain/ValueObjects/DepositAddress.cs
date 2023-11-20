namespace CXChange.Modules.Payment.Domain.ValueObjects
{
    public record DepositAddress
    {

        public DepositAddress(string address)
        {
            Address = address;
        }
        public string Address { get; init; }



    }
}
