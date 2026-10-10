using CXChange.BuildingBlocks.Application.Query.QueryInterfaces;

namespace CXChange.BuildingBlocks.Application.Query
{
    public interface IQueryHandler<T, TResponse> where T : IQueryBase
    {
        void AddToParameters(string name, object value);
        Task<IEnumerable<TResponse>> ExecuteWithPaginationAndOrderingAsync(T query, string initialQuery);
        Task<IEnumerable<TResponse>> ExecuteQueryAsync(string query);
        Task<TResponse> FirstOrDefaultAsync(string query);
        void BuildFilterSql<TType>(FilterItemQuery<TType> query, string columnName);
    }
}
