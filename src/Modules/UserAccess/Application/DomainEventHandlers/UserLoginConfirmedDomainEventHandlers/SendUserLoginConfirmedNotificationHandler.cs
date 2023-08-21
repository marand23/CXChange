using CXChange.Modules.UserAccess.Domain.User.DomainEvents;
using CXChange.Modules.UserAccess.Integration.IntegrationEvents;
using MassTransit;

namespace CXChange.Modules.UserAccess.Application.DomainEventHandlers.UserLoginConfirmedDomainEventHandlers
{

    internal class SendUserLoginConfirmedNotificationHandler : IConsumer<UserLoginConfirmedDomainEvent>
    {
        IBus _bus;
        public SendUserLoginConfirmedNotificationHandler(IBus bus)
        {
            _bus = bus;
        }
        public async Task Consume(ConsumeContext<UserLoginConfirmedDomainEvent> context)
        {
            await _bus.Publish(new UserLoginConfirmedIntegrationEvent { Id = context.Message.Id, PhoneNumber = context.Message.PhoneNumber });
        }
    }
}
