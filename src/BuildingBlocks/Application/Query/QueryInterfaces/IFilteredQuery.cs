namespace CXChange.BuildingBlocks.Application.Query.QueryInterfaces
{

    public interface IFilteredQuery<T> where T : class
    {
        public T Filters { get; init; }
    }
    public record FilterItemQuery<T>
    {

        public FilterItemQuery(List<T> value, FilterType filterType = FilterType.Equal)
        {
            Value = value;
            FilterType = filterType;
        }
        public FilterType FilterType { get; init; }

        public List<T> Value { get; init; }
    }
    public enum FilterType
    {
        Equal,
        NotEqual,
        DiscreteRange,
        ContinousRange,
        Like
    }


}
