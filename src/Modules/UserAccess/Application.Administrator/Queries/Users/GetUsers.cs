using CXChange.BuildingBlocks.Application.MessageInterfaces;
using CXChange.BuildingBlocks.Application.Query.QueryInterfaces;

namespace CXChange.Modules.UserAccess.Administrator.Application.Queries.Users
{
    public record GetUsers : IQueryBase, IPageQyery, IFilteredQuery<UserFilters>, IOrderableQuery, IValidatable
    {

        public string UserId { get; set; }
        public int Page { get; init; }

        public int PageSize { get; init; }

        public UserFilters Filters { get; init; }
        public List<OrderByQuery> OrderBy { get; init; }
    }
    public record UserFilters
    {

        public FilterItemQuery<string> PhoneNumber { get; init; }
        public FilterItemQuery<string> PhoneNumberRange { get; init; }
        public FilterItemQuery<string> Email { get; init; }
        public FilterItemQuery<bool> PhoneNumberConfirmed { get; init; }
        public FilterItemQuery<bool> EmailConfirmed { get; init; }


    }


}
