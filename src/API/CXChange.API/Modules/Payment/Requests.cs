namespace CXChange.API.Modules.Payment
{
    public class Requests
    {
        public record SubmitShabaRequest(string shabaNumber, string bankName);
        public record SubmitBankCardRequest(string cardNumber, string bankName);
        public record ZibalGateWayCallbackInfo(int success, string trackId, string orderId, int status);
        public record JibitGateWayCallbackInfo(string purchaseId, string clientReferenceNumber, string status, string pspReferenceNumber, string pspHashedCardNumber, string failReason);


    }
}
