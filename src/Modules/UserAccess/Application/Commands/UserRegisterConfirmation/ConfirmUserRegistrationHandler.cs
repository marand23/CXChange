using CXChange.BuildingBlocks.Application.Exceptions;
using CXChange.BuildingBlocks.Application.Totp;
using CXChange.Modules.UserAccess.Application.Enums;
using CXChange.Modules.UserAccess.Domain.Interfaces.Repositories;
using CXChange.Modules.UserAccess.Domain.User;
using MassTransit;

namespace CXChange.Modules.UserAccess.Application.Commands.UserRegisterConfirmation
{
    internal class ConfirmUserRegistrationHandler : IConsumer<ConfirmUser>
    {
        IUserRepository _userRepository;
        ITotp _totp;
        public ConfirmUserRegistrationHandler(IUserRepository userRepository, ITotp totp)
        {
            _userRepository = userRepository;
            _totp = totp;
        }
        public async Task Consume(ConsumeContext<ConfirmUser> context)
        {
            User user = await _userRepository.GetUserByPhoneNumber(context.Message.PhoneNumber);
            if (user == null)
            {
                throw new ExchangeCommandException("user does not exist", (int)CommandExceptionEnum.UserDoesNotExist);
            }
            user.ConfirmPhoneNumber();
            var verificationResult = _totp.ValidateCode(user.GetSnapShot().SecurityStamp, int.Parse(context.Message.VerificationCode), TimeSpan.FromMinutes(3), KeyWords.ConfirmRegisterTotpModifier(context.Message.PhoneNumber));
            if (!verificationResult)
            {
                throw new ExchangeCommandException("verification code is invalid", (int)CommandExceptionEnum.VerificationCodeInvalid);


            }

            await _userRepository.ConfirmPhoneNumber(context.Message.PhoneNumber);

            await context.RespondAsync(new UserConfirmed
            {
                Token = "this is the token"
            });


        }
    }
}
