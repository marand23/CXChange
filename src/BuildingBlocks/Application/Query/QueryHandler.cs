using CXChange.BuildingBlocks.Application.Data;
using CXChange.BuildingBlocks.Application.Query.QueryInterfaces;
using Dapper;

namespace CXChange.BuildingBlocks.Application.Query
{
    public class QueryHandler<T, TResponse> where T : class
    {
        public SqlBuilder sql { get; private set; } = new SqlBuilder();
        private ISqlConnectionFactory _connection;
        readonly DynamicParameters parameters = new DynamicParameters();
        SqlBuilder.Template temp = null;

        public QueryHandler(ISqlConnectionFactory connection)
        {
            _connection = connection;
        }
        #region GenerateSql

        private void GeneratePagedSql(T query)
        {

            if (query == null) throw new ArgumentNullException("query");

            IPageQyery pagedQuery = (IPageQyery)query;
            int offSet = (pagedQuery.Page - 1) * pagedQuery.PageSize;

            parameters.Add("@offset", offSet);
            parameters.Add("@pagesize", pagedQuery.PageSize);
            temp = sql.AddTemplate($"{temp.RawSql}  /**where**/ /**orderby**/  Limit @offset , @pagesize");

            // temp = sql.AddTemplate(temp.RawSql + "/**where**/ /**orderby**/");
        }
        private void GenerateOrderableSql(T query)
        {
            if (query == null) throw new ArgumentNullException("query");
            IOrderableQuery orderdQuery = (IOrderableQuery)query;
            if (orderdQuery.OrderBy == null) { return; }
            foreach (var ob in orderdQuery.OrderBy)
            {
                sql.OrderBy($"@o_{orderdQuery.OrderBy.IndexOf(ob)} {ob.Dir}");
                parameters.Add($"@o_{orderdQuery.OrderBy.IndexOf(ob)}", ob.Field);
            }
        }
        private void GenerateFilteredSql(T query)
        {
            if (query == null) throw new ArgumentNullException("query");

            //IFilteredQuery<T> query1 = (IFilteredQuery<T>)query;
            var properties = query.GetType().GetProperties().ToList();
            foreach (var property in properties)
            {
                if (property.Name == "PhoneNumber")
                {
                    //  sql.Where($"{property.Name} = {property.GetValue(query)}");
                    sql.Where($"{property.Name}=@v_{properties.IndexOf(property)}");
                    parameters.Add($"@v_{properties.IndexOf(property)}", property.GetValue(query));
                }
                //var value = property.GetValue(query);

                //sql.Where($"{property.Name}=@v_{properties.IndexOf(property)}");
                //parameters.Add($"@v_{properties.IndexOf(property)}", property.GetValue(query));
            }
        }
        private void ConstructSql(T query, SqlBuilder.Template template)
        {

            if (query is IOrderableQuery)
            {
                GenerateOrderableSql(query);
            }
            if (query is IPageQyery)
            {
                GeneratePagedSql(query);
            }
        }

        #endregion
        public async Task<IEnumerable<TResponse>> ExecuteWithPaginationAndOrderingAsync(T query, SqlBuilder.Template template)
        {
            temp = template;
            ConstructSql(query, template);
            Console.WriteLine(temp.RawSql);
            var result = await _connection.GetOpenConnection().QueryAsync<TResponse>(temp.RawSql, parameters);
            return result;
        }
        public async Task<IEnumerable<TResponse>> ExecuteQueryAsync(SqlBuilder.Template template)
        {
            temp = template;
            var result = await _connection.GetOpenConnection().QueryAsync<TResponse>(temp.RawSql, parameters);
            return result;
        }
        public async Task<TResponse> FirstOrDefaultAsync(SqlBuilder.Template template)
        {
            temp = template;
            return await _connection.GetOpenConnection().QueryFirstOrDefaultAsync<TResponse>(template.RawSql, parameters);
        }




    }
}

