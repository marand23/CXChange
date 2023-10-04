using CXChange.Modules.UserAccess.Domain.User.DomainEvents;
using CXChange.Modules.UserAccess.Integration.IntegrationEvents;
using MassTransit;

namespace CXChange.Modules.UserAccess.Application.DomainEventHandlers.UserEmailDeactivatedHandler
{
    internal class UserEmailDeactivatedNotification : IConsumer<UserEmailDeactivatedDomainEvent>
    {
        IBus _bus;
        public UserEmailDeactivatedNotification(IBus bus)
        {
            _bus = bus;
        }

        public async Task Consume(ConsumeContext<UserEmailDeactivatedDomainEvent> context)
        {
            await _bus.Publish(new UserEmailDeactivatedIntegrationEvent { UserId = context.Message.UserId });
        }
    }
}
