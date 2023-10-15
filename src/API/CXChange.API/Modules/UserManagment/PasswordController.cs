using CXChange.BuildingBlocks.Contract.Mapper;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;

namespace CXChange.API.Modules.UserManagment
{
    [Route("api/auth/[controller]")]
    [ApiController]
    public class PasswordController : ExchangeControllerBase
    {
        public PasswordController(IMediator mediator, IMapperService _mapperService) : base(mediator, _mapperService)
        {
        }

        //[HttpPost("forget/confirm")]
        //public async Task<ActionResult<UserPasswordReset>> ConfirmPasswordReset(PasswordResetConfirmRequest request)
        //{
        //    return await ReturnResponse<PasswordResetConfirmRequest, ResetUserPasswordConfirm, UserPasswordReset>(request);
        //}
        //[HttpGet("forget/verify")]
        //public async Task<ActionResult<ResetPasswordCodeVerified>> VerifyPassowrdResetCode(string code, string Phonenumber)
        //{
        //    return await ReturnResponse<VerifyResetPasswordCode, ResetPasswordCodeVerified>(new VerifyResetPasswordCode { PhoneNumber = Phonenumber, VerificationCode = code });
        //}


    }
}
