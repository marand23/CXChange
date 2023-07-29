using CXChange.BuildingBlocks.Domain;
using CXChange.Modules.UserAccess.Domain.Interfaces.Repositories;
using CXChange.Modules.UserAccess.Domain.User;
using MassTransit;

namespace CXChange.Modules.UserAccess.Application.Commands.UserRegistration
{
    internal class RegisterUserHandler : IConsumer<RegisterUser>
    {
        IUserRepository _userRepository;
        IDomainEventDispatcher _dispatcher;

        public RegisterUserHandler(IUserRepository userRepository, IDomainEventDispatcher dispatcher)
        {
            _userRepository = userRepository;
            _dispatcher = dispatcher;


        }

        public async Task Consume(ConsumeContext<RegisterUser> context)
        {



            var user = new User();
            var a = user.RegisterNewUser(context.Message.PhoneNumber, context.Message.Password);
            await _userRepository.CreateUser(a);

            await _dispatcher.Dispatch(user.DomainEvents);






            await context.RespondAsync(new UserRegistered { PhoneNumber = context.Message.PhoneNumber, TwoFactorType = TwoFactorType.SMS });
            {



            };


        }



    }






}

