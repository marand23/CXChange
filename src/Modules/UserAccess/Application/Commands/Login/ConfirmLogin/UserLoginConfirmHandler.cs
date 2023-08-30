using CXChange.BuildingBlocks.Application.Exceptions;
using CXChange.BuildingBlocks.Domain;
using CXChange.Modules.UserAccess.Application.Enums;
using CXChange.Modules.UserAccess.Application.Services.Interfaces;
using CXChange.Modules.UserAccess.Application.Utils;
using CXChange.Modules.UserAccess.Domain.Interfaces.Repositories;
using MassTransit;
using Microsoft.AspNetCore.Http;


namespace CXChange.Modules.UserAccess.Application.Commands.Login.ConfirmLogin
{
    internal class UserLoginConfirmHandler : IConsumer<ConfirmUserLogin>
    {
        IHttpContextAccessor _httpContextAccessor;

        IUserRepository _userRepository;

        IDomainEventDispatcher _dispatcher;
        IVerificationCodeFunctions _verifier;
        public UserLoginConfirmHandler(IUserRepository userRepository, IHttpContextAccessor httpContextAccessor, IDomainEventDispatcher dispatcher, IVerificationCodeFunctions verifier)
        {
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
            _verifier = verifier;
            _dispatcher = dispatcher;
        }
        public async Task Consume(ConsumeContext<ConfirmUserLogin> context)
        {

            var user = await _userRepository.GetUserByPhoneNumber(context.Message.PhoneNumber);
            if (user == null)
            {
                throw new ExchangeCommandException("user does not exist", CommandExceptionEnum.UserDoesNotExist);

            }
            var tokenResult = await _userRepository.ValidateUserToken(user, context.Message.Token, KeyWords.CustomTokenProvider, string.Concat(KeyWords.LoginUser, context.Message.PhoneNumber));
            if (!tokenResult)
            {
                throw new ExchangeCommandException("plase login first", CommandExceptionEnum.UserLoginTokenNotValid);

            }

            user.ConfirmUserLogin();
            await _userRepository.SignIn(user);
            var token = (string)_httpContextAccessor.HttpContext.Items["token"];
            await _dispatcher.Dispatch(user.DomainEvents);
            //update user securtiy stamp
            await context.RespondAsync(new UserLoginConfirmed { Token = token });
        }
    }
}
