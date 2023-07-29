using CXChange.BuildingBlocks.Domain;

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
