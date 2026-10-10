namespace CXChange.Modules.Payment.Application.Commands.BankCardAdd
{
    public record SubmitBankCard
    {
        public string CardNumber { get; init; }
        public string BankName { get; init; }
        public Guid UserId { get; init; }
    }
}
