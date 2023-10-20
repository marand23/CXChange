using CXChange.API.Modules.UserManagment.Requests.AuthenticationTickets;
using CXChange.API.Modules.UserManagment.Requests.Code;
using CXChange.BuildingBlocks.Contract.Mapper;
using CXChange.Modules.UserAccess.Application.Commands.VerificationCode.AuthenticatorCodeValidate;
using CXChange.Modules.UserAccess.Application.Commands.VerificationCode.CodeValidate;
using CXChange.Modules.UserAccess.Application.Commands.VerificationCode.EmailCodeSend;
using CXChange.Modules.UserAccess.Application.Commands.VerificationCode.EmailCodeValidate;
using CXChange.Modules.UserAccess.Application.Commands.VerificationCode.PhoneNumberCodeSend;
using CXChange.Modules.UserAccess.Application.Queries.UserAuthenticationTickets;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CXChange.API.Modules.UserManagment
{
    [Route("api/auth/[controller]")]
    [ApiController]
    public class CodeController : ExchangeControllerBase
    {

        public CodeController(IScopedMediator mediator, IMapperService _mapService) : base(mediator, _mapService)
        {

        }
        [HttpGet("send/mobile")]
        [Authorize(policy: "codeTypePolicy", AuthenticationSchemes = "CustomScheme")]

        public async Task<ActionResult<CodeToPhoneNumberSent>> SendCodeToPhoneNumber(SendPhoneNumberCodeRequest request)
        {
            return await ReturnResponse<SendCodeToPhoneNumber, CodeToPhoneNumberSent>(new SendCodeToPhoneNumber { });
        }

        [HttpGet("send/email")]
        [Authorize(policy: "codeTypePolicy", AuthenticationSchemes = "CustomScheme")]
        public async Task<ActionResult<CodeToEmailSent>> SendCodeToEmail(SendEmailCodeRequest request)
        {
            return await ReturnResponse<SendCodeToEmail, CodeToEmailSent>(new SendCodeToEmail { });

        }
        [HttpPost("verify/mobile")]
        [Authorize]

        [Authorize(AuthenticationSchemes = "CustomScheme")]
        public async Task<ActionResult<PhoneNumberCodeValidated>> ValidatePhoneNumberCode(ValidatePhoneCodeRequest request)
        {

            return await ReturnResponse<ValidatePhoneNumberCode, PhoneNumberCodeValidated>(new ValidatePhoneNumberCode { });
        }
        [HttpPost("verify/email")]
        [Authorize(AuthenticationSchemes = "CustomScheme")]
        public async Task<ActionResult<EmailCodeValidated>> ValidateEmailCode(ValidateEmailCodeRequest request)
        {
            return await ReturnResponse<ValidateEmailCode, EmailCodeValidated>(new ValidateEmailCode { });
        }
        [HttpPost("verify/totp")]
        [Authorize(AuthenticationSchemes = "CustomScheme")]
        public async Task<ActionResult<AuthenticatorCodeValidated>> ValidateAuthenticatorCode(ValidateAuthenticatorCodeRequest request)
        {
            return await ReturnResponse<ValidateAuthenticatorCode, AuthenticatorCodeValidated>(new ValidateAuthenticatorCode { });
        }

        [HttpGet]

        public async Task<ActionResult<TicketList>> GetUser([FromQuery] GetUserAuthenticationTicketsRequest request)
        {
            return await ReturnResponse<GetUserAuthenticationTickets, TicketList>(_mapService.Map<GetUserAuthenticationTicketsRequest, GetUserAuthenticationTickets>(request));

        }

    }
}
