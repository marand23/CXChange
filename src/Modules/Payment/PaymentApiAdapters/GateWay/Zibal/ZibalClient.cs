using CXChange.BuildingBlocks.Infrastructure;
using CXChange.BuildingBlocks.Infrastructure.HttpClient;
using CXChange.Modules.Payment.PaymentApiAdapters.GateWay.Zibal.Purchase;
using CXChange.Modules.Payment.PaymentApiAdapters.GateWay.Zibal.Verify;
using Microsoft.Extensions.Options;

namespace CXChange.Modules.Payment.PaymentApiAdapters.GateWay.Zibal
{
    internal class ZibalClient
    {

        string _merchant = "";
        IExchangeHttpClient _client;
        ZibalGatewayURLs _gatewayURLs;
        public ZibalClient(IOptions<ZibalGatewayURLs> gatewayURLs, IExchangeHttpClient client)
        {
            _gatewayURLs = gatewayURLs.Value;
            _client = client;

        }
        public async Task<ZibalPurchaseResponse> Purchase(ZibalPurchaseRequest purchaseRequest)
        {
            var zibalrequest = new
            {
                purchaseRequest.mobile,
                purchaseRequest.allowedCards,
                purchaseRequest.amount,
                callbackUrl = _gatewayURLs.CallBackUri,
                purchaseRequest.orderId,
                merchant = _merchant,

            };
            var response = await _client.Execute<object, ZibalPurchaseResponse>(zibalrequest, null, _gatewayURLs.Purchase, HttpMethod.Post);
            if (response.result != 100)
            {
                throw new ApiException(response.ToString());
            }
            return response;
        }
        public async Task<ZibalVerifyResponse> Verify(ZibalVerifyRequest purchaseRequest)
        {
            var zibalrequest = new
            {
                purchaseRequest.trackId,
                merchant = _merchant,

            };
            var response = await _client.Execute<object, ZibalVerifyResponse>(zibalrequest, null, _gatewayURLs.Verify, HttpMethod.Post);
            if (response.result != 100)
            {
                throw new ApiException(response.ToString());
            }
            return response;

        }
    }

}
