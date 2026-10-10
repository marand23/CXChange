using CXChange.BuildingBlocks.Application.Query.QueryInterfaces;

namespace CXChange.Modules.UserAccess.Application.Queries.UserAuthenticationTickets
{
    public record GetUserAuthenticationTickets : IQueryBase, IPageQyery, IOrderableQuery
    {
        public string UserId { get; init; }
        public int Page { get; init; }

        public int PageSize { get; init; }
        public List<OrderByQuery> OrderBy { get; init; }
    }

}
