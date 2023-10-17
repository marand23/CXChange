namespace CXChange.BuildingBlocks.Application.Query.QueryInterfaces
{

    public interface IFilteredQuery<T> where T : class
    {
        public T Filters { get; init; }
    }
    public record FilterItemQuery<T>
    {

        public FilterItemQuery(T value, OperationType filterType = OperationType.Equal)
        {
            Value = value;
            OperationType = filterType;
        }
        public OperationType OperationType { get; init; }

        public T Value { get; init; }
    }
    public enum OperationType
    {
        Equal,
        Between,
        Like
    }


}
