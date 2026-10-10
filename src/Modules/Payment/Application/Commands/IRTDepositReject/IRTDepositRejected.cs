namespace CXChange.Modules.Payment.Application.Commands.IRTDepositReject
{
    public record IRTDepositRejected
    {
        public Enum Error { get; init; }

    }
}
