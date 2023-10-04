using CXChange.Modules.UserAccess.Domain.User.DomainEvents;
using CXChange.Modules.UserAccess.Integration.IntegrationEvents;
using MassTransit;

namespace CXChange.Modules.UserAccess.Application.DomainEventHandlers.AuthenticatorDeactivatedHandlers
{
    internal class AuthenticationDeactivatedNotificationHandler : IConsumer<AuthenticatorDeactivatedDomainEvent>
    {
        IBus _bus;
        public AuthenticationDeactivatedNotificationHandler(IBus bus)
        {
            _bus = bus;
        }
        public async Task Consume(ConsumeContext<AuthenticatorDeactivatedDomainEvent> context)
        {
            await _bus.Publish(new AuthenticatorDeactivatedIntegrationEvent { UserId = context.Message.UserId });
        }
    }
}
