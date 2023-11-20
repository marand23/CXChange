using CXChange.BuildingBlocks.Infrastructure.HttpClient;
using CXChange.Modules.Payment.PaymentApiAdapters.GateWay.Jibit.Purchase;
using CXChange.Modules.Payment.PaymentApiAdapters.GateWay.Jibit.Verify;
using Microsoft.Extensions.Options;

namespace CXChange.Modules.Payment.PaymentApiAdapters.GateWay.Jibit
{
    internal class JibitClient
    {
        IExchangeHttpClient _client;
        string _gatewayToken = "";
        JibitGatewayURLs _gatewayURLs;

        public JibitClient(IOptions<JibitGatewayURLs> gatewayURLs, IExchangeHttpClient client)
        {
            _gatewayURLs = gatewayURLs.Value;
            _client = client;

        }
        public async Task<JibitPurchaseResponse> Purchase(JibitPurchaseRequest purchaseRequest)
        {

            var response = await _client.Execute<JibitPurchaseRequest, JibitPurchaseResponse>(purchaseRequest, null, _gatewayURLs.Purchase, HttpMethod.Post);
            return response;
        }
        public async Task<JibitVerifyResponse> Verify(JibitVerifyRequest verifyRequest)
        {
            var response = await _client.Execute<JibitVerifyRequest, JibitVerifyResponse>(verifyRequest, null, _gatewayURLs.Verify, HttpMethod.Get);
            return response;
        }





    }

}
