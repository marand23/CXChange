using CXChange.BuildingBlocks.Infrastructure.HttpClient;
using CXChange.Modules.Payment.PaymentApiAdapters.GateWay.Jibit.Inquiry;
using CXChange.Modules.Payment.PaymentApiAdapters.GateWay.Jibit.Purchase;
using CXChange.Modules.Payment.PaymentApiAdapters.GateWay.Jibit.RetryWithdraw;
using CXChange.Modules.Payment.PaymentApiAdapters.GateWay.Jibit.Verify;
using CXChange.Modules.Payment.PaymentApiAdapters.GateWay.Jibit.Withdraw;
using CXChange.Modules.Payment.PaymentApiAdapters.GateWay.Jibit.WithdrawInquiry;
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
        public async Task<HttpResult<JibitPurchaseResponse>> Purchase(JibitPurchaseRequest purchaseRequest)
        {


            var response = await _client.Execute<JibitPurchaseRequest, JibitPurchaseResponse>(purchaseRequest, null, _gatewayURLs.Purchase, HttpMethod.Post);

            return response;
        }
        public async Task<HttpResult<JibitVerifyResponse>> Verify(JibitVerifyRequest verifyRequest)
        {

            var response = await _client.Execute<JibitVerifyRequest, JibitVerifyResponse>(verifyRequest, null, _gatewayURLs.GetVerifyURL(verifyRequest.purchaseId), HttpMethod.Get);
            return response;
        }
        public async Task<JibitInquiryResponse> Inquiry(JibitInquiryRequest inquiryRequest)
        {
            throw new NotImplementedException();
            //var response = await _client.Execute<JibitInquiryRequest, JibitInquiryResponse>(inquiryRequest, null, _gatewayURLs.GetInquiryURL(inquiryRequest.PurchaseId), HttpMethod.Get);
            //return response;
        }
        public async Task<HttpResult<JibitSendWithdrawResponse>> SendWithdraw(JibitSendWithdrawRequest sendWithdrawRequest)
        {

            var response = await _client.Execute<JibitSendWithdrawRequest, JibitSendWithdrawResponse>(sendWithdrawRequest, null, _gatewayURLs.SendWithdraw, HttpMethod.Post);
            return response;
        }
        public async Task<HttpResult<JibitWithdrawalInquiryResponse>> InquiryWithdraw(JibitWithdrawalInquiryRequest inquiryRequest)
        {
            return await _client.Execute<JibitWithdrawalInquiryRequest, JibitWithdrawalInquiryResponse>(null, null, _gatewayURLs.InquriyWithdraw, HttpMethod.Get);
        }
        public async Task<HttpResult<JibitRetryWithdrawResponse>> RetryWithdraw(JibitRetryWithdrawRequest retryWithdrawRequest)
        {
            return await _client.Execute<JibitRetryWithdrawRequest, JibitRetryWithdrawResponse>(retryWithdrawRequest, null, _gatewayURLs.RetryWithdraw, HttpMethod.Patch);
        }





    }

}
