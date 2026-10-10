using CXChange.Modules.Payment.Application.Commands.TomanDepositVerify;
using static CXChange.API.Modules.Payment.Requests;

namespace CXChange.API.Modules.Payment.Maps
{
    public static class MapZibalCallBackToConfirmDeposit
    {
        public static VerifyIRTDeposit MapToConfirmDeposit(this ZibalGateWayCallbackInfo source, object extraData = null)
        {
            return new VerifyIRTDeposit
            {
                DepositId = Guid.Parse(source.orderId),
                Success = true,
                TrackId = source.trackId
            };
        }
    }
}

