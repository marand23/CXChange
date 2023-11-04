using CXChange.API.Modules.Kyc.Requests;
using CXChange.BuildingBlocks.Contract.Mapper;
using CXChange.Modules.Kyc.Application.Commands.Kyc;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;

namespace CXChange.API.Modules.Kyc
{
    [Route("api/kyc")]
    [ApiController]
    public class KycController : ExchangeControllerBase
    {
        public KycController(IScopedMediator mediator, IMapService mapService) : base(mediator, mapService)
        {
        }
        [HttpPost("kyc-request")]

        public async Task<ActionResult<KycInitiated>> DoKyc([FromForm] KycRequest requset)
        {

            return await ReturnResponse<InitiateKyc, KycInitiated>(new InitiateKyc { Image = requset.Image, BithDate = requset.BirthDate, Name = requset.Name, SurName = requset.SurName, NationalId = requset.NationalId, UserId = requset.UserId });



        }
    }
}

