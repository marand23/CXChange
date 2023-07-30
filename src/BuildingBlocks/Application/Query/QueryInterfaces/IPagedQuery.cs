namespace CXChange.BuildingBlocks.Application.Query.QueryInterfaces
{
    public interface IPagedQuery
    {
        int page { get; }
        int pageSize { get; }

    }
}
