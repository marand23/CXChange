namespace CXChange.BuildingBlocks.Domain
{
    public interface IEntity
    {
        IReadOnlyList<IDomainEvent> DomainEvents { get; }

    }
}
