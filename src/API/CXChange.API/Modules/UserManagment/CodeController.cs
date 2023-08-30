using CXChange.API.Modules.UserManagment.Requests;
using CXChange.Modules.UserAccess.Application.Commands.VerificationCode.CodeSendToAuthenticatedUser;
using CXChange.Modules.UserAccess.Application.Commands.VerificationCode.CodeSendToUnAuthenticatedUser;
using CXChange.Modules.UserAccess.Application.Commands.VerificationCode.CodeValidate;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;

namespace CXChange.API.Modules.UserManagment
{
    [Route("api/auth/[controller]")]
    [ApiController]
    public class CodeController : ExchangeControllerBase
    {
        public CodeController(IMediator mediator) : base(mediator)
        {
        }
        [HttpGet("send1")]

        public async Task<ActionResult<CodeToUnAuthenticatedUserSent>> SendCodeToUnAuthenticatedUser(string phoneNumber, string codeType, string token)
        {
            return await ReturnResponse<SendCodeToUnAuthenticatedUser, CodeToUnAuthenticatedUserSent>(new SendCodeToUnAuthenticatedUser { PhoneNumber = phoneNumber, Token = token, CodeType = codeType });
        }

        [HttpGet("send")]
        //[Authorize]
        public async Task<ActionResult<CodeSendedToAuthenticatedUser>> SendCodeToAuthenticatedUser(string phoneNumber, string codeType, string token)
        {
            return await ReturnResponse<SendCodeToAuthenticatedUser, CodeSendedToAuthenticatedUser>(new SendCodeToAuthenticatedUser { UserId = "", Token = token, CodeType = codeType });
        }
        [HttpPost("verify")]
        public async Task<ActionResult<CodeValidated>> ValidateCode(CodeVerifyRequest reqeuest)
        {
            return await ReturnResponse<ValidateCode, CodeValidated>(new ValidateCode { Code = reqeuest.Code, CodeType = reqeuest.CodeType, UserId = "4591a062-10ad-40f1-8b97-5def14498fa9" });
        }
    }
}
