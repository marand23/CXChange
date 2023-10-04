using CXChange.BuildingBlocks.Domain;
using MassTransit.Mediator;

namespace CXChange.Modules.UserAccess.Infrastructure
{
    internal class DomainEventDispatcher : IDomainEventDispatcher
    {
        IScopedMediator mediator;

        public DomainEventDispatcher(IScopedMediator mediator)
        {
            this.mediator = mediator;



        }



        public Task Dispatch(List<IDomainEvent> domainEvents)
        {
            if (domainEvents != null)
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
