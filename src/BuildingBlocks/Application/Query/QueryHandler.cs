using CXChange.BuildingBlocks.Application.Data;
using CXChange.BuildingBlocks.Application.Query.QueryInterfaces;
using Dapper;

namespace CXChange.BuildingBlocks.Application.Query
{
    public class QueryHandler<T, TResponse> where T : class
    {
        public SqlBuilder sql { get; private set; } = new SqlBuilder();
        private ISqlConnectionFactory _connection;
        public QueryHandler(ISqlConnectionFactory connection)
        {
            _connection = connection;
        }
        private async Task<SqlBuilder.Template> GeneratePagedSql(T query, SqlBuilder.Template sqlQuery)
        {
            if (query == null) throw new ArgumentNullException("query");

            IPagedQuery pagedQuery = (IPagedQuery)query;
            int offSet = (pagedQuery.page - 1) * pagedQuery.pageSize;
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@offset", offSet);
            parameters.Add("@pagesize", pagedQuery.pageSize);
            var q = $"{sqlQuery.RawSql}  OFFSET @offset rows FETCH NEXT @pagesize ROWS ONLY";
            var finalQuery = sql.AddTemplate(q, parameters);

            return finalQuery;


            throw new NotImplementedException();
        }
        public async Task<IEnumerable<TResponse>> ExecuteAsync(T query, string sqlQuery)
        {
            SqlBuilder.Template temp = null;
            if (query is IPagedQuery pagedQuery)
            {
                var order = sql.AddTemplate(sqlQuery + "order by id");

                temp = await GeneratePagedSql(query, order);

            }
            else
            {

            }
            var result = await _connection.GetOpenConnection().QueryAsync<TResponse>(temp.RawSql, temp.Parameters);
            return result;
        }
        public async Task<TResponse> FirstOrDefaultAsync(string sqlQuery)
        {

            return await _connection.GetOpenConnection().QueryFirstOrDefaultAsync<TResponse>(sqlQuery);
        }



    }
}

