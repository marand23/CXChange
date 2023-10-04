using CXChange.BuildingBlocks.Application.Totp;
using CXChange.Modules.UserAccess.Application.Enums;
using CXChange.Modules.UserAccess.Application.Exceptions;
using CXChange.Modules.UserAccess.Application.Utils;
using CXChange.Modules.UserAccess.Domain.Interfaces.Repositories;
using CXChange.Modules.UserAccess.Domain.User;
using MassTransit;
using Microsoft.Extensions.Caching.Memory;

namespace CXChange.Modules.UserAccess.Application.Commands.VerificationCode.AuthenticatorCodeValidate
{
    internal class ValidateAuthenticatorCodeHandler : IConsumer<ValidateAuthenticatorCode>
    {
        IUserRepository _repo;
        IAuthenticator _authenticator;
        IMemoryCache _memoryCache;
        public ValidateAuthenticatorCodeHandler(IUserRepository repo, IAuthenticator authenticator, IMemoryCache memoryCache)
        {
            _repo = repo;
            _authenticator = authenticator;
            _memoryCache = memoryCache;
        }
        public async Task Consume(ConsumeContext<ValidateAuthenticatorCode> context)
        {
            var user = await _repo.GetUserById(context.Message.UserId);
            if (user == null)
            {
                throw new UserAccessCommandException(CommandExceptionEnum.UserNotFound);
            }
            //get from memory 
            var key = await _repo.GetAuthenticatorKey(user);
            var result = _authenticator.ValidateTwoFactorPIN(key, context.Message.Code, TimeSpan.FromSeconds(30));
            if (!result)
            {
                throw new UserAccessCommandException(CommandExceptionEnum.VerificationCodeInvalid);
            }
            var token = await _repo.GenerateUserToken(user, KeyWords.CustomTokenProvider, CodeType.GeneratePurposeForSpecificTwoFactor(user.GetSnapShot().Id, context.Message.CodeType, TwoFactorEnum.AUTHENTICATOR.ToString()));
            await context.RespondAsync(new AuthenticatorCodeValidated { Token = token });
        }
    }
}
