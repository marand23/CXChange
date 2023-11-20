namespace CXChange.Modules.Payment.PaymentApiAdapters.GateWay.Jibit.Verify
{
    internal record JibitVerifyRequest
    {
        public string purchaseId { get; init; }
    }
}
