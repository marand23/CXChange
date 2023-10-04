using CXChange.Modules.UserAccess.Application.Enums;
using CXChange.Modules.UserAccess.Application.Exceptions;
using CXChange.Modules.UserAccess.Application.Services.Interfaces;
using CXChange.Modules.UserAccess.Application.Utils;
using CXChange.Modules.UserAccess.Domain.Interfaces.Repositories;
using MassTransit;

namespace CXChange.Modules.UserAccess.Application.Commands.VerificationCode.EmailCodeValidate
{
    internal class ValidateEmailCodeHandler : IConsumer<ValidateEmailCode>
    {
        IUserRepository _repo;
        IVerificationCodeFunctions _validator;
        public ValidateEmailCodeHandler(IUserRepository repo, IVerificationCodeFunctions validator)
        {
            _repo = repo;
            _validator = validator;
        }
        public async Task Consume(ConsumeContext<ValidateEmailCode> context)
        {
            var user = await _repo.GetUserByEmail(context.Message.UserId);
            if (user == null)
            {
                throw new UserAccessCommandException(CommandExceptionEnum.UserNotFound);
            }

            if (!user.GetSnapShot().EmailConfirmed)
            {
                throw new UserAccessCommandException(CommandExceptionEnum.UserNotFound);
            }
            var snapshot = user.GetSnapShot();
            var result = _validator.Verify(user, int.Parse(context.Message.Code), CodeType.GeneratePurposeForSpecificTwoFactor(snapshot.Id, context.Message.CodeType, snapshot.Email));
            if (!result)
            {
                throw new UserAccessCommandException(CommandExceptionEnum.VerificationCodeInvalid);
            }


            var token = await _repo.GenerateUserToken(user, KeyWords.CustomTokenProvider, CodeType.GeneratePurposeForSpecificTwoFactor(snapshot.Id, context.Message.CodeType, snapshot.Email));


            await context.RespondAsync(new EmailCodeValidated { Token = token });
        }

    }
}
