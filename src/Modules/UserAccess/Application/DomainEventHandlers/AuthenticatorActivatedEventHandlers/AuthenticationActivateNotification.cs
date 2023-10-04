using CXChange.Modules.UserAccess.Domain.User.DomainEvents;
using CXChange.Modules.UserAccess.Integration.IntegrationEvents;
using MassTransit;

namespace CXChange.Modules.UserAccess.Application.DomainEventHandlers.AuthenticatorActivatedEventHandlers
{
    internal class AuthenticationActivateNotification : IConsumer<AuthenticatorActivatedDomainEvent>
    {
        IBus _bus;
        public AuthenticationActivateNotification(IBus bus)
        {
            _bus = bus;
        }

        public async Task Consume(ConsumeContext<AuthenticatorActivatedDomainEvent> context)
        {
            await _bus.Publish(new AuthenticatorActivatedIntegrationEvent { UserId = context.Message.UserId });
        }
    }
}
