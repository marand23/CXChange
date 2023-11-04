using CXChange.BuildingBlocks.Domain.Specifications;

namespace CXChange.BuildingBlocks.Domain
{
    public abstract class Entity<TId>
    {
        public TId Id { get; protected set; }
        private List<IDomainEvent> _domainEvents;
        public List<IDomainEvent> DomainEvents => _domainEvents;

        public void AddDomainEvent(IDomainEvent eventItem)
        {
            _domainEvents = _domainEvents ?? new List<IDomainEvent>();
            _domainEvents.Add(eventItem);
        }

        public void RemoveDomainEvent(IDomainEvent eventItem)
        {
            _domainEvents?.Remove(eventItem);
        }
        public void CheckRule(IBussinessRuleSpecification rule)
        {
            if (!rule.isSatisfiedBy())
            {
                throw new BusinessRuleException(rule);
            }
        }

    }
}
