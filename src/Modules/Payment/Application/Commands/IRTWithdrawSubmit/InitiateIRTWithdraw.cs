namespace CXChange.Modules.Payment.Application.Commands.IRTWithdrawSubmit
{
    public record SubmitIRTWithdraw
    {
        public Guid UserId { get; init; }
        public string Shaba { get; init; }
        public decimal Amount { get; init; }

    }
}
