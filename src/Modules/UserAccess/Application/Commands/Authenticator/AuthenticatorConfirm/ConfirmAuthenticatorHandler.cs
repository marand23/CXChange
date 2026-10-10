using CXChange.BuildingBlocks.Domain;
using CXChange.Modules.UserAccess.Application.Enums;
using CXChange.Modules.UserAccess.Application.Exceptions;
using CXChange.Modules.UserAccess.Application.Utils;
using CXChange.Modules.UserAccess.Domain.Interfaces.Repositories;
using CXChange.Modules.UserAccess.Domain.User;
using MassTransit;

namespace CXChange.Modules.UserAccess.Application.Commands.Authenticator.AuthenticatorConfirm
{
    internal class ConfirmAuthenticatorHandler : IConsumer<ConfirmAuthenticator>
    {
        public IUserRepository _repo;
        public IDomainEventDispatcher _dispatcher;
        public ConfirmAuthenticatorHandler(IUserRepository repo, IDomainEventDispatcher dispatcher)
        {
            _repo = repo;
            _dispatcher = dispatcher;
        }

        public async Task Consume(ConsumeContext<ConfirmAuthenticator> context)
        {
            var user = await _repo.GetUserById(context.Message.UserId);
            if (user == null)
            {
                throw new UserAccessCommandException(CommandExceptionEnum.UserNotFound);

            }
            var snapshot = user.GetSnapShot();
            var authTokenResult = await _repo.ValidateUserToken(user, context.Message.AuthToken, KeyWords.CustomTokenProvider, CodeType.GeneratePurposeForSpecificTwoFactor(snapshot.Id, KeyWords.ConfirmAuthenticatorActivation, TwoFactorEnum.AUTHENTICATOR.ToString()));
            if (!authTokenResult)
            {
                throw new UserAccessCommandException(CommandExceptionEnum.EnterCodeFirst);
            }
            var codeTokenResult = await _repo.ValidateUserToken(user, context.Message.CodeToken, KeyWords.CustomTokenProvider, CodeType.GeneratePurposeForDefaultTwoFactor(user.GetSnapShot(), KeyWords.ConfirmAuthenticatorActivation));
            if (!codeTokenResult)
            {
                throw new UserAccessCommandException(CommandExceptionEnum.EnterCodeFirst);
            }
            user.UpdateSecurityStamp();
            user.ActivateAuthenticator();

            await _dispatcher.Dispatch(user.DomainEvents);
            await _repo.Update(user);

            await context.RespondAsync(new AuthenticatorConfirmed { });
        }
    }
}
