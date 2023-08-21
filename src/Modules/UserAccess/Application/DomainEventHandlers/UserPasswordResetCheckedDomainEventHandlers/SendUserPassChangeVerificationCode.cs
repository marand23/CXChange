using CXChange.BuildingBlocks.Application.Totp;
using CXChange.Modules.UserAccess.Domain.User.DomainEvents;
using CXChange.Modules.UserAccess.Integration.IntegrationEvents;
using MassTransit;

namespace CXChange.Modules.UserAccess.Application.DomainEventHandlers.UserPasswordResetCheckedDomainEventHandlers
{
    internal class SendUserPassChangeVerificationCode : IConsumer<UserPasswordResetCheckedDomainEvent>
    {
        IBus _bus;
        ITotp _totp;
        public SendUserPassChangeVerificationCode(IBus bus, ITotp totp)
        {
            _bus = bus;
        }

        public async Task Consume(ConsumeContext<UserPasswordResetCheckedDomainEvent> context)
        {
            var verificationCode = _totp.GenerateCode(context.Message.SecurityStamp, TimeSpan.FromMinutes(3), KeyWords.ResetPasswordTotpModifier(context.Message.PhoneNumber));
            await _bus.Send(new UserPasswordResetCheckedIntegrationEvent { PhoneNumber = context.Message.PhoneNumber, Id = context.Message.Id, VerificationCode = verificationCode.ToString() });
        }
    }
}
