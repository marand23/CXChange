using CXChange.BuildingBlocks.Application;
using CXChange.Modules.Payment.Application.GateWayAdapter;
using CXChange.Modules.Payment.Application.GateWayAdapter.Inquiry;
using CXChange.Modules.Payment.Application.GateWayAdapter.InquiryWithdraw;
using CXChange.Modules.Payment.Application.GateWayAdapter.Purchase;
using CXChange.Modules.Payment.Application.GateWayAdapter.RetryWithdraw;
using CXChange.Modules.Payment.Application.GateWayAdapter.Verify;
using CXChange.Modules.Payment.Application.GateWayAdapter.Withdraw;
using CXChange.Modules.Payment.PaymentApiAdapters.GateWay.Jibit.Inquiry;
using CXChange.Modules.Payment.PaymentApiAdapters.GateWay.Jibit.Purchase;
using CXChange.Modules.Payment.PaymentApiAdapters.GateWay.Jibit.RetryWithdraw;
using CXChange.Modules.Payment.PaymentApiAdapters.GateWay.Jibit.Verify;
using CXChange.Modules.Payment.PaymentApiAdapters.GateWay.Jibit.Withdraw;
using CXChange.Modules.Payment.PaymentApiAdapters.GateWay.Jibit.WithdrawInquiry;
using Microsoft.Extensions.Options;
using Serilog;

namespace CXChange.Modules.Payment.PaymentApiAdapters.GateWay.Jibit
{
    internal class Jibit : IGatewayAdapter
    {
        JibitClient _jibitClient;
        JibitGatewayURLs _jibitGatewayURLs;
        ILogger _logger;
        public Jibit(JibitClient jibitClient, IOptions<JibitGatewayURLs> gatewayURLs, ILogger logger)
        {

            _jibitClient = jibitClient;
            _jibitGatewayURLs = gatewayURLs.Value;
            _logger = logger;
        }
        public async Task<Result<GateWayPurchaseResponse>> CreatePurchase(GateWayPurchaseRequest request)
        {

            var requestData = new JibitPurchaseRequest
            {
                amount = (int)request.Amount * 10,
                callBackUrl = _jibitGatewayURLs.CallbackUri,
                clientReferenceNumber = request.DepositId,
                payerCardNumber = request.CardNumber,
                describtion = "",
                payerMobileNumber = request.PhoneNumber,
                currency = "IRR"
            };
            var response = await _jibitClient.Purchase(requestData);
            if (response.Success)
            {
                return Result.OK(
                 new GateWayPurchaseResponse
                 {
                     PayLink = response.Data.pspSwitchingUrl,
                     TrackId = response.Data.purchaseIdStr
                 }
                );
            }
            _logger.Error("Jibit create link failed message :{message}", response.Error);
            return Result.CommandError<GateWayPurchaseResponse>(GatewayPurchaseError.GatewayCreateLinkFailed, "jibit create link failed");


        }

        public async Task<GatewayInquiryResponse> Inquiry(GatewayInquiryRequest request)
        {
            var requestData = new JibitInquiryRequest
            {
                PurchaseId = request.TrackId
            };
            var response = await _jibitClient.Inquiry(requestData);
            return new GatewayInquiryResponse
            {
                FailReason = response.pspFailReason,
                Status = response.state switch
                {
                    1 => PurchaseStatus.Failed,
                    3 => PurchaseStatus.PaidUnverified,
                    4 => PurchaseStatus.PaidVerified,
                    6 => PurchaseStatus.Unkkown,

                }
            };
        }

        public async Task<Result<InquiryWithdrawResponse>> InquiryWithdraw(InquiryWithdrawRequest request)
        {
            var requestData = new JibitWithdrawalInquiryRequest
            {
                WithdrawId = request.WithdrawId.ToString()
            };
            var response = await _jibitClient.InquiryWithdraw(requestData);
            if (response.Success)
            {
                return Result.OK(response.Data.MapToInquiryWithdrawResponse());
            }
            _logger.Error("jibit  withdraw inquiry failed message : {messsage}", response.Error);
            return Result.CommandError<InquiryWithdrawResponse>(null, response.Message);
        }

        public async Task<Result<RetryWithdrawResponse>> RetryWithdraw(RetryWithdrawRequest request)
        {
            var requestData = new JibitRetryWithdrawRequest
            {
                transferId = request.WithdrawId.ToString(),
            };
            var response = await _jibitClient.RetryWithdraw(requestData);
            if (response.Success)
            {
                return Result.OK(new RetryWithdrawResponse { });
            }
            _logger.Error("jibit retry failed messsage : {message}", response.Error);
            return Result.CommandError<RetryWithdrawResponse>(null, response.Error);
        }

        public async Task<Result<SendWithdrawResponse>> SendWithdraw(SendWithdrawRequest request)
        {

            var requestData = new JibitSendWithdrawRequest
            {
                batchId = Guid.NewGuid().ToString(),
                submissionMode = "",
                transfers =
               [
                     new Transfer
                     {
                         amount = (long)request.Amount,
                         cancelabe = false,
                         currency = "IRT",
                         description = "withdraw",
                         destination = request.Shaba,
                         transferId = request.WithdrawId.ToString()

                     }
               ]
            };
            var response = await _jibitClient.SendWithdraw(requestData);
            if (response.Success)
            {
                if (response.Data.sumbittedCount == 0)
                {
                    //if response status code was 200 but submitted count is 0 it mean the did not submitted
                    return Result.Fail<SendWithdrawResponse>("the count of submmited withdraws are 0");
                }
                return Result.OK(new SendWithdrawResponse { WithdrawId = request.WithdrawId });
            }
            _logger.Error("jibit send withdraw failed message : {messsage}", response.Error);
            return Result.Fail<SendWithdrawResponse>(response.Error);
        }



        public async Task<Result<GateWayVerifyResponse>> Verify(GateWayVerifyRequest request)
        {
            var requestData = new JibitVerifyRequest(request.TrackId, request.CardHash, request.CardNumber);

            var response = await _jibitClient.Verify(requestData);
            if (response.Data.status != "SUCCESSFUL")
            {
                _logger.Error("jibit verify failed message :{message} trackId : {trackId}", response.Error, request.TrackId);
                return Result.CommandError<GateWayVerifyResponse>(GetVerifyError(response.Data.status));
            }
            return Result.OK(new GateWayVerifyResponse { Success = true });

        }

        private GatewayVerifyError GetVerifyError(string status)
        {
            GatewayVerifyError error = status switch
            {
                "ALREADY_VERIFIED" => GatewayVerifyError.AlreadyVerified,
                "FAILED" => GatewayVerifyError.Failed,
                "NOT_VERIFIABLE" => GatewayVerifyError.NotVerifiable,
                "REVERSED" => GatewayVerifyError.Reversed,
                "UNKNOWN" => GatewayVerifyError.Unknown,
                _ => GatewayVerifyError.Unknown
            };
            return error;
        }


    }

}
