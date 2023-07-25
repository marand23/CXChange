using CXChange.BuildingBlocks.Domain;
using MassTransit.Mediator;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CXChange.Modules.UserAccess.Infrastructure
{
    internal class DomainEventDispatcher : IDomainEventDispatcher
    {
        MassTransit.Mediator.IMediator mediator;
        public DomainEventDispatcher(MassTransit.Mediator.IMediator mediator)
        {
            this.mediator = mediator;
           
        }

        private readonly List<IDomainEventProvider> providers;
        

        public Task Dispatch(List<IDomainEvent> domainEvents)
        {
            if(domainEvents!=null)
            {
                foreach (var domainEvent in domainEvents)
                {
                    mediator.Publish(domainEvent);

                }
            }
         
           return Task.CompletedTask;
        }
    }
}
