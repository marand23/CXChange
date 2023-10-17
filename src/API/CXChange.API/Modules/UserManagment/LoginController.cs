using CXChange.BuildingBlocks.Contract.Mapper;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;

namespace CXChange.API.Modules.UserManagment
{
    [Route("api/auth/[controller]")]
    [ApiController]
    public class LoginController : ExchangeControllerBase
    {

        public LoginController(IScopedMediator mediator, IMapperService _mapper) : base(mediator, _mapper)
        {
        }

        //[HttpPost("")]
        //public async Task<ActionResult<UserLoginChecked>> Login(CheckLoginRequest request)
        //{
        //    return await ReturnResponse<CheckLoginRequest, CheckUserLogin, UserLoginChecked>(request);
        //}
        //[HttpPost("confirm")]
        //public async Task<ActionResult<UserLoginConfirmed>> ConfirmLogin(ConfirmLoginRequest request)
        //{
        //    return await ReturnResponse<ConfirmLoginRequest, ConfirmUserLogin, UserLoginConfirmed>(request);
        //}


    }

}
