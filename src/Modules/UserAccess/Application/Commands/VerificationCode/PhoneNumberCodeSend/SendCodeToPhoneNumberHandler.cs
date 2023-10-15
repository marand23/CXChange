using CXChange.Modules.UserAccess.Application.Enums;
using CXChange.Modules.UserAccess.Application.Exceptions;
using CXChange.Modules.UserAccess.Application.Services.Interfaces;
using CXChange.Modules.UserAccess.Application.Utils;
using CXChange.Modules.UserAccess.Domain.Interfaces.Repositories;
using CXChange.Modules.UserAccess.Domain.User;
using MassTransit;

namespace CXChange.Modules.UserAccess.Application.Commands.VerificationCode.PhoneNumberCodeSend
{
    internal class SendCodeToPhoneNumberHandler : IConsumer<SendCodeToPhoneNumber>
    {
        IUserRepository _repo;
        IVerificationCodeFunctions _sender;
        public SendCodeToPhoneNumberHandler(IUserRepository repo, IVerificationCodeFunctions sender)
        {
            _repo = repo;
            _sender = sender;
        }
        public async Task Consume(ConsumeContext<SendCodeToPhoneNumber> context)
        {
            var codeType = CodeType.GetCodeTypeByName(context.Message.CodeType);
            User user = null;
            if (codeType.RequiresAuthentication)
            {
                user = await _repo.GetUserById(context.Message.UserId);
            }
            else
            {
                user = await _repo.GetUserByPhoneNumber(context.Message.PhoneNumber);
            }
            if (user == null)
            {
                throw new UserAccessCommandException(CommandExceptionEnum.UserNotFound);
            }
            if (codeType.Name == KeyWords.ConfirmRegister)
            {
                user.CheckRegisterRequirements();
            }
            var snapshot = user.GetSnapShot();
            if (snapshot.PhoneNumber != context.Message.PhoneNumber && codeType.RequiresEmailOrPhoneNumberMatching)
            {
                throw new UserAccessCommandException(CommandExceptionEnum.PhoneNumberDoesNotMatch);
            }
            if (!snapshot.PhoneNumberConfirmed && codeType.RequiresEmailOrPhoneNumberConfirmation)
            {
                throw new UserAccessCommandException(CommandExceptionEnum.PhoneNumberNotConfirmed);
            }
            if (snapshot.PhoneNumberConfirmed && !codeType.RequiresEmailOrPhoneNumberConfirmation)
            {
                throw new UserAccessCommandException(CommandExceptionEnum.PhoneNumberAlreadyConfirmed);
            }
            if (codeType.RequiresTokenVerification)
            {
                var tokenRes = await _repo.ValidateUserToken(user, context.Message.Token, KeyWords.CustomTokenProvider, context.Message.CodeType);
                if (!tokenRes)
                {
                    throw new UserAccessCommandException(CommandExceptionEnum.EnterCodeFirst);
                }
            }

            await _sender.Send(user, CodeType.GeneratePurposeForSpecificTwoFactor(snapshot.Id, context.Message.CodeType, context.Message.PhoneNumber));
            await context.RespondAsync(new CodeToPhoneNumberSent { });
        }
    }
}
