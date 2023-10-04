using CXChange.API.Modules.UserManagment.Requests.Code;
using CXChange.Modules.UserAccess.Application.Commands.VerificationCode.AuthenticatorCodeValidate;
using CXChange.Modules.UserAccess.Application.Commands.VerificationCode.CodeSendToUnAuthenticatedUser;
using CXChange.Modules.UserAccess.Application.Commands.VerificationCode.CodeValidate;
using CXChange.Modules.UserAccess.Application.Commands.VerificationCode.EmailCodeSend;
using CXChange.Modules.UserAccess.Application.Commands.VerificationCode.EmailCodeValidate;
using CXChange.Modules.UserAccess.Application.Commands.VerificationCode.PhoneNumberCodeSend;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Authorization;
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
        [HttpGet("send/mobile")]
        [Authorize(policy: "codeTypePolicy", AuthenticationSchemes = "CustomScheme")]

        public async Task<ActionResult<CodeToPhoneNumberSent>> SendCodeToPhoneNumber(string phoneNumber, string codeType, string token)
        {
            return await ReturnResponse<SendCodeToPhoneNumber, CodeToPhoneNumberSent>(new SendCodeToPhoneNumber { PhoneNumber = phoneNumber, Token = token, CodeType = codeType });
        }

        [HttpGet("send/email")]
        [Authorize(policy: "codeTypePolicy", AuthenticationSchemes = "CustomScheme")]
        public async Task<ActionResult<CodeToEmailSent>> SendCodeToEmail(string email, string codeType, string token)
        {
            return await ReturnResponse<SendCodeToEmail, CodeToEmailSent>(new SendCodeToEmail { Email = email, Token = token, CodeType = codeType });
        }
        [HttpPost("verify/mobile")]
        [Authorize]

        [Authorize(AuthenticationSchemes = "CustomScheme")]
        public async Task<ActionResult<PhoneNumberCodeValidated>> ValidatePhoneNumberCode(ValidatePhoneCodeRequest request)
        {

            return await ReturnResponse<ValidatePhoneNumberCode, PhoneNumberCodeValidated>(new ValidatePhoneNumberCode { Code = request.Code, CodeType = request.CodeType, UserId = UserId });
        }
        [HttpPost("verify/email")]
        [Authorize(AuthenticationSchemes = "CustomScheme")]
        public async Task<ActionResult<EmailCodeValidated>> ValidateEmailCode(ValidateEmailCodeRequest request)
        {
            return await ReturnResponse<ValidateEmailCode, EmailCodeValidated>(new ValidateEmailCode { Code = request.Code, CodeType = request.CodeType, UserId = UserId });
        }
        [HttpPost("verify/totp")]
        [Authorize(AuthenticationSchemes = "CustomScheme")]
        public async Task<ActionResult<AuthenticatorCodeValidated>> ValidateAuthenticatorCode(ValidateAuthenticatorCodeRequest request)
        {
            return await ReturnResponse<ValidateAuthenticatorCode, AuthenticatorCodeValidated>(new ValidateAuthenticatorCode { Code = request.Code, CodeType = request.CodeType, UserId = UserId });
        }
    }
}
