using CXChange.Modules.Payment.Domain.Withdraw.DomainEvents;
using MassTransit;

namespace CXChange.Modules.Payment.Application.DomianEventHandlers.WithdrawConfirmedByUserDomainEventHandler
{
    internal class SendWithdrawHandler : IConsumer<WithdrawConfirmedByUserDomainEvent>
    {
        IBus _bus;
        public SendWithdrawHandler(IBus bus)
        {
            _bus = bus;
        }
        public Task Consume(ConsumeContext<WithdrawConfirmedByUserDomainEvent> context)
        {
            throw new NotImplementedException();
        }
    }
}
