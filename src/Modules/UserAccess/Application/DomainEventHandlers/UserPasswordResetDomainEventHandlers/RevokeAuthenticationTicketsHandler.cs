using CXChange.Modules.UserAccess.Domain.Interfaces.Repositories;
using CXChange.Modules.UserAccess.Domain.User.DomainEvents;
using MassTransit;

namespace CXChange.Modules.UserAccess.Application.DomainEventHandlers.UserPasswordResetDomainEventHandlers
{
    internal class RevokeAuthenticationTicketsHandler : IConsumer<UserPasswordResetDomainEvent>
    {
        ITicketRepository _repo;

        public RevokeAuthenticationTicketsHandler(ITicketRepository repo)
        {
            _repo = repo;
        }
        public async Task Consume(ConsumeContext<UserPasswordResetDomainEvent> context)
        {
            _repo.RemoveAllAsync(context.Message.Id);
        }
    }
}
