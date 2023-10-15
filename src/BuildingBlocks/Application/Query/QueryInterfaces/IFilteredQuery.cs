namespace CXChange.BuildingBlocks.Application.Query.QueryInterfaces
{
    public interface IFilteredQuery<T> where T : class
    {
        public T Filters { get; set; }
    }
    public record IFilterItem<T>
    {


        public OperationType OperationType { get; set; }

        public T Value { get; set; }
    }
    public enum OperationType
    {
        Equal,
        Between,
        Like
    }


}
