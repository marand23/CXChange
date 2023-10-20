using CXChange.AdminAPI.Modules.UserAccess.Requests.Filter;
using CXChange.BuildingBlocks.Application.Query.QueryInterfaces;

namespace CXChange.AdminAPI.Modules.UserAccess.Requests.User
{
    public class GetUsersRequest
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public FiltersRequst Filters { get; set; }
        public List<OrderByRequest> OrderBy { get; set; }

    }

}
public class FiltersRequst
{

    public FilterItemRequest<string> PhoneNumber { get; set; }

    public FilterItemRequest<string> Email { get; set; }

    public FilterItemRequest<bool> EmailConfirmed { get; set; }
    public FilterItemRequest<bool> PhoneNumberConfirmed { get; set; }
}
public class FilterItemRequest<T>
{
    public List<T> Value { get; set; }
    public FilterType FilterType { get; set; }
}


