namespace CXChange.Modules.Payment.Application.GateWayAdapter.Withdraw
{
    public record SendWithdrawRequest
    {
        public decimal Amount { get; init; }
        public string Shaba { get; init; }
        public Guid WithdrawId { get; init; }

    }
}
