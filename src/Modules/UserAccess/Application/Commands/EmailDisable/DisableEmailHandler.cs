using CXChange.BuildingBlocks.Domain;
using CXChange.Modules.UserAccess.Application.Commands.AuthenticatorDeactivate;
using CXChange.Modules.UserAccess.Application.Commands.EmailDisable;
using CXChange.Modules.UserAccess.Application.Exceptions;
using CXChange.Modules.UserAccess.Application.Utils;
using CXChange.Modules.UserAccess.Domain.Interfaces.Repositories;
using MassTransit;

namespace CXChange.Modules.UserAccess.Application.Commands.EmailDeActivate
{
    internal class DisableEmailHandler : IConsumer<DisableEmail>
    {
        IUserRepository _repo;
        IDomainEventDispatcher _dispatcher;
        public DisableEmailHandler(IUserRepository repo, IDomainEventDispatcher dispatcher)
        {
            _repo = repo;
            _dispatcher = dispatcher;
        }
        public async Task Consume(ConsumeContext<DisableEmail> context)
        {
            var user = await _repo.GetUserById(context.Message.UserId);
            if (user == null)
            {
                throw new UserAccessCommandException(Enums.CommandExceptionEnum.UserNotFound);
            }
            var tokenResult = await _repo.ValidateUserToken(user, context.Message.Token, KeyWords.CustomTokenProvider, CodeType.GeneratePurposeForDefaultTwoFactor(user.GetSnapShot(), KeyWords.DisableEmail));
            if (!tokenResult)
            {
                throw new UserAccessCommandException(Enums.CommandExceptionEnum.EnterCodeFirst);
            }
            user.DisableEmail();
            /*await*/
            await _dispatcher.Dispatch(user.DomainEvents);
            await _repo.Update(user);
            await context.RespondAsync(new AuthenticatorDeactivated { });

        }
    }
}
