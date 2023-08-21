using CXChange.BuildingBlocks.Application.Totp;
using CXChange.Modules.UserAccess.Domain.User.DomainEvents;
using CXChange.Modules.UserAccess.Integration.IntegrationEvents;
using MassTransit;

namespace CXChange.Modules.UserAccess.Application.DomainEventHandlers.UserRegisteredDomainEventHandlers
{
    public class SendVerificationSmsHandler : IConsumer<UserRegisteredDomainEvent>
    {
        ITotp _totp;
        IBus _bus;
        public SendVerificationSmsHandler(ITotp totp, IBus bus)
        {
            _totp = totp;
            _bus = bus;
        }

        public async Task Consume(ConsumeContext<UserRegisteredDomainEvent> context)
        {
            int verificationCode = _totp.GenerateCode(context.Message.SecurityToken, TimeSpan.FromMinutes(3), KeyWords.ConfirmRegisterTotpModifier(context.Message.PhoneNumber));
            await _bus.Publish(new UserRegisteredIntegrationEvent { PhoneNumber = context.Message.PhoneNumber, VerificationCode = verificationCode.ToString() });
            Console.WriteLine(verificationCode);
        }
    }
}
