namespace CXChange.Modules.Payment.PaymentApiAdapters.GateWay.Jibit.Purchase
{
    internal record JibitPurchaseResponse
    {
        public string purchaseIdStr { get; init; }
        public string clientReferenceNumber { get; init; }
        public string pspSwitchingUrl { get; init; }

    }
}
