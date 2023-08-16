using CXChange.BuildingBlocks.Application;
using CXChange.BuildingBlocks.Application.Totp;
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
                await context.RespondAsync(Result<UserConfirmed>.CreateFailure("user did not confirm ", ResultStatus.CommandNotValid, new string[] { "code is not currect" }));
            }
            user.ConfirmPhoneNumber();
            var verificationResult = _totp.ValidateCode(user.GetSnapShot().SecurityStamp, int.Parse(context.Message.VerificationCode), TimeSpan.FromMinutes(3), "user-registered");
            if (!verificationResult)
            {
                await context.RespondAsync(Result<UserConfirmed>.CreateFailure("user did not confirm ", ResultStatus.CommandNotValid, new string[] { "code is not currect" }));

            }

            await _userRepository.ConfirmPhoneNumber(context.Message.PhoneNumber);
            await context.RespondAsync(new UserConfirmed
            {
                Token = "this is the token"
            });


        }
    }
}
