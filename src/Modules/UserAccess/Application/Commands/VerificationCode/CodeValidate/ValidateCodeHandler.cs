using CXChange.BuildingBlocks.Application.Exceptions;
using CXChange.BuildingBlocks.Application.Totp;
using CXChange.Modules.UserAccess.Application.Enums;
using CXChange.Modules.UserAccess.Application.Services.Interfaces;
using CXChange.Modules.UserAccess.Application.Utils;
using CXChange.Modules.UserAccess.Domain.Interfaces.Repositories;
using MassTransit;

namespace CXChange.Modules.UserAccess.Application.Commands.VerificationCode.CodeValidate
{
    internal class ValidateCodeHandler : IConsumer<ValidateCode>
    {
        IUserRepository _repo;
        IVerificationCodeFunctions _validator;
        IAuthenticator _authenticator;
        public ValidateCodeHandler(IUserRepository repo, IVerificationCodeFunctions validator, IAuthenticator authenticator)
        {
            _repo = repo;
            _validator = validator;
            _authenticator = authenticator;
        }

        public async Task Consume(ConsumeContext<ValidateCode> context)
        {
            var user = await _repo.GetUserById(context.Message.UserId);
            if (user == null)
            {
                throw new ExchangeCommandException("user does not exist", CommandExceptionEnum.UserDoesNotExist);
            }
            bool result = false;
            if (CodeType.GetCodeTypeByName(context.Message.CodeType).Provider == CodeProvider.Default)
            {
                result = _validator.Verify(user, int.Parse(context.Message.Code), string.Concat(CodeType.GetCodeTypeByName(context.Message.CodeType).Name, context.Message.UserId));
            }
            else
            {
                var key = await _repo.GetAuthenticatorKey(user);
                result = _authenticator.ValidateTwoFactorPIN(key, context.Message.Code, TimeSpan.FromSeconds(30));
            }

            if (!result)
            {
                throw new ExchangeCommandException("code is not currect", CommandExceptionEnum.VerificationCodeInvalid);
            }
            var token = await _repo.GenerateUserToken(user, KeyWords.CustomTokenProvider, string.Concat(CodeType.GetCodeTypeByName(context.Message.CodeType).Name, context.Message.UserId));
            await context.RespondAsync(new CodeValidated { Token = token });
        }
    }
}
