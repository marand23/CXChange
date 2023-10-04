using CXChange.API.Modules.UserManagment.Requests.Password;
using CXChange.Modules.UserAccess.Application.Commands.PasswordReset.UserResetPasswordConfirm;
using CXChange.Modules.UserAccess.Application.Commands.PasswordReset.UserResetPasswordVerifCode;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;

namespace CXChange.API.Modules.UserManagment
{
    [Route("api/auth/[controller]")]
    [ApiController]
    public class PasswordController : ExchangeControllerBase
    {
        public PasswordController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost("forget/confirm")]
        public async Task<ActionResult<UserPasswordReset>> ConfirmPasswordReset(PasswordResetConfirmRequest request)
        {
            return await ReturnResponse<ResetUserPasswordConfirm, UserPasswordReset>(new ResetUserPasswordConfirm { NewPassword = request.NewPassword, PhoneNumber = request.PhoneNumber, Token = request.Token });
        }
        [HttpGet("forget/verify")]
        public async Task<ActionResult<ResetPasswordCodeVerified>> VerifyPassowrdResetCode(string code, string Phonenumber)
        {
            return await ReturnResponse<VerifyResetPasswordCode, ResetPasswordCodeVerified>(new VerifyResetPasswordCode { PhoneNumber = Phonenumber, VerificationCode = code });
        }


    }
}
