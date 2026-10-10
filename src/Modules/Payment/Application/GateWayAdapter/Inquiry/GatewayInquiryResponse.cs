namespace CXChange.Modules.Payment.Application.GateWayAdapter.Inquiry
{
    public class GatewayInquiryResponse
    {
        public PurchaseStatus Status { get; init; }
        public string FailReason { get; init; }
    }
    public enum PurchaseStatus
    {
        WaitingForPayment = -80,
        PaidUnverified,
        PaidVerified,
        Failed,
        Unkkown
    }
}
