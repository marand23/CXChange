namespace CXChange.Modules.Payment.Application.GateWayAdapter.InquiryWithdraw
{
    public record InquiryWithdrawResponse
    {
        public InquiryWithdrawStatus Status { get; init; }
        public Guid WithdrawId { get; init; }
        public string BankTransferId { get; init; }
        public string FailureReason { get; init; }

    }
}
