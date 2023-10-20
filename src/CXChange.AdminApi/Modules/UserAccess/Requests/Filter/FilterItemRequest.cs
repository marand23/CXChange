using CXChange.BuildingBlocks.Application.Query.QueryInterfaces;

namespace CXChange.AdminAPI.Modules.UserAccess.Requests.Filter
{
    public class FilterItemRequest<T>
    {
        public List<T> Value { get; set; }
        public FilterType FilterType { get; set; }
    }
}
