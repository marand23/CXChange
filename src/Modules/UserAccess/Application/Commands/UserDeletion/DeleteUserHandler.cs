using CXChange.Modules.UserAccess.Domain.Interfaces.Repositories;
using MassTransit;

namespace CXChange.Modules.UserAccess.Application.Commands.UserDeletion
{
    internal class DeleteUserHandler : IConsumer<DeleteUser>
    {

        IUserRepository _userRepository;
        public DeleteUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Consume(ConsumeContext<DeleteUser> context)
        {
            await _userRepository.DeleteUser(context.Message.UserId);
            await context.RespondAsync(new UserDeleted { }/*new UserDeleted { PhoneNumber = context.Message.UserId */);
        }
    }
}
