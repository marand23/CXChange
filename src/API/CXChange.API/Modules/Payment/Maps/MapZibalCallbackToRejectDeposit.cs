using CXChange.Modules.Payment.Application.Commands.IRTDepositReject;
using CXChange.Modules.Payment.Application.GateWayAdapter.Purchase;
using static CXChange.API.Modules.Payment.Requests;

namespace CXChange.API.Modules.Payment.Maps
{
    public static class MapZibalCallbackToRejectDeposit
    {
        public static RejectIRTDeposit MapToRejectDeposit(this ZibalGateWayCallbackInfo source, object extraData = null)
        {
            return new RejectIRTDeposit
            {
                DepositId = Guid.Parse(source.orderId),
                Error = GetFailReason(source.status),
                TrackId = source.trackId
            };
        }
        private static GatewayPurchaseError GetFailReason(int status)
        {

            GatewayPurchaseError error = status switch
            {
                3 => GatewayPurchaseError.CanceledByUser,
                4 => GatewayPurchaseError.CardDoesNotMatch,
                5 => GatewayPurchaseError.NotEnoughBalance,
                6 => GatewayPurchaseError.PinNotValid,
                7 => GatewayPurchaseError.RequestLimit,
                8 => GatewayPurchaseError.Other,
                9 => GatewayPurchaseError.Other,
                10 => GatewayPurchaseError.InvalidCardIssuer,
                11 => GatewayPurchaseError.SwitchIssue,
                12 => GatewayPurchaseError.CardNotAccessable,
                -2 => GatewayPurchaseError.InternalError,
                _ => GatewayPurchaseError.Other
            };
            return error;
        }
    }
}