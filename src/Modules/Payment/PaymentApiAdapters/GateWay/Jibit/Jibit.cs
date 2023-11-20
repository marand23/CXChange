using CXChange.Modules.Payment.Application.GateWayAdapter;
using CXChange.Modules.Payment.Application.GateWayAdapter.Purchase;
using CXChange.Modules.Payment.Application.GateWayAdapter.Verify;
using CXChange.Modules.Payment.PaymentApiAdapters.GateWay.Jibit.Purchase;
using CXChange.Modules.Payment.PaymentApiAdapters.GateWay.Jibit.Verify;
using Microsoft.Extensions.Options;

namespace CXChange.Modules.Payment.PaymentApiAdapters.GateWay.Jibit
{
    internal class Jibit : IGatewayAdapter
    {
        JibitClient _jibitClient;
        JibitGatewayURLs _gatewayURLs;
        public Jibit(IOptions<JibitGatewayURLs> gatewayURLs, JibitClient jibitClient)
        {
            _gatewayURLs = gatewayURLs.Value;
            _jibitClient = jibitClient;
        }
        public async Task<GateWayPurchaseResponse> CreatePurchase(GateWayPurchaseRequest request)
        {
            var requestData = new JibitPurchaseRequest
            {

            };
            var response = await _jibitClient.Purchase(requestData);
            return new GateWayPurchaseResponse
            {

            };
        }

        public async Task<GateWayVerifyResponse> Verify(GateWayVerifyRequest request)
        {
            var requestData = new JibitVerifyRequest
            {
                purchaseId = request.TrackId
            };
            var response = await _jibitClient.Verify(requestData);
            return new GateWayVerifyResponse
            {

            };
        }


    }

}
