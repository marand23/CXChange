using CXChange.Modules.Payment.Application.GateWayAdapter.Purchase;
using CXChange.Modules.Payment.Application.GateWayAdapter.Verify;

namespace CXChange.Modules.Payment.Application.GateWayAdapter
{
    public interface IGatewayAdapter
    {
        Task<GateWayPurchaseResponse> CreatePurchase(GateWayPurchaseRequest request);
        Task<GateWayVerifyResponse> Verify(GateWayVerifyRequest request);
    }
}
