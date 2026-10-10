using MassTransit;

namespace CXChange.Modules.Payment.Application.EventConsumers.Internal
{
    internal class WithdrawConfirmedByUserEventConsumer : IConsumer<WithdrawConfirmedByUserEventConsumer>
    {
        public Task Consume(ConsumeContext<WithdrawConfirmedByUserEventConsumer> context)
        {
            throw new NotImplementedException();
        }
    }
}
