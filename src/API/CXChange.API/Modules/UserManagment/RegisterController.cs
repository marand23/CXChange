using CXChange.BuildingBlocks.Contract.Mapper;
using Microsoft.AspNetCore.Mvc;

namespace CXChange.API.Modules.UserManagment
{
    [Route("api/auth/[controller]")]
    [ApiController]

    public class RegisterController : ExchangeControllerBase
    {

        public RegisterController(MassTransit.Mediator.IMediator mediator, IMapperService mapperService) : base(mediator, mapperService)
        {

        }

        //[HttpPost("")]
        //public async Task<ActionResult<UserRegistered>> RegisterUser(RegisterRequest request)
        //{
        //    return await ReturnResponse<RegisterRequest, RegisterUser, UserRegistered>(request);
        //}
        //[HttpPost("confirm")]
        //public async Task<ActionResult<UserConfirmed>> ConfirmRegistration(UserConfirmationRequest request)
        //{

        //    return await ReturnResponse<UserConfirmationRequest, ConfirmUser, UserConfirmed>(request);

        //}


    }
}
