namespace CXChange.Modules.Payment.PaymentApiAdapters.GateWay.Jibit.Withdraw
{
    internal class JibitSendWithdrawRequest
    {
        public string batchId { get; init; }
        public string submissionMode { get; init; }
        public Transfer[] transfers { get; init; }

    }
    internal record Transfer
    {
        public string transferId { get; init; }
        public string transferMode { get; init; }
        public string destination { get; init; }
        public long amount { get; init; }
        public string currency { get; init; }
        public string description { get; init; }
        public string notifyUrl { get; init; }
        public bool cancelabe { get; init; }

    }
}
