using CXChange.Modules.UserAccess.Domain.User.DomainEvents;
using CXChange.Modules.UserAccess.Integration.IntegrationEvents;
using MassTransit;

namespace CXChange.Modules.UserAccess.Application.DomainEventHandlers.UserTwoFactorTypeChangedDomainEventHandlers
{
    internal class SendTwoFactorTypeChangeNotificationHandler : IConsumer<UserTwoFactorTypeChangedDomainEvent>
    {
        IBus _bus;
        public SendTwoFactorTypeChangeNotificationHandler(IBus bus)
        {
            _bus = bus;
        }

        public async Task Consume(ConsumeContext<UserTwoFactorTypeChangedDomainEvent> context)
        {
            await _bus.Publish(new UserTwoFactorTypeChangedIntegrationEvent { UserId = context.Message.UserId });
        }
    }
}
