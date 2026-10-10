namespace CXChange.Modules.Payment.PaymentApiAdapters.GateWay.Zibal.WithdrawInquiry
{
    internal record ZibalWithdrawInquiryRequest
    {
        public string chckoutRequestId { get; init; }
        public string walletId { get; init; }

    }
}
