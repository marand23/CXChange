using CXChange.BuildingBlocks.Domain;
using CXChange.Modules.UserAccess.Application.Enums;
using CXChange.Modules.UserAccess.Application.Exceptions;
using CXChange.Modules.UserAccess.Application.Services.Interfaces;
using CXChange.Modules.UserAccess.Application.Utils;
using CXChange.Modules.UserAccess.Domain.Interfaces.Repositories;
using MassTransit;

namespace CXChange.Modules.UserAccess.Application.Commands.EmailChange
{
    internal class ChangeEmailHandler : IConsumer<ChangeEmail>
    {
        IUserRepository _repo;
        IVerificationCodeFunctions _verifier;
        IDomainEventDispatcher _dispatcher;
        public ChangeEmailHandler(IUserRepository repo, IVerificationCodeFunctions verifier, IDomainEventDispatcher dispatcher)
        {
            _repo = repo;
            _verifier = verifier;
            _dispatcher = dispatcher;
        }
        public async Task Consume(ConsumeContext<ChangeEmail> context)
        {
            var user = await _repo.GetUserById(context.Message.UserId);
            if (user == null)
            {
                throw new UserAccessCommandException(CommandExceptionEnum.UserNotFound);
            }
            var snapshot = user.GetSnapShot();
            //verify old email 
            var tokenResult = await _repo.ValidateUserToken(user, context.Message.Token, KeyWords.CustomTokenProvider, CodeType.GeneratePurposeForSpecificTwoFactor(snapshot.Id, KeyWords.VerifyOldEmailOnEmailChange, snapshot.Email));
            if (!tokenResult)
            {
                throw new UserAccessCommandException(CommandExceptionEnum.EnterCodeFirst);
            }
            //verify code send to new email
            var codeResult = _verifier.Verify(user, int.Parse(context.Message.Code), CodeType.GeneratePurposeForSpecificTwoFactor(snapshot.Id, KeyWords.VerifyNewEmailOnEmailChange, context.Message.Email));
            if (!codeResult)
            {
                throw new UserAccessCommandException(CommandExceptionEnum.VerificationCodeInvalid);
            }
            user.ChangeEmail(context.Message.Email);
            user.UpdateSecurityStamp();
            await _repo.Update(user);
            await _dispatcher.Dispatch(user.DomainEvents);
            await context.RespondAsync(new EmailChanged { });
        }
    }
}
