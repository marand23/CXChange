using CXChange.BuildingBlocks.Application.Totp;
using CXChange.Modules.UserAccess.Application.Commands.VerificationCode.CodeValidate;
using CXChange.Modules.UserAccess.Application.Enums;
using CXChange.Modules.UserAccess.Application.Exceptions;
using CXChange.Modules.UserAccess.Application.Services.Interfaces;
using CXChange.Modules.UserAccess.Application.Utils;
using CXChange.Modules.UserAccess.Domain.Interfaces.Repositories;
using MassTransit;

namespace CXChange.Modules.UserAccess.Application.Commands.VerificationCode.PhoneNumberCodeValidate
{
    internal class ValidatePhoneNumberCodeHandler : IConsumer<ValidatePhoneNumberCode>
    {
        IUserRepository _repo;
        IVerificationCodeFunctions _validator;

        public ValidatePhoneNumberCodeHandler(IUserRepository repo, IVerificationCodeFunctions validator, IAuthenticator authenticator)
        {
            _repo = repo;
            _validator = validator;

        }

        public async Task Consume(ConsumeContext<ValidatePhoneNumberCode> context)
        {
            var user = await _repo.GetUserByPhoneNumber(context.Message.UserId);
            if (user == null)
            {
                throw new UserAccessCommandException(CommandExceptionEnum.UserNotFound);
            }

            if (user.GetSnapShot().PhoneNumberConfirmed)
            {
                throw new UserAccessCommandException(CommandExceptionEnum.UserNotFound);
            }
            var snapshot = user.GetSnapShot();
            var tokenResult = _validator.Verify(user, int.Parse(context.Message.Code), CodeType.GeneratePurposeForSpecificTwoFactor(snapshot.Id, context.Message.CodeType, snapshot.Email));
            if (!tokenResult)
            {
                throw new UserAccessCommandException(CommandExceptionEnum.VerificationCodeInvalid);
            }
            user.UpdateSecurityStamp();
            var token = await _repo.GenerateUserToken(user, KeyWords.CustomTokenProvider, CodeType.GeneratePurposeForSpecificTwoFactor(snapshot.Id, context.Message.CodeType, snapshot.Email));

            await _repo.Update(user);
            await context.RespondAsync(new PhoneNumberCodeValidated { Token = token });
        }
    }
}
