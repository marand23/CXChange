using CXChange.API.Modules.Payment.Maps;
using CXChange.API.ResourceFiles;
using CXChange.BuildingBlocks.Contract.Mapper;
using CXChange.Modules.Payment.Application.Commands.BankCardAdd;
using CXChange.Modules.Payment.Application.Commands.IRTDepositReject;
using CXChange.Modules.Payment.Application.Commands.IRTDepositSubmit;
using CXChange.Modules.Payment.Application.Commands.ShabaAdd;
using CXChange.Modules.Payment.Application.Commands.TomanDepositConfirm;
using CXChange.Modules.Payment.Application.Commands.TomanDepositVerify;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;
using static CXChange.API.Modules.Payment.Requests;


namespace CXChange.API.Modules.Payment
{
    [Route("api/payment/[controller]")]
    [ApiController]
    public class BankCardController : ExchangeControllerBase
    {

        public BankCardController(IScopedMediator mediator, IMapService mapService) : base(mediator, mapService)
        {

        }
        [HttpPost("add")]
        public async Task<ActionResult<BankCardSubmitted>> AddBankCard(SubmitBankCardRequest request)
        {
            return await ReturnResponse<SubmitBankCard, BankCardSubmitted>(new SubmitBankCard { BankName = request.bankName, CardNumber = request.cardNumber, UserId = Guid.Parse("08dbe56e-1555-b324-92e8-68d2b8970000") });
        }
        [HttpPost("add/shaba")]
        public async Task<ActionResult<ShabaSubmitted>> AddShaba(SubmitShabaRequest request)
        {
            return await ReturnResponse<SubmitShaba, ShabaSubmitted>(new SubmitShaba { BankName = request.bankName, ShabaNumber = request.shabaNumber, UserId = Guid.Parse("08dbdd30-9b5e-332d-92e8-68d2507c0000") });
        }

        [HttpGet("deposit")]
        public async Task<ActionResult<IRTDepositSubmitted>> InitiateTomanDeposit(string userId, string cardNumber, decimal amount)
        {

            return await ReturnResponse<SubmitIRTDeposit, IRTDepositSubmitted>(new SubmitIRTDeposit { Amount = amount, CardNumber = cardNumber, UserId = Guid.Parse(userId) });
        }
        [HttpGet("gateway/zibal/callback")]
        public async Task<ActionResult> VerifyIRTDeposit([FromQuery] ZibalGateWayCallbackInfo zibal)
        {
            if (zibal.success == 1)
            {
                var successResult = await GetRawResponse<VerifyIRTDeposit, IRTDepositVerified>(zibal.MapToConfirmDeposit());
                return Ok(successResult);
            }
            var failureResult = await GetRawResponse<RejectIRTDeposit, IRTDepositRejected>(zibal.MapToRejectDeposit());
            string err = GatewayErrorMessages.ResourceManager.GetString(failureResult.Error.ToString());
            return Redirect("https://localhost:7273/swagger/index.html");


        }
        [HttpPost("gateway/jibit/callback")]
        public async Task<ActionResult> ConfirmDeposit([FromQuery] JibitGateWayCallbackInfo jibit)
        {
            throw new NotImplementedException();
            //if (jibit.status == "SUCCESSFUL")
            //{
            //    var result = await GetRawResponse<VerifyIRTDeposit, IRTDepositVerified>(jibit.MapToVerifyIRTDeposit());
            //    return Ok(result);

            //}
            //var failureResult = await GetRawResponse<RejectIRTDeposit, IRTDepositRejected>(jibit.MapToRejectIRTDeposit());
            //string err = GatewayErrorMessages.ResourceManager.GetString(failureResult.Error.ToString());
            //return BadRequest(new ApiErrorResponse { code = (GatewatPurchaseError)failureResult.Error, data = jibit.purchaseId, message = err, success = false });


        }

    }
}
