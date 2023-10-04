using CXChange.BuildingBlocks.Application.Totp;
using CXChange.Modules.UserAccess.Application.Enums;
using CXChange.Modules.UserAccess.Application.Exceptions;
using CXChange.Modules.UserAccess.Domain.Interfaces.Repositories;
using MassTransit;

namespace CXChange.Modules.UserAccess.Application.Commands.Authenticator.AuthenticatorInitiate
{
    internal class InitiateAuthenticatorHandler : IConsumer<InitiateAuthenticator>
    {
        IUserRepository _repo;
        IAuthenticator _authenticator;
        public InitiateAuthenticatorHandler(IUserRepository repo, IAuthenticator authenticator)
        {
            _repo = repo;
            _authenticator = authenticator;

        }
        public async Task Consume(ConsumeContext<InitiateAuthenticator> context)
        {
            var user = await _repo.GetUserById(context.Message.UserId);
            if (user == null)
            {
                throw new UserAccessCommandException(CommandExceptionEnum.UserNotFound);

            }
            await _repo.ResetAuthenticatorKey(user);
            var key = await _repo.GetAuthenticatorKey(user);
            var url = _authenticator.GenerateAuthenticatorUrl(key, "atrobit", user.GetSnapShot().PhoneNumber);
            await context.RespondAsync(new AuthenticatorInitiated { Url = url });

        }
    }
}
