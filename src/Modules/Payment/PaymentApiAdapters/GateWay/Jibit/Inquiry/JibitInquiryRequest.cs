namespace CXChange.Modules.Payment.PaymentApiAdapters.GateWay.Jibit.Inquiry
{
    internal record JibitInquiryRequest
    {
        public string PurchaseId { get; init; }
        public string GetQueryString()
        {
            return $"?purchaseId={PurchaseId}";
        }
    }
}
