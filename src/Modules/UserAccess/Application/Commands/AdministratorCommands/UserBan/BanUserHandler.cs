using CXChange.BuildingBlocks.Domain;
using CXChange.Modules.UserAccess.Application.Exceptions;
using CXChange.Modules.UserAccess.Domain.Interfaces.Repositories;
using MassTransit;

namespace CXChange.Modules.UserAccess.Application.Commands.AdministratorCommands.UserBan
{
    internal class BanUserHandler : IConsumer<BanUser>
    {
        IUserRepository _repo;
        IDomainEventDispatcher _dispatcher;
        public BanUserHandler(IUserRepository repo, IDomainEventDispatcher dispatcher)
        {
            _repo = repo;
            _dispatcher = dispatcher;
        }

        public async Task Consume(ConsumeContext<BanUser> context)
        {
            var user = await _repo.GetUserById(context.Message.UserId);
            if (user == null)
            {
                throw new UserAccessCommandException(Enums.CommandExceptionEnum.UserNotFound);
            }

        }
    }
}
