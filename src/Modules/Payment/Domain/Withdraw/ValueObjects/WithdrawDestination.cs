using System.Text.RegularExpressions;

namespace CXChange.Modules.Payment.Domain.Withdraw.ValueObjects
{
    public record WithdrawDestination
    {
        public WithdrawDestination(string address)
        {
            Address = address;
        }
        private void Validate()
        {
            var result = Regex.IsMatch(Address, AddressRegex);
        }
        public string Address { get; init; }

        public string AddressRegex { get; init; }
    }
}
