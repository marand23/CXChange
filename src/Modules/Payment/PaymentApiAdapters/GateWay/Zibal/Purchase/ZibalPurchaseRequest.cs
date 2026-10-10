namespace CXChange.Modules.Payment.PaymentApiAdapters.GateWay.Zibal.Purchase
{
    internal record ZibalPurchaseRequest
    {
        public string merchant { get; init; }
        public long amount { get; init; }
        public string callbackUrl { get; init; }
        public string orderId { get; init; }
        public string mobile { get; init; }
        public string[] allowedCards { get; init; }


    }
}
