using CXChange.BuildingBlocks.Application.Exceptions;
using CXChange.BuildingBlocks.Domain;
using CXChange.Modules.UserAccess.Application.Enums;
using CXChange.Modules.UserAccess.Domain.Interfaces.Repositories;
using MassTransit;

namespace CXChange.Modules.UserAccess.Application.Commands.EmailActivate
{
    internal class ActivateEmailHandler : IConsumer<ActivateEmail>
    {
        IUserRepository _repo;
        IDomainEventDispatcher _dispatcher;
        public ActivateEmailHandler(IUserRepository repo, IDomainEventDispatcher dispatcher)
        {
            _repo = repo;
            _dispatcher = dispatcher;
        }
        public async Task Consume(ConsumeContext<ActivateEmail> context)
        {
            var user = await _repo.GetUserById(context.Message.UserId);
            if (user == null)
            {
                throw new ExchangeCommandException("user does not exist", CommandExceptionEnum.UserDoesNotExist);

            }
            //check token
            user.ActivateEmail(context.Message.Email);
            await _repo.Update(user);
            await context.RespondAsync(new EmailActivated { });

        }
    }
}
