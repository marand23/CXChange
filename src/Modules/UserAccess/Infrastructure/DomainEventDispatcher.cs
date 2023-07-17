using CXChange.BuildingBlocks.Domain;
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
        IMediator mediator;
        public DomainEventDispatcher(IMediator mediator)
        {
            this.mediator = mediator;
           
        }

        private readonly List<IDomainEventProvider> providers;
        

        public Task Dispatch(List<IDomainEvent> domainEvents)
        {
           foreach (var domainEvent in domainEvents)
            {
                mediator.Publish(domainEvent);

            }
           return Task.CompletedTask;
        }
    }
}
