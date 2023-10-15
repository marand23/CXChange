using CXChange.API.Modules.UserManagment.Requests.Code;
using CXChange.API.Modules.UserManagment.Requests.User;
using CXChange.BuildingBlocks.Contract.Mapper;
using CXChange.Modules.UserAccess.Application.Commands.VerificationCode.AuthenticatorCodeValidate;
using CXChange.Modules.UserAccess.Application.Commands.VerificationCode.CodeValidate;
using CXChange.Modules.UserAccess.Application.Commands.VerificationCode.EmailCodeSend;
using CXChange.Modules.UserAccess.Application.Commands.VerificationCode.EmailCodeValidate;
using CXChange.Modules.UserAccess.Application.Commands.VerificationCode.PhoneNumberCodeSend;
using CXChange.Modules.UserAccess.Application.Queries.Users;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CXChange.API.Modules.UserManagment
{
    [Route("api/auth/[controller]")]
    [ApiController]
    public class CodeController : ExchangeControllerBase
    {

        public CodeController(IMediator mediator, IMapperService _mapService) : base(mediator, _mapService)
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
        [HttpPost]
        public async Task<ActionResult<UserList>> GetUsers(GetUsersRequest request)
        {

            return await ReturnResponse<GetUsers, UserList>(_mapService.Map<GetUsersRequest, GetUsers>(request));
        }
        //[HttpGet]
        //[Authorize(AuthenticationSchemes = "CustomScheme")]
        //public async Task<ActionResult<Result>> GetUser()
        //{
        //    return await ReturnResponse<GetUser, Result>(new GetUser { Filters = new List<FilterItem> { new FilterItem { Field = "id", Value = UserId } } });

        //}

    }
}
