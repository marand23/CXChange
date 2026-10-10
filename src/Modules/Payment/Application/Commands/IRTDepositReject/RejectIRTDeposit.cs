using CXChange.Modules.Payment.Application.GateWayAdapter.Purchase;

namespace CXChange.Modules.Payment.Application.Commands.IRTDepositReject
{
    public record RejectIRTDeposit
    {
        public Guid DepositId { get; init; }
        public string TrackId { get; init; }
        public GatewayPurchaseError Error { get; init; }
    }

}
