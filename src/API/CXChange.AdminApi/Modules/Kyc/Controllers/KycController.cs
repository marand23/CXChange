using CXChange.AdminAPI.Modules.Kyc.Requests;
using CXChange.BuildingBlocks.Contract.Mapper;
using CXChange.Modules.Kyc.Administrator.Application.Commands.KycRequestApprove;
using CXChange.Modules.Kyc.Administrator.Application.Commands.KycRequestReject;
using CXChange.Modules.Kyc.Administrator.Application.Queries.KycRequestsGet;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;

namespace CXChange.AdminAPI.Modules.Kyc.Controllers
{
    [Route("api/[controller]")]
    [ApiController()]

    public class KycController : AdminControllerBase
    {
        public KycController(IScopedMediator mediator, IMapService mapService) : base(mediator, mapService)
        {
        }
        [HttpPost]
        public async Task<ActionResult<KycRequestList>> GetKycRequests(KycRequests request)
        {
            return await ReturnResponse<GetKycRequests, KycRequestList>(_mapService.Map<KycRequests, GetKycRequests>(request));
        }
        [HttpPost("approve-kyc")]
        public async Task<ActionResult<KycRequestApproved>> ApproveKyc(KycApproveRequest request)
        {
            return await ReturnResponse<ApproveKycRequest, KycRequestApproved>(new ApproveKycRequest { UserId = request.UserId });
        }
        [HttpPost("reject-kyc")]
        public async Task<ActionResult<KycRequestRejected>> RejectKyc(KycApproveRequest request)
        {
            return await ReturnResponse<RejectKycRequest, KycRequestRejected>(new RejectKycRequest { UserId = request.UserId });
        }
    }
}
