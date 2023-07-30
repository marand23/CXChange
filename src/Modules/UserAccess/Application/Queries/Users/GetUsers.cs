using CXChange.BuildingBlocks.Application.Query.QueryInterfaces;

namespace CXChange.Modules.UserAccess.Application.Queries.Users
{
    public class GetUsers : IQueryBase, IPagedQuery, IFilteredQuery
    {
        public GetUsers(int page, int pagesize, Dictionary<string, string> filter)
        {
            this.page = page;
            this.pageSize = pagesize;
            Query = filter;
        }
        public string Id { get; set; }

        public int page { get; }

        public int pageSize { get; }

        public Dictionary<string, string> Query { get; }
    }
}
