namespace CXChange.BuildingBlocks.Application.Query.QueryInterfaces
{
    public interface IOrderableQuery
    {
        public List<Order> OrderBy { get; set; }

    }
    public class Order
    {
        public string Field { get; set; }
        public string Dir { get; set; }
    }
}
