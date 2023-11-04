using CXChange.Modules.UserAccess.Domain.User.DomainEvents;
using CXChange.Modules.UserAccess.Integration.IntegrationEvents;
using MassTransit;

namespace CXChange.Modules.UserAccess.Application.DomainEventHandlers.UserRegistrationConfirmedDomainEventHandlers
{
    internal class SendUserRegistrationConfirmedNotificationHandler : IConsumer<UserRegistrationConfirmedDomainEvent>
    {

        IBus _bus;
        public SendUserRegistrationConfirmedNotificationHandler(IBus bus)
        {

            _bus = bus;
        }
        public async Task Consume(ConsumeContext<UserRegistrationConfirmedDomainEvent> context)
        {





            await _bus.Publish(new UserRegistrationConfiremedIntegrationEvent { PhoneNumber = context.Message.PhoneNumber, UserId = context.Message.UserId });
            Console.WriteLine("email sent");

        }
    }
}
