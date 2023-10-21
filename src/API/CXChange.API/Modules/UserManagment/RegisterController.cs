using CXChange.API.Modules.UserManagment.Requests.UserRegister;
using CXChange.BuildingBlocks.Contract.Mapper;
using CXChange.Modules.UserAccess.Application.Commands.Register.UserRegisterConfirmation;
using CXChange.Modules.UserAccess.Application.Commands.Register.UserRegistration;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;

namespace CXChange.API.Modules.UserManagment
{
    [Route("api/auth/[controller]")]
    [ApiController]

    public class RegisterController : ExchangeControllerBase
    {

        public RegisterController(IScopedMediator mediator, IMapperService mapperService) : base(mediator, mapperService)
        {

        }

        [HttpPost("")]
        public async Task<ActionResult<UserRegistered>> RegisterUser(RegisterRequest request)
        {
            return await ReturnResponse<RegisterUser, UserRegistered>(new RegisterUser { PhoneNumber = request.PhoneNumber, Password = request.Password });
        }
        [HttpPost("confirm")]
        public async Task<ActionResult<UserConfirmed>> ConfirmRegistration(UserConfirmationRequest request)
        {

            return await ReturnResponse<ConfirmUser, UserConfirmed>(new ConfirmUser { PhoneNumber = request.PhoneNumber, VerificationCode = request.VerificationCode });

        }


    }
}
