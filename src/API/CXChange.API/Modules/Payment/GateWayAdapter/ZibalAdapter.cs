using CXChange.Modules.Payment.Application.Commands.TomanDepositVerify;
using static CXChange.API.Modules.Payment.Requests;

namespace CXChange.API.Modules.Payment.GateWayAdapter
{
    public class ZibalAdapter : IGateWayAdapter
    {
        public VerifyTomanDeposit Adapt(IAdaptee adaptee)
        {
            ZibalGateWayRequest request = (ZibalGateWayRequest)(adaptee);
            return new VerifyTomanDeposit
            {
                DepositId = Guid.Parse(request.orderId),
                Success = request.success,
                TrackId = request.trackId,
            };
        }
    }
}
