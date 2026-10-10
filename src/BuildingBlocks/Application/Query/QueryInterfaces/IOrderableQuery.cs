namespace CXChange.BuildingBlocks.Application.Query.QueryInterfaces
{
    public interface IOrderableQuery
    {
        public List<OrderByQuery> OrderBy { get; init; }

    }
    public class OrderByQuery
    {
        public string Field { get; init; }
        public string Dir { get; init; }
    }
}
