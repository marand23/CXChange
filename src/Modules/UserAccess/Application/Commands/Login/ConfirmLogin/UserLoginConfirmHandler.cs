using CXChange.BuildingBlocks.Application.Totp;
using CXChange.BuildingBlocks.Domain;
using CXChange.Modules.UserAccess.Application.Enums;
using CXChange.Modules.UserAccess.Application.Exceptions;
using CXChange.Modules.UserAccess.Application.Services.Interfaces;
using CXChange.Modules.UserAccess.Application.Utils;
using CXChange.Modules.UserAccess.Domain.Interfaces.Repositories;
using CXChange.Modules.UserAccess.Domain.User;
using MassTransit;
using Microsoft.AspNetCore.Http;


namespace CXChange.Modules.UserAccess.Application.Commands.Login.ConfirmLogin
{
    internal class UserLoginConfirmHandler : IConsumer<ConfirmUserLogin>
    {
        IHttpContextAccessor _httpContextAccessor;

        IUserRepository _userRepository;

        IDomainEventDispatcher _dispatcher;
        IVerificationCodeFunctions _verifier;
        IAuthenticator _authenticator;
        public UserLoginConfirmHandler(IUserRepository userRepository, IHttpContextAccessor httpContextAccessor, IDomainEventDispatcher dispatcher, IVerificationCodeFunctions verifier, IAuthenticator authenticator)
        {
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
            _verifier = verifier;
            _dispatcher = dispatcher;
            _authenticator = authenticator;
        }
        public async Task Consume(ConsumeContext<ConfirmUserLogin> context)
        {

            var user = await _userRepository.GetUserByPhoneNumber(context.Message.PhoneNumber);
            if (user == null)
            {
                throw new UserAccessCommandException(CommandExceptionEnum.UserNotFound);


            }
            var tokenResult = await _userRepository.ValidateUserToken(user, context.Message.Token, KeyWords.CustomTokenProvider, KeyWords.LoginUser);
            if (!tokenResult)
            {
                throw new UserAccessCommandException(CommandExceptionEnum.UserLoginTokenNotValid);

            }
            bool codeResult = false;
            if (user.GetSnapShot().ActiveTwoFactor == TwoFactorEnum.AUTHENTICATOR)
            {
                var key = await _userRepository.GetAuthenticatorKey(user);
                codeResult = _authenticator.ValidateTwoFactorPIN(key, context.Message.VerificationCode, TimeSpan.FromSeconds(30));
            }
            else
            {

                codeResult = _verifier.Verify(user, int.Parse(context.Message.VerificationCode), CodeType.GeneratePurposeForDefaultTwoFactor(user.GetSnapShot(), KeyWords.LoginUser));
            }
            ;
            if (!codeResult)
            {
                throw new UserAccessCommandException(CommandExceptionEnum.VerificationCodeInvalid);
            }

            user.ConfirmUserLogin();
            await _userRepository.SignIn(user);
            var token = (string)_httpContextAccessor.HttpContext.Items["token"];
            user.UpdateSecurityStamp();
            await _userRepository.Update(user);
            /*await*/
            await _dispatcher.Dispatch(user.DomainEvents);

            await context.RespondAsync(new UserLoginConfirmed { Token = token });
        }
    }
}
