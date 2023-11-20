namespace CXChange.Modules.Payment.Application.Commands.TomanDepositVerify
{
    public record VerifyTomanDeposit
    {
        public Guid DepositId { get; init; }
        public bool Success { get; init; }
        public string TrackId { get; init; }
    }
    public enum Status
    {
        success,
        unknown,
        failure
    }
}
