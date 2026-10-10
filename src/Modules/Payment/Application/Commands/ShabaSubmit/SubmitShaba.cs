namespace CXChange.Modules.Payment.Application.Commands.ShabaAdd
{
    public record SubmitShaba
    {
        public Guid UserId { get; set; }
        public string ShabaNumber { get; init; }
        public string BankName { get; init; }
    }
}
