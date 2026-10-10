namespace CXChange.BuildingBlocks.Domain
{
    public interface IVersionedEntity
    {
        int Version { get; set; }
    }
}
