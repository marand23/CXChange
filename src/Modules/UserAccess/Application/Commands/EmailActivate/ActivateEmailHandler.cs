using CXChange.BuildingBlocks.Domain;
using CXChange.Modules.UserAccess.Application.Enums;
using CXChange.Modules.UserAccess.Application.Exceptions;
using CXChange.Modules.UserAccess.Application.Services.Interfaces;
using CXChange.Modules.UserAccess.Application.Utils;
using CXChange.Modules.UserAccess.Domain.Interfaces.Repositories;
using MassTransit;

namespace CXChange.Modules.UserAccess.Application.Commands.EmailActivate
{
    internal class ActivateEmailHandler : IConsumer<ActivateEmail>
    {
        IUserRepository _repo;
        IDomainEventDispatcher _dispatcher;
        IVerificationCodeFunctions _verifier;
        public ActivateEmailHandler(IUserRepository repo, IDomainEventDispatcher dispatcher, IVerificationCodeFunctions verifier)
        {
            _repo = repo;
            _dispatcher = dispatcher;
            _verifier = verifier;
        }
        public async Task Consume(ConsumeContext<ActivateEmail> context)
        {
            var user = await _repo.GetUserById(context.Message.UserId);
            if (user == null)
            {
                throw new UserAccessCommandException(CommandExceptionEnum.UserNotFound);

            }
            //checks if user have passed default two factor step
            var tokenResult = await _repo.ValidateUserToken(user, context.Message.Token, KeyWords.CustomTokenProvider, CodeType.GeneratePurposeForDefaultTwoFactor(user.GetSnapShot(), KeyWords.VerifyEmailOnEmailSet));
            if (tokenResult)
            {
                throw new UserAccessCommandException(CommandExceptionEnum.EnterCodeFirst);
            }
            //checks the code sent to email
            var codeResutl = _verifier.Verify(user, int.Parse(context.Message.Code), CodeType.GeneratePurposeForSpecificTwoFactor(user.GetSnapShot().Id, KeyWords.VerifyEmailOnEmailSet, context.Message.Email));
            if (!codeResutl)
            {
                throw new UserAccessCommandException(CommandExceptionEnum.VerificationCodeInvalid);
            }
            user.ActivateEmail(context.Message.Email);
            user.UpdateSecurityStamp();
            await _repo.Update(user);
            await _dispatcher.Dispatch(user.DomainEvents);
            await context.RespondAsync(new EmailActivated { });

        }
    }
}
