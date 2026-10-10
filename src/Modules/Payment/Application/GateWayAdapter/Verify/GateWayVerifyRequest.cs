namespace CXChange.Modules.Payment.Application.GateWayAdapter.Verify
{
    public record GateWayVerifyRequest
    {
        public string TrackId { get; init; }
        public string CardHash { get; init; }
        public string CardNumber { get; init; }
    }
}
