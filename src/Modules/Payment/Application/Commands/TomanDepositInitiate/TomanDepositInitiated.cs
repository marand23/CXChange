namespace CXChange.Modules.Payment.Application.Commands.TomanDepositInitiate
{
    public record TomanDepositInitiated
    {
        public string PayLink { get; init; }
    }
}
