using CXChange.BuildingBlocks.Application;
using CXChange.Modules.Payment.Application.GateWayAdapter;
using CXChange.Modules.Payment.Application.GateWayAdapter.Inquiry;
using CXChange.Modules.Payment.Application.GateWayAdapter.InquiryWithdraw;
using CXChange.Modules.Payment.Application.GateWayAdapter.Purchase;
using CXChange.Modules.Payment.Application.GateWayAdapter.RetryWithdraw;
using CXChange.Modules.Payment.Application.GateWayAdapter.Verify;
using CXChange.Modules.Payment.Application.GateWayAdapter.Withdraw;
using CXChange.Modules.Payment.PaymentApiAdapters.GateWay.Zibal.Inquiry;
using CXChange.Modules.Payment.PaymentApiAdapters.GateWay.Zibal.Purchase;
using CXChange.Modules.Payment.PaymentApiAdapters.GateWay.Zibal.Verify;
using CXChange.Modules.Payment.PaymentApiAdapters.GateWay.Zibal.Withdraw;
using CXChange.Modules.Payment.PaymentApiAdapters.GateWay.Zibal.WithdrawInquiry;
using CXChange.Modules.Payment.PaymentApiAdapters.GateWay.Zibal.WithdrawInquiry.Maps;
using Microsoft.Extensions.Options;
using Serilog;
namespace CXChange.Modules.Payment.PaymentApiAdapters.GateWay.Zibal
{
    internal class Zibal : IGatewayAdapter
    {
        ZibalClient _client;
        ZibalGatewayURLs _gatewayURLs;
        ILogger _logger;
        public Zibal(ZibalClient client, IOptions<ZibalGatewayURLs> gatewayUrls, ILogger logger)
        {
            _client = client;
            _gatewayURLs = gatewayUrls.Value;
            _logger = logger;
        }
        public async Task<Result<GateWayPurchaseResponse>> CreatePurchase(GateWayPurchaseRequest request)
        {


            var requestData = new ZibalPurchaseRequest
            {
                amount = (int)request.Amount * 10,
                allowedCards = new[] { request.CardNumber },
                mobile = request.PhoneNumber,
                orderId = request.DepositId
            };
            var result = await _client.Purchase(requestData);
            if (result.Success)
            {
                if (result.Data.result == 100)
                {
                    return Result.OK(new GateWayPurchaseResponse
                    {
                        PayLink = $"{_gatewayURLs.PayLink}{result.Data.trackId}",
                        TrackId = result.Data.trackId.ToString(),
                    });
                }
                _logger.Error("Zibal purchase failed message:{message} result: {result}", result.Data.message, result.Data.result);
                return Result.CommandError<GateWayPurchaseResponse>(GatewayPurchaseError.GatewayCreateLinkFailed, "gateway did not return expected response");
            }
            _logger.Error("Zibal purchase failed message : {message}", result.Error);
            return Result.CommandError<GateWayPurchaseResponse>(GatewayPurchaseError.GatewayCreateLinkFailed, result.Error);


        }

        public async Task<Result<GateWayVerifyResponse>> Verify(GateWayVerifyRequest request)
        {


            var requestData = new ZibalVerifyRequest
            {
                trackId = long.Parse(request.TrackId)
            };


            var response = await _client.Verify(requestData);
            if (response.Success)
            {
                if (response.Data.result == 100)
                {
                    return Result.OK(new GateWayVerifyResponse { Success = true });
                }
                _logger.Error("Zibal verify failed message:{message} result: {result}", response.Data.message, response.Data.result);
                return Result.CommandError<GateWayVerifyResponse>(GetVerifyError(response.Data.result));
            }
            _logger.Error("zibal verify failed message :{message}", response.Error);
            return Result.CommandError<GateWayVerifyResponse>(GatewayVerifyError.Failed);



        }
        public async Task<GatewayInquiryResponse> Inquiry(GatewayInquiryRequest request)
        {
            var requestData = new ZibalInquiryRequest
            {
                TrackId = long.Parse(request.TrackId)
            };
            var response = await _client.Inquiry(requestData);
            return new GatewayInquiryResponse
            {
                // FailReason = GetFailReason(response.status),
                Status = response.status switch
                {
                    -1 => PurchaseStatus.WaitingForPayment,
                    1 => PurchaseStatus.PaidVerified,
                    2 => PurchaseStatus.PaidUnverified,
                    _ => PurchaseStatus.Failed
                }

            };


        }

        public async Task<Result<SendWithdrawResponse>> SendWithdraw(SendWithdrawRequest request)
        {
            var requestData = new ZibalSendWithdrawRequest
            {
                amount = (long)request.Amount,
                bankAccount = request.Shaba,
                checkoutDelay = -1,
                uniqueCode = request.WithdrawId.ToString(),
            };
            var respopnse = await _client.SendWithdraw(requestData);
            if (respopnse.Success)
            {

                return Result.OK(new SendWithdrawResponse
                {
                    WithdrawId = request.WithdrawId,
                });
            }
            _logger.Error("zibal send withdraw failed message : {message}", respopnse.Error);

            return Result.Fail<SendWithdrawResponse>(respopnse.Error);



        }

        public async Task<Result<InquiryWithdrawResponse>> InquiryWithdraw(InquiryWithdrawRequest request)
        {
            var requestData = new ZibalWithdrawInquiryRequest
            {
                chckoutRequestId = request.WithdrawId.ToString(),
                walletId = "walletId"

            };

            var response = await _client.InquiryWithdraw(requestData);
            if (response.Success)
            {
                return Result.OK(response.Data.MapToInquiryWithdrawResponse());
            }
            _logger.Error("zibal withdraw inquiry failed message :{message}", response.Error);
            return Result.CommandError<InquiryWithdrawResponse>(null, response.Error);
        }
        private GatewayVerifyError GetVerifyError(int result)
        {
            GatewayVerifyError status = result switch
            {

                201 => GatewayVerifyError.AlreadyVerified,
                202 => GatewayVerifyError.FailedOrNotPaidYet,
                _ => GatewayVerifyError.Failed,
            };
            return status;
        }

        public Task<Result<RetryWithdrawResponse>> RetryWithdraw(RetryWithdrawRequest request)
        {
            throw new NotImplementedException();
        }
    }

}
