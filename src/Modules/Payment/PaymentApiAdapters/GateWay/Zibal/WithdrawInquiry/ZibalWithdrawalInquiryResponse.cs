namespace CXChange.Modules.Payment.PaymentApiAdapters.GateWay.Zibal.WithdrawInquiry
{
    internal record ZibalWithdrawalInquiryResponse
    {
        public string message { get; init; }
        public int result { get; init; }
        public ZibalWithdrawalInquiryData data { get; init; }
    }
    internal record ZibalWithdrawalInquiryData
    {
        public string status { get; init; }
        public string type { get; init; }
        public string checkout { get; init; }
        public long amount { get; init; }
        public string bankAccount { get; init; }
        public string subMerchantName { get; init; }
        public string refCode { get; init; }
        public string trackId { get; init; }
        public string settlementDate { get; init; }

    }
}
