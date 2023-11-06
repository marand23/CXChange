using CXChange.Modules.UserAccess.Application.Utils;
using CXChange.Modules.UserAccess.Integration.IntegrationEvents;
using MassTransit;

namespace CXChange.Modules.UserAccess.Application.ApplicationEvents
{
    internal class TotpCodeVerifiedEventHandler : IConsumer<TotpCodeVerifiedEvent>
    {
        public IBus _bus;
        public TotpCodeVerifiedEventHandler(IBus bus)
        {
            _bus = bus;
        }
        public async Task Consume(ConsumeContext<TotpCodeVerifiedEvent> context)
        {
            if (context.Message.CodeType == CodeType.GetCodeTypeByName("LandLine"))
            {
                await _bus.Publish(new LandLineCodeVerifiedIntegrationEvent { UserId = context.Message.UserId, LandLine = context.Message.Info });
            }
        }
    }
}
