namespace CXChange.Modules.Payment.Application.Commands.IRTDepositSubmit
{
    public record IRTDepositSubmitted
    {
        public string PayLink { get; init; }
    }
}
