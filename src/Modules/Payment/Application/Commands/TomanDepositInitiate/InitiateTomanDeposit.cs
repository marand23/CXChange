namespace CXChange.Modules.Payment.Application.Commands.TomanDepositInitiate
{
    public record InitiateTomanDeposit
    {
        public Guid UserId { get; init; }
        public decimal Amount { get; init; }
        public string CardNumber { get; init; }

    }
}
