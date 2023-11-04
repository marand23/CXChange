using CXChange.Modules.Kyc.Domain.KycRequest.DomainEvents;
using CXChange.Modules.Kyc.Integration.IntegrationEvents;
using MassTransit;

namespace CXChange.Modules.Kyc.Application.DomainEventHandlers.KycInitiatedDomainEventHandlers
{
    internal class NotifyInitiatedKycDomainEventHandler : IConsumer<KycRequestInitiatedDomainEvent>
    {
        IBus _bus;
        public NotifyInitiatedKycDomainEventHandler(IBus bus)
        {
            _bus = bus;
        }
        public async Task Consume(ConsumeContext<KycRequestInitiatedDomainEvent> context)
        {
            await _bus.Publish(new KycRequestInitiatedIntegrationEvent
            {
                UserId = context.Message.UserId,
            });
        }
    }
}
