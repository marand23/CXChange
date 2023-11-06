using CXChange.BuildingBlocks.Domain;
using CXChange.Modules.UserAccess.Application.Commands.UserDeletion;
using CXChange.Modules.UserAccess.Domain.Interfaces.Repositories;
using CXChange.Modules.UserAccess.Domain.User;
using MassTransit;
using MassTransit.Mediator;

namespace CXChange.Modules.UserAccess.Application.Commands.Register.UserRegistration
{
    internal class RegisterUserHandler : IConsumer<RegisterUser>
    {
        IUserRepository _userRepository;
        IDomainEventDispatcher _dispatcher;

        IMediator _mediator;


        public RegisterUserHandler(IUserRepository userRepository, IDomainEventDispatcher dispatcher, IMediator mediator)
        {
            _userRepository = userRepository;
            _dispatcher = dispatcher;
            _mediator = mediator;

        }

        public async Task Consume(ConsumeContext<RegisterUser> context)
        {



            // Set the worker id provider to use the random MAC address
            NewId.SetWorkerIdProvider(new iworker());
            Console.WriteLine(NewId.NextSequentialGuid());
            var user = await _userRepository.GetUserByPhoneNumber(context.Message.PhoneNumber);
            if (user != null)
            {

                user.CheckRegisterRequirements();
                var client = _mediator.CreateRequestClient<DeleteUser>();

                var response = await client.GetResponse<UserDeleted>(new DeleteUser { UserId = user.GetSnapShot().Id });

            }

            user = User.RegisterNewUser(context.Message.PhoneNumber, context.Message.Password, true, context.Message.PhoneNumber, NewId.NextSequentialGuid());
            await _userRepository.CreateUser(user);
            await _dispatcher.Dispatch(user.DomainEvents);
            await context.RespondAsync(new UserRegistered { PhoneNumber = context.Message.PhoneNumber, TwoFactorType = TwoFactorType.SMS });
            {
            };
        }
    }
    public class iworker : IWorkerIdProvider
    {
        public byte[] GetWorkerId(int index)
        {
            return new byte[] { 0x00, 0x11, 0x22, 0x33, 0x44, 0x66, 0x99 };
        }
    }
}

