namespace CXChange.BuildingBlocks.Application.Query
{
    public interface IPagedQuery
    {
        int page { get; }
        int pageSize { get; }

    }
}
