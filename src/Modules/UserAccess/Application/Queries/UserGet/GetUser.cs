using CXChange.BuildingBlocks.Application.Query.QueryInterfaces;

namespace CXChange.Modules.UserAccess.Application.Queries.UserGet
{
    public record GetUser : IQueryBase
    {
        public string UserId { get; set; }
    }
}
