using CXChange.BuildingBlocks.Application.Totp;
using CXChange.Modules.UserAccess.Application.ApplicationEvents;
using CXChange.Modules.UserAccess.Application.Commands.VerificationCode.CodeValidate;
using CXChange.Modules.UserAccess.Application.Enums;
using CXChange.Modules.UserAccess.Application.Exceptions;
using CXChange.Modules.UserAccess.Application.Services.Interfaces;
using CXChange.Modules.UserAccess.Application.Utils;
using CXChange.Modules.UserAccess.Domain.Interfaces.Repositories;
using MassTransit;
using MassTransit.Mediator;

namespace CXChange.Modules.UserAccess.Application.Commands.VerificationCode.PhoneNumberCodeValidate
{
    internal class ValidatePhoneNumberCodeHandler : IConsumer<ValidatePhoneNumberCode>
    {
        IUserRepository _repo;
        IVerificationCodeFunctions _validator;
        IScopedMediator _mediator;

        public ValidatePhoneNumberCodeHandler(IUserRepository repo, IVerificationCodeFunctions validator, IAuthenticator authenticator, IScopedMediator mediator)
        {
            _repo = repo;
            _validator = validator;
            _mediator = mediator;

        }

        public async Task Consume(ConsumeContext<ValidatePhoneNumberCode> context)
        {
            var user = await _repo.GetUserById(context.Message.UserId);
            if (user == null)
            {
                throw new UserAccessCommandException(CommandExceptionEnum.UserNotFound);
            }

            if (user.GetSnapShot().PhoneNumberConfirmed)
            {
                throw new UserAccessCommandException(CommandExceptionEnum.UserNotFound);
            }
            var snapshot = user.GetSnapShot();
            var tokenResult = _validator.Verify(user, int.Parse(context.Message.Code), CodeType.GeneratePurposeForSpecificTwoFactor(snapshot.Id, context.Message.CodeType, context.Message.PhoneNumber));
            if (!tokenResult)
            {
                throw new UserAccessCommandException(CommandExceptionEnum.VerificationCodeInvalid);
            }
            user.UpdateSecurityStamp();
            var token = await _repo.GenerateUserToken(user, KeyWords.CustomTokenProvider, CodeType.GeneratePurposeForSpecificTwoFactor(snapshot.Id, context.Message.CodeType, snapshot.Email));

            await _repo.Update(user);
            await _mediator.Publish(new TotpCodeVerifiedEvent { CodeType = CodeType.GetCodeTypeByName(context.Message.CodeType), UserId = user.Id, Info = context.Message.PhoneNumber });
            await context.RespondAsync(new PhoneNumberCodeValidated { Token = token });
        }
    }
}
