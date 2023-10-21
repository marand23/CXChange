
using CXChange.BuildingBlocks.Application.Query;
using CXChange.BuildingBlocks.Application.Query.QueryInterfaces;
using CXChange.BuildingBlocks.Infrastructure.Data;
using Dapper;

namespace CXChange.BuildingBlocks.Infrastructure.Query
{
    internal class QueryHandler<T, TResponse> : IQueryHandler<T, TResponse> where T : IQueryBase
    {
        private ISqlConnectionFactory _connection;
        readonly DynamicParameters parameters = new DynamicParameters();
        List<string> filterClause = new List<string>();
        string finalSql = string.Empty;
        private int paramCounter = 0;

        public QueryHandler(ISqlConnectionFactory connection)
        {
            _connection = connection;
        }
        #region GenerateSql
        public void AddToParameters(string name, object value)
        {
            parameters.Add(name, value);
        }
        private void GeneratePagedSql(T query)
        {

            if (query == null) throw new ArgumentNullException("query");

            IPageQyery pagedQuery = (IPageQyery)query;
            int offSet = (pagedQuery.Page - 1) * pagedQuery.PageSize;
            finalSql += $" {SqlKeyWords.Limit} @pagesize offset @offset";
            AddToParameters("@offset", offSet);
            AddToParameters("@pagesize", pagedQuery.PageSize);

        }
        private void GenerateOrderableSql(T query)
        {

            paramCounter++;
            if (query == null) throw new ArgumentNullException("query");
            IOrderableQuery orderdQuery = (IOrderableQuery)query;
            if (orderdQuery.OrderBy == null) { return; }
            finalSql += SqlKeyWords.OrderBy;
            List<string> orClauses = new List<string>();

            foreach (var ob in orderdQuery.OrderBy)
            {
                orClauses.Add($"@o_{paramCounter} {ob.Dir}");
                AddToParameters($"@o_{paramCounter}", ob.Field);
                paramCounter++;
            }
            finalSql += string.Join(" , ", orClauses);
        }
        public void BuildFilterSql<TType>(FilterItemQuery<TType> query, string columnName)
        {
            //this is not a full feautered filter method
            //it wil be used in simple filter situations
            //like select * from table where [conditions] order by limit page,offset




            string paramPrefix = "value_";
            string paramName = paramPrefix + paramCounter;
            paramCounter++;

            switch (query.FilterType)
            {

                case FilterType.Like:
                    filterClause.Add($"{columnName.ToSnakeCase()} {SqlKeyWords.Like} @{paramName}");
                    AddToParameters($"@{paramName}", query.Value[0]);
                    break;
                case FilterType.Equal:
                    filterClause.Add($"{columnName.ToSnakeCase()}=@{paramName}");
                    AddToParameters($"@{paramName}", query.Value[0]);
                    break;
                case FilterType.ContinousRange:
                    filterClause.Add($"{columnName.ToSnakeCase()} {SqlKeyWords.Between} @{paramName}_min {SqlKeyWords.And} @{paramName}_max");
                    AddToParameters($"{paramName}_min", query.Value[0]);
                    AddToParameters($"{paramName}_max", query.Value[1]);
                    break;
                case FilterType.DiscreteRange:
                    List<string> orClauses = new List<string>();
                    int index = 0;
                    foreach (var v in query.Value)
                    {
                        orClauses.Add($"{columnName.ToSnakeCase()}=@{paramName + "_" + index}");
                        AddToParameters($"@{paramName + "_" + index}", query.Value[index]);
                        index++;
                    }
                    filterClause.Add($"({string.Join($" {SqlKeyWords.Or} ", orClauses)})");

                    break;
                case FilterType.NotEqual:
                    filterClause.Add($"{columnName.ToSnakeCase()}<>@{paramName}");
                    AddToParameters($"@{paramName}", query.Value[0]);
                    break;


            }


        }

        private void ConstructPageAndOrderSql(T query)
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
        public async Task<IEnumerable<TResponse>> ExecuteWithPaginationAndOrderingAsync(T query, string initialQuery)
        {


            if (filterClause.Count > 0)
            {
                finalSql += string.Join(SqlKeyWords.And, filterClause);
            }
            finalSql = finalSql.AddInitialQuery(initialQuery.SetInitialQuery(), filterClause.Count > 0);
            ConstructPageAndOrderSql(query);
            Console.WriteLine(finalSql);
            DefaultTypeMap.MatchNamesWithUnderscores = true;


            var result = await _connection.GetOpenConnection().QueryAsync<TResponse>(finalSql, parameters);
            return result;
        }
        public async Task<IEnumerable<TResponse>> ExecuteQueryAsync(string query)
        {
            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await _connection.GetOpenConnection().QueryAsync<TResponse>(query, parameters);
            return result;
        }
        public async Task<TResponse> FirstOrDefaultAsync(string query)
        {
            DefaultTypeMap.MatchNamesWithUnderscores = true;

            return await _connection.GetOpenConnection().QueryFirstOrDefaultAsync<TResponse>(query, parameters);
        }




    }
}

