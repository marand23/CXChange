using CXChange.Modules.Payment.Application.Commands.TomanDepositVerify;
using static CXChange.API.Modules.Payment.Requests;

namespace CXChange.API.Modules.Payment.Maps
{
    public static class MapJibitCallbackInfoToVerifyIRTDeposit
    {
        public static VerifyIRTDeposit MapToVerifyIRTDeposit(this JibitGateWayCallbackInfo source, object extraData = null)
        {
            return new VerifyIRTDeposit
            {
                DepositId = Guid.Parse(source.clientReferenceNumber),
                Success = true,
                TrackId = source.purchaseId
            };
        }
    }
}
