using CXChange.Modules.Kyc.Domain.User.DomainEvents;
using CXChange.Modules.Kyc.Integration.IntegrationEvents;
using MassTransit;

namespace CXChange.Modules.Kyc.Administrator.Application.DomainEventHandler.KycRejectedDomainEventHandlers
{
    internal class NotifyRejectedKycDomainEventHandler : IConsumer<KycRejectedDomainEvent>
    {
        public IBus _bus;
        public NotifyRejectedKycDomainEventHandler(IBus bus)
        {
            _bus = bus;
        }
        public async Task Consume(ConsumeContext<KycRejectedDomainEvent?> context)
        {
            await _bus.Publish(new KycRejectedIntegrationEvent
            {
                UserId = context.Message.UserId
            });
        }
    }
}
