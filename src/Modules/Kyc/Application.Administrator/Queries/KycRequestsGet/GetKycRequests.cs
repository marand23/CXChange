using CXChange.BuildingBlocks.Application.Query.QueryInterfaces;

namespace CXChange.Modules.Kyc.Administrator.Application.Queries.KycRequestsGet
{
    public record GetKycRequests : IQueryBase, IPageQyery, IFilteredQuery<KycRequestFilters>, IOrderableQuery
    {

        public List<OrderByQuery> OrderBy { get; init; }
        public int Page { get; init; }
        public int PageSize { get; init; }
        public KycRequestFilters Filters { get; init; }
    }

}
public record KycRequestFilters
{

    public FilterItemQuery<string> UserId { get; init; }
    public FilterItemQuery<string> NationalId { get; init; }
    public FilterItemQuery<string> BirthDate { get; init; }
    public FilterItemQuery<string> Name { get; init; }
    public FilterItemQuery<string> SurName { get; init; }


}
