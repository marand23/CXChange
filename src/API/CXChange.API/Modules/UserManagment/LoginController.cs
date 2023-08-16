using CXChange.API.Modules.UserManagment.Requests.UserLogin;
using CXChange.Modules.UserAccess.Application.Commands.ConfirmLogin;
using CXChange.Modules.UserAccess.Application.Commands.UserLogin;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;

namespace CXChange.API.Modules.UserManagment
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ExchangeControllerBase
    {
        public LoginController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost("")]
        public async Task<ActionResult<UserLoginChecked>> LogIn(CheckLoginRequest request)
        {
            return await ReturnResponse<CheckUserLogin, UserLoginChecked>(new CheckUserLogin { PhoneNumber = request.PhoneNumber, Password = request.Password });
        }
        [HttpPost("confirm")]
        public async Task<ActionResult<UserLoginConfirmed>> ConfirmLogin(ConfirmLoginRequest request)
        {
            return await ReturnResponse<ConfirmUserLogin, UserLoginConfirmed>(new ConfirmUserLogin { PhoneNumber = request.PhoneNumber, Token = request.Token, VerificationCode = request.VerificationCode });
        }
    }
}
