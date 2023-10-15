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
            int offSet = (pagedQuery.page - 1) * pagedQuery.pageSize;

            parameters.Add("@offset", offSet);
            parameters.Add("@pagesize", pagedQuery.pageSize);
            temp = sql.AddTemplate($"{temp.RawSql}  Limit @offset , @pagesize");
        }
        private void GenerateOrderableSql(T query)
        {
            if (query == null) throw new ArgumentNullException("query");
            IOrderableQuery orderdQuery = (IOrderableQuery)query;
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
        private SqlBuilder.Template ConstructSql(T query, string sqlQuery)
        {

            //if (query is IFilteredQuery<T>)
            //{
            GenerateFilteredSql(query);
            //}
            //GenerateRangedFilterSql(query);

            //if (query is IOrderableQuery)
            //{
            //    GenerateOrderableSql(query);
            //}
            //if (query is IPageQyery)
            //{
            //    GeneratePagedSql(query);
            //}

            temp = sql.AddTemplate(sqlQuery + "/**where**/ /**orderby**/");


            return temp;
        }

        #endregion
        public async Task<IEnumerable<TResponse>> ExecuteAsync(T query, string sqlQuery)
        {

            ConstructSql(query, sqlQuery);
            Console.WriteLine(temp.RawSql);
            var result = await _connection.GetOpenConnection().QueryAsync<TResponse>(temp.RawSql, parameters);
            return result;
        }
        public async Task<TResponse> FirstOrDefaultAsync(T query, string sqlQuery)
        {


            ConstructSql(query, sqlQuery);
            return await _connection.GetOpenConnection().QueryFirstOrDefaultAsync<TResponse>(temp.RawSql, parameters);
        }




    }
}

