using CXChange.BuildingBlocks.Application;
using CXChange.BuildingBlocks.Domain;
using CXChange.Modules.UserAccess.Domain.User.DomainEvents;
using CXChange.Modules.UserAccess.Integration.IntegrationEvents;
using MassTransit;
using MassTransit.Mediator;
using MassTransit.Testing;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CXChange.Modules.UserAccess.Application.DomainEventHandlers
{
    public class SendEmailHandler :INotificationHandler<UserRegisteredDomainEvent>
    {
        private IBus _publish;
        private readonly IUnitOfWork _unitOfWork;
        public SendEmailHandler(IBus publish,IUnitOfWork unitOfWork)
        {
            _publish = publish;
            _unitOfWork = unitOfWork;
        }

        

        public async Task Handle(UserRegisteredDomainEvent notification, CancellationToken cancellationToken)
        {
            Console.WriteLine("notificaiton recived");
        }
    }
}
