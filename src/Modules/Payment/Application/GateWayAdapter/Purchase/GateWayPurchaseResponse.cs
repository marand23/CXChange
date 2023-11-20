namespace CXChange.Modules.Payment.Application.GateWayAdapter.Purchase
{
    public record GateWayPurchaseResponse
    {
        public string TrackId { get; init; }
        public string PayLink { get; init; }
    }
}
