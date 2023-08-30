using CXChange.BuildingBlocks.Application.Exceptions;
using CXChange.Modules.UserAccess.Application.Enums;
using CXChange.Modules.UserAccess.Application.Utils;
using CXChange.Modules.UserAccess.Domain.Interfaces.Repositories;
using MassTransit;

namespace CXChange.Modules.UserAccess.Application.Commands.Authenticator.AuthenticatorConfirm
{
    internal class ConfirmAuthenticatorHandler : IConsumer<ConfirmAuthenticator>
    {
        public IUserRepository _repo;
        public ConfirmAuthenticatorHandler(IUserRepository repo)
        {
            _repo = repo;
        }

        public async Task Consume(ConsumeContext<ConfirmAuthenticator> context)
        {
            var user = await _repo.GetUserById(context.Message.UserId);
            if (user == null)
            {
                throw new ExchangeCommandException("user does not exist", CommandExceptionEnum.UserDoesNotExist);

            }
            var authTokenResult = await _repo.ValidateUserToken(user, context.Message.AuthToken, KeyWords.CustomTokenProvider, string.Concat(KeyWords.ConfirmAuthenticator, context.Message.UserId));
            if (!authTokenResult)
            {
                throw new ExchangeCommandException("validate authenticator code", CommandExceptionEnum.ValidateAuthenticatorCode);
            }
            var codeTokenResult = await _repo.ValidateUserToken(user, context.Message.CodeToken, KeyWords.CustomTokenProvider, string.Concat(KeyWords.ConfirmDefault2FaForAuthenticator, context.Message.UserId));
            if (!codeTokenResult)
            {
                throw new ExchangeCommandException("validate authenticator code", CommandExceptionEnum.ValidateVerificationCode);
            }
            //update security stamp
            await context.RespondAsync(new AuthenticatorConfirmed { });
        }
    }
}
