namespace CXChange.BuildingBlocks.Domain
{
    public interface IDomainEventProvider
    {
        List<IDomainEvent> GetDomainEvents();
    }
}
