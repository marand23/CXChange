using CXChange.Modules.UserAccess.Application.Enums;
using CXChange.Modules.UserAccess.Application.Exceptions;
using CXChange.Modules.UserAccess.Application.Services.Interfaces;
using CXChange.Modules.UserAccess.Application.Utils;
using CXChange.Modules.UserAccess.Domain.Interfaces.Repositories;
using CXChange.Modules.UserAccess.Domain.User;
using MassTransit;

namespace CXChange.Modules.UserAccess.Application.Commands.VerificationCode.EmailCodeSend
{
    internal class SendCodeToEmailHandler : IConsumer<SendCodeToEmail>
    {
        IUserRepository _repo;
        IVerificationCodeFunctions _sender;
        public SendCodeToEmailHandler(IUserRepository repo, IVerificationCodeFunctions sender)
        {
            _repo = repo;
            _sender = sender;
        }

        public async Task Consume(ConsumeContext<SendCodeToEmail> context)
        {
            User user = null;
            var codeType = CodeType.GetCodeTypeByName(context.Message.CodeType);
            //if this code type requires authentication get user by id
            if (codeType.RequiresAuthentication)
            {
                user = await _repo.GetUserById(context.Message.UserId);
            }
            //else get user by email as it does not require authentication
            else
            {
                user = await _repo.GetUserByEmail(context.Message.Email);
            }
            if (user == null)
            {
                throw new UserAccessCommandException(CommandExceptionEnum.UserNotFound);
            }
            var snapshot = user.GetSnapShot();
            //if codetype is register check the requirement
            //like phonenumber should not confirmed
            if (codeType.Name == KeyWords.ConfirmRegister)
            {
                user.CheckRegisterRequirements();
            }

            //request email should be equal to useremail
            //unless the codetype does not need mathching 
            if (snapshot.Email != context.Message.Email && codeType.RequiresEmailOrPhoneNumberMatching)
            {
                throw new UserAccessCommandException(CommandExceptionEnum.EmaiDoesNotMatch);
            }
            //email should be in unconfirmed 
            //state only when this is required in codetype
            if (!snapshot.EmailConfirmed && codeType.RequiresEmailOrPhoneNumberConfirmation)
            {
                throw new UserAccessCommandException(CommandExceptionEnum.EmailNotConfirmed);
            }
            //email should be in unconfirmed 
            //state when user sets email
            if (snapshot.EmailConfirmed && !codeType.RequiresEmailOrPhoneNumberConfirmation)
            {
                throw new UserAccessCommandException(CommandExceptionEnum.EmailAlreadyConfirmed);
            }
            if (codeType.RequiresTokenVerification)
            {
                var tokenRes = await _repo.ValidateUserToken(user, context.Message.Token, KeyWords.CustomTokenProvider, context.Message.CodeType);
                if (!tokenRes)
                {
                    throw new UserAccessCommandException(CommandExceptionEnum.EnterCodeFirst);
                }
            }

            await _sender.Send(user, CodeType.GeneratePurposeForSpecificTwoFactor(user.GetSnapShot().Id, context.Message.CodeType, context.Message.Email));

            await context.RespondAsync(new CodeToEmailSent { });

        }

    }
}
