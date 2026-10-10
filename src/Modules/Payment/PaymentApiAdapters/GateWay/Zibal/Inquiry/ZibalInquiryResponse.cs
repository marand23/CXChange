namespace CXChange.Modules.Payment.PaymentApiAdapters.GateWay.Zibal.Inquiry
{
    internal class ZibalInquiryResponse
    {
        public string cardNumber { get; init; }
        public int status { get; init; }
        public string refNumber { get; init; }
        public int result { get; init; }
        public string message { get; init; }
    }
}
