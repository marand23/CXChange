using CXChange.Modules.UserAccess.Domain.User.DomainEvents;
using CXChange.Modules.UserAccess.Integration.IntegrationEvents;
using MassTransit;

namespace CXChange.Modules.UserAccess.Application.DomainEventHandlers.EmailActivateDomainEventHandlers
{
    internal class EmailActivationNotificationHandler : IConsumer<EmailActivatedDomainEvent>
    {
        IBus _bus;
        public EmailActivationNotificationHandler(IBus bus)
        {
            _bus = bus;
        }
        public async Task Consume(ConsumeContext<EmailActivatedDomainEvent> context)
        {
            await _bus.Publish(new EmailActivatedIntegrationEvent
            {
                Email = context.Message.Email,
                Id = context.Message.Id
            });
        }
    }
}
