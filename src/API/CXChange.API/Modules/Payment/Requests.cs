using CXChange.API.Modules.Payment.GateWayAdapter;

namespace CXChange.API.Modules.Payment
{
    public class Requests
    {
        public record SubmitShabaRequest(string shabaNumber, string bankName);
        public record SubmitBankCardRequest(string cardNumber, string bankName);
        public record ZibalGateWayRequest(bool success, string trackId, string orderId, string status) : IAdaptee;
        public record NextGateWayRequest(string trans_id, string order_id, int amount) : IAdaptee;
        public record JibitGateWayRequest(int amount, int wage, string currency, string purchaseId, string clientReferenceNumber, string success, string payerIp, string pspReferenceNumber, string payerMaskedCardNumber, string pspRRN, string pspNmae, string pspHashedCardNumber) : IAdaptee;

        //amount=500000&wage=4000&currency=IRR&purchaseId=3476532108364833&clientReferenceNumber=client-ref-num&status=SUCCESSFUL&payerIp=23.155.43.100&pspReferenceNumber=GmshtyjwKSsd%2Fd54Idy8COJK78gDjse2BjPw%2B3dlFj&payerMaskedCardNumber=504172******7412&pspRRN=17356154785&pspName=saman-ipg&pspHashedCardNumber=F62A0955E51BC46D71B3647594913594
    }
}
