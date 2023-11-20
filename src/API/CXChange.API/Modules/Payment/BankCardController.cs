using CXChange.API.Modules.Payment.GateWayAdapter;
using CXChange.BuildingBlocks.Contract.Mapper;
using CXChange.Modules.Payment.Application.Commands.BankCardAdd;
using CXChange.Modules.Payment.Application.Commands.ShabaAdd;
using CXChange.Modules.Payment.Application.Commands.TomanDepositConfirm;
using CXChange.Modules.Payment.Application.Commands.TomanDepositInitiate;
using CXChange.Modules.Payment.Application.Commands.TomanDepositVerify;
using CXChange.Modules.Payment.Application.Commands.WalletAdd;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using static CXChange.API.Modules.Payment.Requests;


namespace CXChange.API.Modules.Payment
{
    [Route("api/payment/[controller]")]
    [ApiController]
    public class BankCardController : ExchangeControllerBase
    {
        zibal _gateway;
        public BankCardController(IScopedMediator mediator, IMapService mapService, IOptions<zibal> options) : base(mediator, mapService)
        {
            _gateway = options.Value;
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
        [HttpPost("add-wallet")]
        public async Task<ActionResult<WalletAdded>> AddWallet(string userId, string symbol)
        {
            return await ReturnResponse<AddWallet, WalletAdded>(new AddWallet { UserId = Guid.Parse(userId), Currency = symbol });
        }
        [HttpGet("deposit")]
        public async Task<ActionResult<TomanDepositInitiated>> InitiateTomanDeposit(string userId, string cardNumber, decimal amount)
        {
            var a = _gateway;
            return await ReturnResponse<InitiateTomanDeposit, TomanDepositInitiated>(new InitiateTomanDeposit { Amount = amount, CardNumber = cardNumber, UserId = Guid.Parse(userId) });
        }
        [HttpGet("deposit/confirm")]
        public async Task<ActionResult<TomanDepositVerified>> ConfirmDeposit([FromQuery] ZibalGateWayRequest zibal)
        {

            return await ReturnResponse<VerifyTomanDeposit, TomanDepositVerified>(_mapService.Map<ZibalGateWayRequest, VerifyTomanDeposit>(zibal));
        }
        [HttpGet("deposit/next")]
        public async Task<ActionResult<TomanDepositVerified>> ConfirmDeposit([FromQuery] NextGateWayRequest next, [FromKeyedServices("next")] IGateWayAdapter gateWayAdapter)
        {
            return await ReturnResponse<VerifyTomanDeposit, TomanDepositVerified>(gateWayAdapter.Adapt(next));
        }
    }
}
