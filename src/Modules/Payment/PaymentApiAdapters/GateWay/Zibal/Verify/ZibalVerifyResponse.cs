namespace CXChange.Modules.Payment.PaymentApiAdapters.GateWay.Zibal.Verify
{
    internal record ZibalVerifyResponse
    {

        public int amount { get; init; }
        public int result { get; init; }
        public string cardNumber { get; init; }
        public string orderId { get; init; }
        public string description { get; init; }
        public string refNumber { get; init; }
        public string message { get; init; }
        public override string ToString()
        {
            return $"zibal verification failed info:{message}   {result}";
        }
    }
}
