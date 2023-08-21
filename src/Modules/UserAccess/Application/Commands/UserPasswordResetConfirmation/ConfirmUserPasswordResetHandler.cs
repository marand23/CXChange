using CXChange.BuildingBlocks.Application.Exceptions;
using CXChange.BuildingBlocks.Application.Totp;
using CXChange.BuildingBlocks.Domain;
using CXChange.Modules.UserAccess.Application.Enums;
using CXChange.Modules.UserAccess.Domain.Interfaces.Repositories;
using MassTransit;

namespace CXChange.Modules.UserAccess.Application.Commands.UserPasswordResetConfirmation
{
    internal class ConfirmUserPasswordResetHandler : IConsumer<ConfirmUserPasswordReset>
    {
        IUserRepository _repo;
        IDomainEventDispatcher _dispatcher;
        ITotp _totp;
        public ConfirmUserPasswordResetHandler(IUserRepository repo, IDomainEventDispatcher dispatcher, ITotp totp)
        {
            _repo = repo;
            _dispatcher = dispatcher;
            _totp = totp;
        }

        public async Task Consume(ConsumeContext<ConfirmUserPasswordReset> context)
        {
            var user = await _repo.GetUserByPhoneNumber(context.Message.PhoneNumber);
            if (user == null)
            {
                throw new ExchangeCommandException("user does not exist", (int)CommandExceptionEnum.UserDoesNotExist);
            }
            var codeResult = _totp.ValidateCode(user.GetSnapShot().SecurityStamp, int.Parse(context.Message.VerificationCode), TimeSpan.FromMinutes(3), KeyWords.ResetPasswordTotpModifier(context.Message.PhoneNumber));
            if (!codeResult)
            {
                throw new ExchangeCommandException("validation code invalid", (int)CommandExceptionEnum.VerificationCodeInvalid);
            }
            var result = _repo.ResetPassword(user, context.Message.Token, context.Message.NewPassword);

            await context.RespondAsync(new UserPasswordResetConfirmed { });
        }
    }
}
