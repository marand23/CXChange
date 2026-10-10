using CXChange.BuildingBlocks.Domain.Specifications;

namespace CXChange.BuildingBlocks.Domain
{

    public abstract class Entity<TId> : IEntity
    {
        public TId Id { get; protected set; }
        private List<IDomainEvent> _domainEvents;
        public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents?.AsReadOnly();

        public void AddDomainEvent(IDomainEvent eventItem)
        {

            _domainEvents = _domainEvents ?? new List<IDomainEvent>();
            _domainEvents.Add(eventItem);
        }

        public void Clear()
        {
            _domainEvents.Clear();
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
