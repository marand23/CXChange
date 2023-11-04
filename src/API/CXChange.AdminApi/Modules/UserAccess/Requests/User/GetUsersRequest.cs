using CXChange.AdminAPI.Modules.UserAccess.Requests.Filter;

namespace CXChange.AdminAPI.Modules.UserAccess.Requests.User
{
    public class GetUsersRequest
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public FiltersRequest Filters { get; set; }
        public List<OrderByRequest> OrderBy { get; set; }

    }

}
public class FiltersRequest
{

    public FilterItemRequest<string> PhoneNumber { get; set; }

    public FilterItemRequest<string> Email { get; set; }

    public FilterItemRequest<bool> EmailConfirmed { get; set; }
    public FilterItemRequest<bool> PhoneNumberConfirmed { get; set; }
}



