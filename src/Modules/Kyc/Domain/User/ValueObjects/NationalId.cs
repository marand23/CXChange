namespace CXChange.Modules.Kyc.Domain.User.ValueObjects
{
    public record NationalId
    {
        public NationalId(string nationalNumber)
        {
            NationalNumber = nationalNumber;
        }
        public string NationalNumber { get; init; }
    }
}
