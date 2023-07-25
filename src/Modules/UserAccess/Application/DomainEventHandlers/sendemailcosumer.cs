using CXChange.BuildingBlocks.Application.Totp;
using CXChange.BuildingBlocks.Domain;
using CXChange.Modules.UserAccess.Domain.User.DomainEvents;
using CXChange.Modules.UserAccess.Integration.IntegrationEvents;
using MassTransit;
using RabbitMQ.Client.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CXChange.Modules.UserAccess.Application.DomainEventHandlers
{
    public class UserRegisteredDomainEventHandler : IConsumer<UserRegisteredDomainEvent>
    {
        ITotp _totp;
        IBus _bus;
        public UserRegisteredDomainEventHandler(ITotp totp,IBus bus)
        {
            _totp = totp;
            _bus = bus;
        }

        public async Task Consume(ConsumeContext<UserRegisteredDomainEvent> context)
        {
            int Code = _totp.GenerateCode(context.Message.SecurityToken, TimeSpan.FromMinutes(3), "user-registered");
           await _bus.Publish(new UserRegistrationConfirmedIntegrationEvent { PhoneNumber = context.Message.PhoneNumber , VerificationCode=Code.ToString()});  
           Console.WriteLine(Code);
        }
    }
}
