using CXChange.Modules.Payment.Application.Commands.TomanDepositVerify;
using static CXChange.API.Modules.Payment.Requests;

namespace CXChange.API.Modules.Payment.GateWayAdapter
{
    public class NextAdapter : IGateWayAdapter
    {
        public VerifyTomanDeposit Adapt(IAdaptee adaptee)
        {
            var nextRequst = (NextGateWayRequest)adaptee;
            return new VerifyTomanDeposit
            {
                DepositId = Guid.Parse(nextRequst.order_id),
                TrackId = nextRequst.trans_id,
                Success = true,

            };
        }
    }
}
