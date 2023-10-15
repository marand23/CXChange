namespace CXChange.API.Modules.UserManagment.Requests.Filter
{
    public class DataFilterRequest
    {
        public List<FilterItemReq> Filters { get; set; }
        // public List<OrderingReq> OrderBy { get; set; }

        public List<RangedFilterRequest> RangedFilters { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }


    }
    public class FilterItemReq
    {
        public string Field { get; set; }
        public string Value { get; set; }

    }
    public class OrderingReq
    {
        public string Field { get; set; }
        public string Dir { get; set; }
    }
    public class RangedFilterRequest
    {
        public string Id { get; set; }
        public string StartValue { get; set; }
        public string EndValue { get; set; }
    }


}
