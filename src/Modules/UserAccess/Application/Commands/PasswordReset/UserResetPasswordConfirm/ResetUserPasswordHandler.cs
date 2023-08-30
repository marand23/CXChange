using CXChange.BuildingBlocks.Application.Exceptions;
using CXChange.BuildingBlocks.Domain;
using CXChange.Modules.UserAccess.Application.Commands.PasswordReset.UserResetPasswordConfirm;
using CXChange.Modules.UserAccess.Application.Enums;
using CXChange.Modules.UserAccess.Application.Utils;
using CXChange.Modules.UserAccess.Domain.Interfaces.Repositories;
using MassTransit;

namespace CXChange.Modules.UserAccess.Application.Commands.PasswordReset.UserResetPasswordCofirm
{
    internal class ResetUserPasswordHandler : IConsumer<ResetUserPasswordConfirm>
    {
        IUserRepository _repo;
        IDomainEventDispatcher _dispatcher;
        public ResetUserPasswordHandler(IUserRepository repo, IDomainEventDispatcher dispatcher)
        {
            _repo = repo;
            _dispatcher = dispatcher;
        }
        public async Task Consume(ConsumeContext<ResetUserPasswordConfirm> context)
        {
            var user = await _repo.GetUserByPhoneNumber(context.Message.PhoneNumber);
            if (user == null)
            {
                throw new ExchangeCommandException("user does not exist", CommandExceptionEnum.UserDoesNotExist);
            }
            var tokenResult = await _repo.ValidateUserToken(user, context.Message.Token, KeyWords.CustomTokenProvider, string.Concat(KeyWords.ResetPassword, context.Message.PhoneNumber));
            if (!tokenResult)
            {
                throw new ExchangeCommandException("enter the code first", CommandExceptionEnum.EnterCodeFirst);
            }
            //update security stamp
            await _repo.ResetPassword(user, context.Message.Token, context.Message.NewPassword);
            await _dispatcher.Dispatch(user.DomainEvents);
            await context.RespondAsync(new UserPasswordReset { });
        }
    }
}
