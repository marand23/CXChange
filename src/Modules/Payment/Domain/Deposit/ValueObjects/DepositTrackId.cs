namespace CXChange.Modules.Payment.Domain.Deposit.ValueObjects
{
    public record DepositTrackId
    {
        public DepositTrackId(string trackId)
        {
            TrackId = trackId;
        }
        public string TrackId { get; init; }
    }
}
