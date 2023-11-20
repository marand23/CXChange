using CXChange.BuildingBlocks.Domain;
using CXChange.Modules.UserAccess.Application.Enums;
using CXChange.Modules.UserAccess.Application.Exceptions;
using CXChange.Modules.UserAccess.Application.Utils;
using CXChange.Modules.UserAccess.Domain.Interfaces.Repositories;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace CXChange.Modules.UserAccess.Application.Commands.Login.UserLogin
{
    internal class UserLoginCheckHandler : IConsumer<CheckUserLogin>
    {
        IUserRepository _userRepository;
        IUnitOfWork _unitOfWork;

        public UserLoginCheckHandler(IUserRepository userRepository, [FromKeyedServices("user_access")] IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Consume(ConsumeContext<CheckUserLogin> context)
        {
            try
            {
                var user = await _userRepository.GetUserByPhoneNumber(context.Message.PhoneNumber);
                if (user == null)
                {
                    throw new UserAccessCommandException(CommandExceptionEnum.UserNotFound);
                }

                await _userRepository.CheckSignIn(context.Message.PhoneNumber, context.Message.Password);

                user.UpdateSecurityStamp();
                string token = await _userRepository.GenerateUserToken(user, KeyWords.CustomTokenProvider, CodeType.GeneratePurposeForDefaultTwoFactor(user.GetSnapShot(), KeyWords.LoginUser));
                await _userRepository.Update(user);
                await context.RespondAsync(new UserLoginChecked { Token = token, PhoneNumber = context.Message.PhoneNumber });
            }
            catch (UserAccessCommandException ex)
            {
                await _unitOfWork.CommitChangesAsync();
                throw;
            }



        }
    }
}
