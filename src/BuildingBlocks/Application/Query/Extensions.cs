namespace CXChange.BuildingBlocks.Application.Query
{
    public static class StringExtensions
    {


        public static string AddInitialQuery(this string query, string initialQuery, bool isFilterQuery)
        {
            if (query == null) { throw new ArgumentNullException("query"); }
            if (initialQuery == null) { throw new ArgumentNullException("initialQuery"); }
            if (initialQuery.Contains(SqlKeyWords.Where.Trim(), StringComparison.OrdinalIgnoreCase) && isFilterQuery)
            {
                return initialQuery + SqlKeyWords.And + query;
            }

            else if (!initialQuery.Contains(SqlKeyWords.Where.Trim(), StringComparison.OrdinalIgnoreCase) && isFilterQuery)
            {
                return initialQuery + SqlKeyWords.Where + query;
            }
            else
            {
                return initialQuery;
            }
        }
        public static string SetInitialQuery(this string query)
        {
            return $"{query}\n";
        }

    }
}
