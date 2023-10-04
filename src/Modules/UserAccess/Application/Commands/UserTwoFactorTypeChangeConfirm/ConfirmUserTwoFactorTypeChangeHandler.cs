using CXChange.BuildingBlocks.Domain;
using CXChange.Modules.UserAccess.Application.Enums;
using CXChange.Modules.UserAccess.Application.Exceptions;
using CXChange.Modules.UserAccess.Application.Utils;
using CXChange.Modules.UserAccess.Domain.Interfaces.Repositories;
using MassTransit;

namespace CXChange.Modules.UserAccess.Application.Commands.UserTwoFactorTypeChangeConfirm
{
    internal class ConfirmUserTwoFactorTypeChangeHandler : IConsumer<ConfirmUserTwoFactorTypeChange>
    {
        IUserRepository _repo;
        IDomainEventDispatcher _dispatcher;
        public ConfirmUserTwoFactorTypeChangeHandler(IUserRepository repo, IDomainEventDispatcher dispatcher)
        {
            _repo = repo;
            _dispatcher = dispatcher;
        }

        public async Task Consume(ConsumeContext<ConfirmUserTwoFactorTypeChange> context)
        {
            var user = await _repo.GetUserById(context.Message.UserId);
            if (user == null)
            {
                throw new UserAccessCommandException(CommandExceptionEnum.UserNotFound);
            }
            var tokenResult = await _repo.ValidateUserToken(user, context.Message.Token, KeyWords.CustomTokenProvider, CodeType.GeneratePurposeForDefaultTwoFactor(user.GetSnapShot(), KeyWords.ChangeTwoFactorType));
            if (!tokenResult)
            {
                throw new UserAccessCommandException(CommandExceptionEnum.EnterCodeFirst);
            }

            //  user.ChangeTwoFactorType((TwoFactoEnum)context.Message.NewTwoFactorType);
            await _repo.Update(user);
            /* await */
            await _dispatcher.Dispatch(user.DomainEvents);
            await context.RespondAsync(new UserTwoFactorTypeChanged { });

        }
    }
}
