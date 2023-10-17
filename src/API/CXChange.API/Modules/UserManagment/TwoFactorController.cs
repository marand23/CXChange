using CXChange.BuildingBlocks.Contract.Mapper;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;

namespace CXChange.API.Modules.UserManagment
{
    [Route("api/auth/[controller]")]
    [ApiController]
    public class TwoFactorController : ExchangeControllerBase
    {
        public TwoFactorController(IScopedMediator mediator, IMapperService _mapperService) : base(mediator, _mapperService)
        {
        }

        //[HttpPost("change")]
        //[Authorize(AuthenticationSchemes = "CustomScheme")]
        //public async Task<ActionResult<UserTwoFactorTypeChanged>> ChangeDefaultTwoFactor(ChangeTwoFactorTypeRequest request)
        //{
        //    return await ReturnResponse<ChangeTwoFactorTypeRequest, ConfirmUserTwoFactorTypeChange, UserTwoFactorTypeChanged>(request);
        //}

        //[HttpPost("add/email")]
        //// [Authorize(AuthenticationSchemes = "CustomScheme")]
        //public async Task<ActionResult<EmailActivated>> ActivateEmail(ConfrimAuthenticatorRequest request)
        //{

        //    return await ReturnResponse<ConfrimAuthenticatorRequest, ActivateEmail, EmailActivated>(request);
        //}
        ////[HttpPost("add/authenticator")]
        ////[Authorize(AuthenticationSchemes = "CustomScheme")]
        ////public async Task<ActionResult<AuthenticatorInitiated>> InitiateAuthenticator()
        ////{
        ////    return await ReturnResponse<InitiateAuthenticator, AuthenticatorInitiated>();
        ////}
        //[HttpPost("confirm/authenticator")]
        //[Authorize(AuthenticationSchemes = "CustomScheme")]
        //public async Task<ActionResult<AuthenticatorConfirmed>> ConfirmAuthenticator(ConfrimAuthenticatorRequest request)
        //{
        //    return await ReturnResponse<ConfrimAuthenticatorRequest, ConfirmAuthenticator, AuthenticatorConfirmed>(request);
        //}

    }
}
