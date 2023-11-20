using CXChange.Modules.Payment.Application.GateWayAdapter;
using CXChange.Modules.Payment.Application.GateWayAdapter.Purchase;
using CXChange.Modules.Payment.Application.GateWayAdapter.Verify;
using CXChange.Modules.Payment.PaymentApiAdapters.GateWay.Zibal.Purchase;
using CXChange.Modules.Payment.PaymentApiAdapters.GateWay.Zibal.Verify;
namespace CXChange.Modules.Payment.PaymentApiAdapters.GateWay.Zibal
{
    internal class Zibal : IGatewayAdapter
    {
        ZibalClient _client;
        public Zibal(ZibalClient client)
        {
            _client = client;
        }
        public async Task<GateWayPurchaseResponse> CreatePurchase(GateWayPurchaseRequest request)
        {

            var requestData = new ZibalPurchaseRequest
            {
                amount = (int)request.Amount,
                allowedCards = new[] { request.CardNumber },
                mobile = request.PhoneNumber,
                orderId = request.DepositId
            };
            var result = await _client.Purchase(requestData);
            return new GateWayPurchaseResponse
            {
                PayLink = $"https://gateway.zibal.ir/start/{result.trackId}",
                TrackId = result.trackId.ToString(),
            };
        }

        public async Task<GateWayVerifyResponse> Verify(GateWayVerifyRequest request)
        {


            var requestData = new ZibalVerifyRequest
            {
                trackId = long.Parse(request.TrackId)
            };

            var result = await _client.Verify(requestData);
            return new GateWayVerifyResponse
            {

            };
        }
    }
}
