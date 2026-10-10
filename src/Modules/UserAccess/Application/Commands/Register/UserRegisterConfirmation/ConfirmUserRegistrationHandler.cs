using CXChange.BuildingBlocks.Application;
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
            _dispatcher = dispatcher;
            _verifier = verifier;
        }
        public async Task Consume(ConsumeContext<ConfirmUser> context)
        {
            User user = await _userRepository.GetUserByPhoneNumber(context.Message.PhoneNumber);
            if (user == null)
            {
                await context.RespondAsync(Result.CommandError(CommandExceptionEnum.UserNotFound, "798789"));
                return;
            }
            var snapshot = user.GetSnapShot();
            var result = _verifier.Verify(user, int.Parse(context.Message.VerificationCode), CodeType.GeneratePurposeForSpecificTwoFactor(snapshot.Id, KeyWords.ConfirmRegister, snapshot.PhoneNumber));

            if (!result)
            {
                await context.RespondAsync(Result.CommandError(CommandExceptionEnum.VerificationCodeInvalid, "798789"));
                return;
            }
            user.ConfirmUserRegister();
            user.UpdateSecurityStamp();
            await _userRepository.Update(user);
            await _userRepository.SignIn(user);

            var token = (string)_httpContextAccessor.HttpContext.Items["token"];
            /*await*/
            await _dispatcher.Dispatch(user.DomainEvents);
            //update security stamp
            await context.RespondAsync(new UserConfirmed
            {
                Token = token
            });


        }
    }
}
