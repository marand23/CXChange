using CXChange.API.Modules.UserManagment.Requests.UserRegister;
using CXChange.Modules.UserAccess.Application.Commands.Register.UserRegisterConfirmation;
using CXChange.Modules.UserAccess.Application.Commands.Register.UserRegistration;
using Microsoft.AspNetCore.Mvc;

namespace CXChange.API.Modules.UserManagment
{
    [Route("api/auth/[controller]")]
    [ApiController]

    public class RegisterController : ExchangeControllerBase
    {

        public RegisterController(MassTransit.Mediator.IMediator mediator, Serilog.ILogger logger) : base(mediator)
        {

        }

        [HttpPost("")]
        public async Task<ActionResult<UserRegistered>> RegisterUser(RegisterRequest request)
        {



            return await ReturnResponse<RegisterUser, UserRegistered>(new RegisterUser { Password = request.Password, PhoneNumber = request.PhoneNumber });

        }
        [HttpPost("confirm")]
        public async Task<ActionResult<UserConfirmed>> ConfirmRegistration(UserConfirmationRequest request)
        {

            return await ReturnResponse<ConfirmUser, UserConfirmed>(new ConfirmUser { PhoneNumber = request.PhoneNumber, VerificationCode = request.VerificationCode });
        }






    }


}
