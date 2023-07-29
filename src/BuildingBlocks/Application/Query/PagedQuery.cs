using Dapper;

namespace CXChange.BuildingBlocks.Application.Query

{
    public class PagedQuery
    {


        public static SqlBuilder.Template ToSqlQuery(IPagedQuery query, string sqlQuery)
        {
            var builder = new SqlBuilder();
            int offSet = (query.page - 1) * query.pageSize;
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@offset", offSet);
            parameters.Add("@pagesize", query.pageSize);
            var q = $"{sqlQuery}  OFFSET @offset rows FETCH NEXT @pagesize ROWS ONLY";
            var finalQuery = builder.AddTemplate(q, parameters);

            return finalQuery;
        }
    }
}
