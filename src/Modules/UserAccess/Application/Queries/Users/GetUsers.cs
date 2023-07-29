using CXChange.BuildingBlocks.Application.Query;

namespace CXChange.Modules.UserAccess.Application.Queries.Users
{
    public class GetUsers : IQueryBase, IPagedQuery
    {
        public GetUsers(int page, int pagesize)
        {
            this.page = page;
            this.pageSize = pagesize;
        }
        public string Id { get; set; }

        public int page { get; }

        public int pageSize { get; }

    }
}
