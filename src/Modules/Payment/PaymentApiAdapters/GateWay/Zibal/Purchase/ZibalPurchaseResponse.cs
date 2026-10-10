namespace CXChange.Modules.Payment.PaymentApiAdapters.GateWay.Zibal.Purchase
{
    internal class ZibalPurchaseResponse
    {
        public long trackId { get; init; }
        public int result { get; init; }
        public string payLink { get; init; }
        public string message { get; init; }
        public override string ToString()
        {
            return $"trackId:{trackId} message: {message}, result:{result}";
        }

    }
}
