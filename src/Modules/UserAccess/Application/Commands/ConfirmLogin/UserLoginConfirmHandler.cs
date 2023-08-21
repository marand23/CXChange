using CXChange.BuildingBlocks.Application.Exceptions;
using CXChange.BuildingBlocks.Application.Totp;
using CXChange.BuildingBlocks.Domain;
using CXChange.Modules.UserAccess.Application.Enums;
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
        IDomainEventDispatcher _dispatcher;
        public UserLoginConfirmHandler(IUserRepository userRepository, IHttpContextAccessor httpContextAccessor, ITotp totp, IDomainEventDispatcher dispatcher)
        {
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
            _totp = totp;
            _dispatcher = dispatcher;
        }
        public async Task Consume(ConsumeContext<ConfirmUserLogin> context)
        {

            var user = await _userRepository.GetUserByPhoneNumber(context.Message.PhoneNumber);
            if (user == null)
            {
                throw new ExchangeCommandException("user does not exist", (int)CommandExceptionEnum.UserDoesNotExist);

            }
            var tokenResult = await _userRepository.ValidateUserToken(user, context.Message.Token, KeyWords.LoginTokenProvider(), KeyWords.LoginTokenProviderPurpose(context.Message.PhoneNumber));
            if (!tokenResult)
            {
                throw new ExchangeCommandException("plase login first", (int)CommandExceptionEnum.UserLoginTokenNotValid);

            }
            var codeResult = _totp.ValidateCode(user.GetSnapShot().SecurityStamp, int.Parse(context.Message.VerificationCode), TimeSpan.FromMinutes(3), KeyWords.LoginTokenProviderPurpose(context.Message.PhoneNumber));
            if (!codeResult)
            {
                throw new ExchangeCommandException("verification code not valid", (int)CommandExceptionEnum.VerificationCodeInvalid);

            }
            user.ConfirmUserLogin();
            await _userRepository.SignIn(user);
            var token = (string)_httpContextAccessor.HttpContext.Items["token"];
            await _dispatcher.Dispatch(user.DomainEvents);
            await context.RespondAsync(new UserLoginConfirmed { Token = token });
        }
    }
}
