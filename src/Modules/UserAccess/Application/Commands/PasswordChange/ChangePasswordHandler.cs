using CXChange.BuildingBlocks.Application.Exceptions;
using CXChange.Modules.UserAccess.Application.Enums;
using CXChange.Modules.UserAccess.Application.Utils;
using CXChange.Modules.UserAccess.Domain.Interfaces.Repositories;
using MassTransit;

namespace CXChange.Modules.UserAccess.Application.Commands.PasswordChange
{
    internal class ChangePasswordHandler : IConsumer<ChangePassword>
    {
        IUserRepository _repo;
        public ChangePasswordHandler(IUserRepository repo)
        {
            _repo = repo;
        }
        public async Task Consume(ConsumeContext<ChangePassword> context)
        {

            var user = await _repo.GetUserById(context.Message.UserId);
            if (user == null)
            {
                throw new ExchangeCommandException("user does not exist", CommandExceptionEnum.UserDoesNotExist);
            }
            var tokenreuslt = await _repo.ValidateUserToken(user, context.Message.Token, KeyWords.CustomTokenProvider, string.Concat(KeyWords.ResetPassword, context.Message.UserId));
            if (!tokenreuslt)
            {
                throw new ExchangeCommandException("enter code first", CommandExceptionEnum.EnterCodeFirst);
            }
            bool result = await _repo.CheckPassword(user, context.Message.OldPassword);
            if (!result)
            {
                throw new ExchangeCommandException("old pass not currect", CommandExceptionEnum.PasswordInvalid);
            }
            await _repo.ChangePassword(user, context.Message.OldPassword, context.Message.NewPassword);
            await context.RespondAsync(new PasswordChanged
            {

            });

        }
    }
}
