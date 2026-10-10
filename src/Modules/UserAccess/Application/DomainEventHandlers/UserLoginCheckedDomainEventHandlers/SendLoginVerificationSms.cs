
using CXChange.Modules.UserAccess.Application.Services.Interfaces;
using CXChange.Modules.UserAccess.Application.Utils;
using CXChange.Modules.UserAccess.Domain.User.DomainEvents;
using MassTransit;

namespace CXChange.Modules.UserAccess.Application.DomainEventHandlers.UserLoginCheckedDomainEventHandlers
{
    internal class SendLoginVerificationSms : IConsumer<UserLoginCheckedDomainEvent>
    {

        IVerificationCodeFunctions _sender;
        public SendLoginVerificationSms(IVerificationCodeFunctions sender)
        {

            _sender = sender;
        }

        public async Task Consume(ConsumeContext<UserLoginCheckedDomainEvent> context)
        {
            await _sender.Send(context.Message.User, CodeType.GeneratePurposeForDefaultTwoFactor(context.Message.User.GetSnapShot(), KeyWords.LoginUser));


        }
    }
}
