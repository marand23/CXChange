using CXChange.BuildingBlocks.Application.Totp;
using CXChange.Modules.UserAccess.Domain.User.DomainEvents;
using CXChange.Modules.UserAccess.Integration.IntegrationEvents;
using MassTransit;

namespace CXChange.Modules.UserAccess.Application.DomainEventHandlers.UserLoginCheckedDomainEventHandlers
{
    internal class SendLoginVerificationSms : IConsumer<UserLoginCheckedDomainEvent>
    {
        IBus _bus;
        ITotp _totp;
        public SendLoginVerificationSms(IBus bus, ITotp totp)
        {
            _bus = bus;
            _totp = totp;
        }

        public async Task Consume(ConsumeContext<UserLoginCheckedDomainEvent> context)
        {
            if (context.Message.DefaultTwoFactor == "SMS")
            {

                var verificationCode = _totp.GenerateCode(context.Message.SecurityStamp, TimeSpan.FromMinutes(2), KeyWords.LoginTotpModifier(context.Message.PhoneNumber));
                await _bus.Publish(new UserLoginCheckedIntegrationEvent { Id = context.Message.Id, PhoneNumber = context.Message.PhoneNumber, VerificationCode = verificationCode.ToString() });
                Console.WriteLine($"login confirmation code sent {verificationCode}");
            }

        }
    }
}
