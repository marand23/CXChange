namespace CXChange.Modules.Payment.Application.GateWayAdapter.Verify
{
    public record GateWayVerifyRequest
    {
        public string TrackId { get; init; }
    }
}
