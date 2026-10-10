namespace CXChange.Modules.Payment.Application.Commands.IRTDepositSubmit
{
    public record SubmitIRTDeposit
    {
        public Guid UserId { get; init; }
        public decimal Amount { get; init; }
        public string CardNumber { get; init; }

    }
}
