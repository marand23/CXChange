namespace CXChange.BuildingBlocks.Domain
{
    public interface IDomainEventDispatcher
    {
        Task Dispatch(List<IDomainEvent> domainEvents);

    }
}
