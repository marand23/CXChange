using CXChange.BuildingBlocks.Application.Exceptions;
using CXChange.BuildingBlocks.Domain;
using CXChange.Modules.UserAccess.Application.Enums;
using CXChange.Modules.UserAccess.Application.Services.Interfaces;
using CXChange.Modules.UserAccess.Application.Utils;
using CXChange.Modules.UserAccess.Domain.Interfaces.Repositories;
using CXChange.Modules.UserAccess.Domain.User;
using MassTransit;
using Microsoft.AspNetCore.Http;

namespace CXChange.Modules.UserAccess.Application.Commands.Register.UserRegisterConfirmation
{
    internal class ConfirmUserRegistrationHandler : IConsumer<ConfirmUser>
    {
        IUserRepository _userRepository;
        IHttpContextAccessor _httpContextAccessor;
        IDomainEventDispatcher _dispatcher;
        IVerificationCodeFunctions _verifier;
        public ConfirmUserRegistrationHandler(IUserRepository userRepository, IHttpContextAccessor httpContextAccessor, IDomainEventDispatcher dispatcher, IVerificationCodeFunctions verifier)
        {
            _userRepository = userRepository;


            _httpContextAccessor = httpContextAccessor;
        }
        public async Task Consume(ConsumeContext<ConfirmUser> context)
        {
            User user = await _userRepository.GetUserByPhoneNumber(context.Message.PhoneNumber);
            if (user == null)
            {
                throw new ExchangeCommandException("user does not exist", CommandExceptionEnum.UserDoesNotExist);
            }

            var result = _verifier.Verify(user, int.Parse(context.Message.VerificationCode), string.Concat(KeyWords.ConfirmRegister, context.Message.PhoneNumber));

            if (!result)
            {
                throw new ExchangeCommandException("verification code is invalid", CommandExceptionEnum.VerificationCodeInvalid);
            }
            user.ConfirmUser();
            await _userRepository.Update(user);
            await _userRepository.SignIn(user);

            var token = (string)_httpContextAccessor.HttpContext.Items["token"];
            await _dispatcher.Dispatch(user.DomainEvents);
            //update security stamp
            await context.RespondAsync(new UserConfirmed
            {
                Token = token
            });


        }
    }
}
