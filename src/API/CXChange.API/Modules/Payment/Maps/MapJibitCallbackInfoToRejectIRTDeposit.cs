using CXChange.Modules.Payment.Application.Commands.IRTDepositReject;
using CXChange.Modules.Payment.Application.GateWayAdapter.Purchase;
using static CXChange.API.Modules.Payment.Requests;

namespace CXChange.API.Modules.Payment.Maps
{
    public static class MapJibitCallbackInfoToRejectIRTDeposit
    {
        public static RejectIRTDeposit MapToRejectIRTDeposit(this JibitGateWayCallbackInfo source, object extraData = null)
        {
            return new RejectIRTDeposit
            {
                DepositId = Guid.Parse(source.clientReferenceNumber),
                Error = GetError(source.failReason),
                TrackId = source.purchaseId

            };
        }
        public static GatewayPurchaseError GetError(string failReason)
        {
            GatewayPurchaseError error = failReason switch
            {
                "CANCELLED_BY_USER" => GatewayPurchaseError.CanceledByUser,
                "TRANSACTION_TIMED_OUT" => GatewayPurchaseError.TransactionTimedOut,
                "PSP_PAYER_CARD_NOT_MATCHED" => GatewayPurchaseError.CardDoesNotMatch,
                "UNKNOWN" => GatewayPurchaseError.Unkown

            };
            return error;

        }
    }
}
