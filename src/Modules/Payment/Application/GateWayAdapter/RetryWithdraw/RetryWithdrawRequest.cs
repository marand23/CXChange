namespace CXChange.Modules.Payment.Application.GateWayAdapter.RetryWithdraw
{
    public record RetryWithdrawRequest
    {
        public Guid WithdrawId { get; init; }
    }
}
