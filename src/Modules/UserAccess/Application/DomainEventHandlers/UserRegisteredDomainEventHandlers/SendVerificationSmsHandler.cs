using CXChange.Modules.UserAccess.Application.Services.Interfaces;
using CXChange.Modules.UserAccess.Application.Utils;
using CXChange.Modules.UserAccess.Domain.User.DomainEvents;
using MassTransit;

namespace CXChange.Modules.UserAccess.Application.DomainEventHandlers.UserRegisteredDomainEventHandlers
{
    internal class SendVerificationSmsHandler : IConsumer<UserRegisteredDomainEvent>
    {
        IVerificationCodeFunctions _sender;
        public SendVerificationSmsHandler(IVerificationCodeFunctions sender)
        {
            _sender = sender;
        }

        public async Task Consume(ConsumeContext<UserRegisteredDomainEvent> context)
        {
            await _sender.Send(context.Message.User, string.Concat(KeyWords.ConfirmRegister, context.Message.User.GetSnapShot().Id));

        }
    }
}
