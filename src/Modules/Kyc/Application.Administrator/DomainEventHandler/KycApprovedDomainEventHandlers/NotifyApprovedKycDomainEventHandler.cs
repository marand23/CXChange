using CXChange.Modules.Kyc.Integration.IntegrationEvents;
using MassTransit;

namespace CXChange.Modules.Kyc.Administrator.Application.DomainEventHandler.KycApprovedDomainEventHandlers
{
    internal class NotifyApprovedKycDomainEventHandler : IConsumer<KycApprovedIntegrationEvent>
    {
        public IBus _bus;
        public NotifyApprovedKycDomainEventHandler(IBus bus)
        {
            _bus = bus;
        }
        public async Task Consume(ConsumeContext<KycApprovedIntegrationEvent> context)
        {
            await _bus.Publish(new KycApprovedIntegrationEvent
            {
                UserId = context.Message.UserId,
            });
        }
    }
}
