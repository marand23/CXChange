namespace CXChange.Modules.Payment.PaymentApiAdapters.GateWay.Jibit.RetryWithdraw
{
    internal record JibitRetryWithdrawRequest
    {
        public string transferId { get; init; }
    }
}
