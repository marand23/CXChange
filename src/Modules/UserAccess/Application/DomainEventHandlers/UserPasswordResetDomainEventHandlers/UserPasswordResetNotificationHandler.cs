using CXChange.Modules.UserAccess.Domain.User.DomainEvents;
using CXChange.Modules.UserAccess.Integration.IntegrationEvents;
using MassTransit;

namespace CXChange.Modules.UserAccess.Application.DomainEventHandlers.UserPasswordResetDomainEventHandlers
{
    internal class UserPasswordResetNotificationHandler : IConsumer<UserPasswordResetDomainEvent>
    {
        IBus _bus;
        public UserPasswordResetNotificationHandler(IBus bus)
        {
            _bus = bus;
        }
        public async Task Consume(ConsumeContext<UserPasswordResetDomainEvent> context)
        {
            await _bus.Publish(new PasswordResetIntegrationEvent { UserId = context.Message.Id });
        }
    }
}
