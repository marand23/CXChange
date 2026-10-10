namespace CXChange.Modules.Payment.PaymentApiAdapters.GateWay.Jibit.Inquiry
{
    internal record JibitInquiryResponse
    {
        public int state { get; init; }
        public string pspFailReason { get; init; }

    }
}
