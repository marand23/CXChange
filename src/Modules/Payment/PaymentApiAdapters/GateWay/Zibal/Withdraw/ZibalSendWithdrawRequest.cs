namespace CXChange.Modules.Payment.PaymentApiAdapters.GateWay.Zibal.Withdraw
{
    internal record ZibalSendWithdrawRequest
    {
        public long amount { get; init; }
        public int id { get; init; }
        public string bankAccount { get; init; }
        public int checkoutDelay { get; init; }
        public string uniqueCode { get; init; }
    }
}
