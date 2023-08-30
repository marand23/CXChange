using CXChange.API.Modules.UserManagment.Requests.TwoFactor;
using CXChange.Modules.UserAccess.Application.Commands.Authenticator.AuthenticatorConfirm;
using CXChange.Modules.UserAccess.Application.Commands.Authenticator.AuthenticatorInitiate;
using CXChange.Modules.UserAccess.Application.Commands.EmailActivate;
using CXChange.Modules.UserAccess.Application.Commands.UserTwoFactorTypeChangeConfirm;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;

namespace CXChange.API.Modules.UserManagment
{
    [Route("api/auth/[controller]")]
    [ApiController]
    public class TwoFactorController : ExchangeControllerBase
    {
        public TwoFactorController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost("change")]

        public async Task<ActionResult<UserTwoFactorTypeChanged>> ChangeDefaultTwoFactor(ChangeTwoFactorTypeRequest request)
        {
            return await ReturnResponse<ConfirmUserTwoFactorTypeChange, UserTwoFactorTypeChanged>(new ConfirmUserTwoFactorTypeChange { NewTwoFactorType = request.NewTwoFactorType, Token = request.Token });
        }

        [HttpPost("add/email")]

        public async Task<ActionResult<EmailActivated>> ActivateEmail(ActivateEmailRequest request)
        {

            return await ReturnResponse<ActivateEmail, EmailActivated>(new ActivateEmail { Email = request.Email, Token = request.Token });
        }
        [HttpPost("add/authenticator")]
        public async Task<ActionResult<AuthenticatorInitiated>> InitiateAuthenticator()
        {
            return await ReturnResponse<InitiateAuthenticator, AuthenticatorInitiated>(new InitiateAuthenticator { UserId = "4591a062-10ad-40f1-8b97-5def14498fa9" });
        }
        [HttpPost("confirm/authenticator")]
        public async Task<ActionResult<AuthenticatorConfirmed>> ConfirmAuthenticator(ConfrimAuthenticatorRequest request)
        {
            return await ReturnResponse<ConfirmAuthenticator, AuthenticatorConfirmed>(new ConfirmAuthenticator { CodeToken = request.CodeToken, AuthToken = request.AuthToken, UserId = "4591a062-10ad-40f1-8b97-5def14498fa9" });
        }

    }
}
