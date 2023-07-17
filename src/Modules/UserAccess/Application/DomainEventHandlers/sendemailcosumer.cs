using CXChange.BuildingBlocks.Domain;
using CXChange.Modules.UserAccess.Domain.User.DomainEvents;
using CXChange.Modules.UserAccess.Integration.IntegrationEvents;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CXChange.Modules.UserAccess.Application.DomainEventHandlers
{
    public class sendemailcosumer : IConsumer<UserRegistrationConfirmedIntegrationEvent>
    {
        private readonly IUnitOfWork _unitOfWork;
        public sendemailcosumer(IUnitOfWork unitOfWork)
        {
             _unitOfWork = unitOfWork;
        }
        public async Task Consume(ConsumeContext<UserRegistrationConfirmedIntegrationEvent> context)
        {
         // await  Task.Delay(TimeSpan.FromSeconds(100));
            Console.WriteLine("this is consumer");
           
             
        }
    }
}
