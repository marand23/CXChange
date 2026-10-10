namespace CXChange.Modules.Payment.Application.GateWayAdapter.InquiryWithdraw
{
    public record InquiryWithdrawRequest
    {
        public Guid WithdrawId { get; init; }
    }
}
