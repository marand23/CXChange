using CXChange.BuildingBlocks.Infrastructure.Exceptions;
using CXChange.BuildingBlocks.Infrastructure.HttpClient;
using CXChange.Modules.Payment.PaymentApiAdapters.GateWay.Zibal.Inquiry;
using CXChange.Modules.Payment.PaymentApiAdapters.GateWay.Zibal.Purchase;
using CXChange.Modules.Payment.PaymentApiAdapters.GateWay.Zibal.Verify;
using CXChange.Modules.Payment.PaymentApiAdapters.GateWay.Zibal.Withdraw;
using CXChange.Modules.Payment.PaymentApiAdapters.GateWay.Zibal.WithdrawInquiry;
using Microsoft.Extensions.Options;
using Serilog;

namespace CXChange.Modules.Payment.PaymentApiAdapters.GateWay.Zibal
{
    internal class ZibalClient
    {

        string _merchant = "";
        IExchangeHttpClient _client;
        ZibalGatewayURLs _gatewayURLs;
        ILogger _logger;
        public ZibalClient(IOptions<ZibalGatewayURLs> gatewayURLs, IExchangeHttpClient client, ILogger logger)
        {
            _gatewayURLs = gatewayURLs.Value;
            _client = client;
            _logger = logger;

        }
        public async Task<HttpResult<ZibalPurchaseResponse>> Purchase(ZibalPurchaseRequest purchaseRequest)
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
            return await _client.Execute<object, ZibalPurchaseResponse>(zibalrequest, null, _gatewayURLs.Purchase, HttpMethod.Post);
        }
        public async Task<HttpResult<ZibalVerifyResponse>> Verify(ZibalVerifyRequest verifyRequest)
        {
            var zibalrequest = new
            {
                verifyRequest.trackId,
                merchant = _merchant,

            };
            return await _client.Execute<object, ZibalVerifyResponse>(zibalrequest, null, _gatewayURLs.Verify, HttpMethod.Post);


        }
        public async Task<ZibalInquiryResponse> Inquiry(ZibalInquiryRequest inquiryRequest)
        {
            var zibalRequest = new
            {
                inquiryRequest.TrackId,
                merchant = _merchant
            };
            var response = await _client.Execute<object, ZibalInquiryResponse>(zibalRequest, null, _gatewayURLs.Inquiry, HttpMethod.Post);
            if (response.Success)
            {
                if (response.Data.result != 100)
                {

                    throw new ApiException($"zibal inquiry failed for TrackId:{inquiryRequest.TrackId} failed because {response.ToString()}");
                }
            }
            // _logger.Error("{ErrorData}", response.ErrorData);

            return response.Data;
        }
        public async Task<HttpResult<ZibalSendWithdrawResponse>> SendWithdraw(ZibalSendWithdrawRequest sendWithdrawRequest)
        {

            return await _client.Execute<object, ZibalSendWithdrawResponse>(sendWithdrawRequest, null, _gatewayURLs.SendWithdraw, HttpMethod.Post);

        }
        public async Task<HttpResult<ZibalWithdrawalInquiryResponse>> InquiryWithdraw(ZibalWithdrawInquiryRequest request)
        {
            return await _client.Execute<ZibalWithdrawInquiryRequest, ZibalWithdrawalInquiryResponse>(request, null, _gatewayURLs.InquiryWithdraw, HttpMethod.Post);
        }




    }


}
