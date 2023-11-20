namespace CXChange.BuildingBlocks.Domain
{
    public interface IDomainEventDispatcher
    {
        Task Dispatch(IReadOnlyList<IDomainEvent> domainEvents);

    }
}
