namespace CXChange.Modules.Payment.Application.Commands.TomanDepositVerify
{
    public record VerifyIRTDeposit
    {
        public Guid DepositId { get; init; }
        public bool Success { get; init; }
        public string TrackId { get; init; }
        public string CardHash { get; init; }
    }
}
