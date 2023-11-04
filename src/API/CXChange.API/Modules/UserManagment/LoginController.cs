using CXChange.API.Modules.UserManagment.Requests.UserLogin;
using CXChange.BuildingBlocks.Contract.Mapper;
using CXChange.Modules.UserAccess.Application.Commands.Login.ConfirmLogin;
using CXChange.Modules.UserAccess.Application.Commands.Login.UserLogin;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;

namespace CXChange.API.Modules.UserManagment
{
    [Route("api/auth/[controller]")]
    [ApiController]
    public class LoginController : ExchangeControllerBase
    {

        public LoginController(IScopedMediator mediator, IMapService _mapper) : base(mediator, _mapper)
        {
        }

        [HttpPost("")]
        public async Task<ActionResult<UserLoginChecked>> Login(CheckLoginRequest request)
        {
            return await ReturnResponse<CheckUserLogin, UserLoginChecked>(new CheckUserLogin { Password = request.Password, PhoneNumber = request.PhoneNumber });
        }
        [HttpPost("confirm")]
        public async Task<ActionResult<UserLoginConfirmed>> ConfirmLogin(ConfirmLoginRequest request)
        {
            return await ReturnResponse<ConfirmUserLogin, UserLoginConfirmed>(new ConfirmUserLogin { PhoneNumber = request.PhoneNumber, Code = request.Code, Token = request.Token });
        }


    }

}
