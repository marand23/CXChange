namespace CXChange.Modules.Payment.PaymentApiAdapters.GateWay.Jibit.Purchase
{
    internal record JibitPurchaseRequest
    {
        public int amount { get; init; }
        public string callBackUrl { get; init; }
        public string clientReferenceNumber { get; init; }
        int currency { get; init; }
        public string describtion { get; init; }
        public string payerCardNumber { get; init; }
        public string payerMobileNumber { get; init; }
    }
}
