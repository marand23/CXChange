using CXChange.Modules.UserAccess.Domain.Interfaces.Repositories;
using MassTransit;

namespace CXChange.Modules.UserAccess.Application.Commands.UserLogin
{
    internal class UserLoginCheckHandler : IConsumer<CheckUserLogin>
    {
        IUserRepository _userRepository;
        public UserLoginCheckHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Consume(ConsumeContext<CheckUserLogin> context)
        {
            var user = await _userRepository.GetUserByPhoneNumber(context.Message.PhoneNumber);

            await _userRepository.CheckSignIn(context.Message.PhoneNumber, context.Message.Password);
            string token = await _userRepository.GenerateUserToken(user);
            await context.RespondAsync(new UserLoginChecked { Token = token, PhoneNumber = context.Message.PhoneNumber });


        }
    }
}
