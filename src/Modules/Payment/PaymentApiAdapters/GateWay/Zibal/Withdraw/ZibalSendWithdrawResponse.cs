namespace CXChange.Modules.Payment.PaymentApiAdapters.GateWay.Zibal.Withdraw
{
    internal record ZibalSendWithdrawResponse
    {
        public int result { get; init; }
        public string message { get; init; }
        public ZibalSendWithdrawResponseData data { get; init; }

    }
    internal record ZibalSendWithdrawResponseData
    {
        public string id { get; init; }
        public string createdAt { get; init; }
        public long amount { get; init; }
        public string description { get; init; }
        public int type { get; init; }
        public string transasctionId { get; init; }
        public string transactionOrderId { get; init; }
        public string ledgerId { get; init; }

    }
}
