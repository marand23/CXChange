namespace CXChange.Modules.Payment.PaymentApiAdapters.GateWay.Zibal.Verify
{
    internal record ZibalVerifyRequest
    {

        public long trackId { get; init; }
    }
}
