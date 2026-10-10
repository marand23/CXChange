using CXChange.Modules.UserAccess.Domain.Interfaces.Repositories;
using CXChange.Modules.UserAccess.Domain.User.DomainEvents;
using MassTransit;

namespace CXChange.Modules.UserAccess.Application.DomainEventHandlers.UserBannedDomainEventHandler
{
    internal class UserBannedNotificationHandler : IConsumer<UserBannedDomainEvent>
    {
        public ITicketRepository _ticket;
        public UserBannedNotificationHandler(ITicketRepository ticket)
        {
            _ticket = ticket;
        }
        public Task Consume(ConsumeContext<UserBannedDomainEvent> context)
        {
            throw new NotImplementedException();
        }
    }
}
