using CXChange.API.Modules.UserManagment.Requests.Filter;

namespace CXChange.API.Modules.UserManagment.Requests.User
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
    public FilterItemRequest<string> PhoneNumberRange { get; set; }
    public FilterItemRequest<string> Email { get; set; }
    public FilterItemRequest<bool> PhoneNumberConfirmed { get; set; }
    public FilterItemRequest<bool> EmailConfirmed { get; set; }
}



