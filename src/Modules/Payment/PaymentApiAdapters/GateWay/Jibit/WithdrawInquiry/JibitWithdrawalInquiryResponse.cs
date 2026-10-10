namespace CXChange.Modules.Payment.PaymentApiAdapters.GateWay.Jibit.WithdrawInquiry
{
    internal record JibitWithdrawalInquiryResponse
    {
        public string state { get; init; }
        public string bankTransferId { get; init; }
        public string failReason { get; init; }

    }
}
