using CXChange.Modules.Payment.Domain.Withdraw.DomainEvents;
using CXChange.Modules.Payment.Integration;
using MassTransit;

namespace CXChange.Modules.Payment.Application.DomianEventHandlers.WithdrawConfirmedByUserDomainEventHandler
{
    internal class NotifyWithdrawConfirmationByUserHandler : IConsumer<WithdrawConfirmedByUserDomainEvent>
    {
        IBus _bus;

        public NotifyWithdrawConfirmationByUserHandler(IBus bus)
        {
            _bus = bus;

        }

        public async Task Consume(ConsumeContext<WithdrawConfirmedByUserDomainEvent> context)
        {
            await _bus.Publish(new WithdrawSubmittedIntegrationEvent { WithdrawId = context.Message.WithdrawId });
        }
    }
}
