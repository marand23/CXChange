using CXChange.BuildingBlocks.Application.Totp;
using CXChange.Modules.UserAccess.Domain.Interfaces.Repositories;
using MassTransit;
using Microsoft.AspNetCore.Http;

namespace CXChange.Modules.UserAccess.Application.Commands.ConfirmLogin
{
    internal class UserLoginConfirmHandler : IConsumer<ConfirmUserLogin>
    {
        IHttpContextAccessor _httpContextAccessor;

        IUserRepository _userRepository;
        ITotp _totp;
        public UserLoginConfirmHandler(IUserRepository userRepository, IHttpContextAccessor httpContextAccessor, ITotp totp)
        {
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
            _totp = totp;
        }
        public async Task Consume(ConsumeContext<ConfirmUserLogin> context)
        {
            var user = await _userRepository.GetUserByPhoneNumber(context.Message.PhoneNumber);
            var tokenResult = await _userRepository.ValidateUserToken(user, context.Message.Token);
            if (!tokenResult)
            {
                throw new CommandException("plase login first");

            }
            var codeResult = _totp.ValidateCode(user.GetSnapShot().SecurityStamp, int.Parse(context.Message.VerificationCode), TimeSpan.FromMinutes(3), "login");
            if (!codeResult)
            {
                throw new CommandException("code is not currect");

            }
            user.ConfirmUserLogin();
            await _userRepository.SignIn(user);
            var token = (string)_httpContextAccessor.HttpContext.Items["token"];
            await context.RespondAsync(new UserLoginConfirmed { Token = token });
        }
    }
}
