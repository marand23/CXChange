using CXChange.AdminAPI.Modules.UserAccess.Requests.Filter;

namespace CXChange.AdminAPI.Modules.Kyc.Requests
{
    public class KycRequests
    {

        public int Page { get; set; }
        public int PageSize { get; set; }
        public KycFiltersRequestReq? Filters { get; set; }
        public List<OrderByRequest>? OrderBy { get; set; }



    }
    public class KycFiltersRequestReq
    {

        public FilterItemRequest<string>? UserId { get; set; }
        public FilterItemRequest<string>? BirthDate { get; set; }
        public FilterItemRequest<string>? NationalId { get; set; }
        public FilterItemRequest<string>? Name { get; set; }
        public FilterItemRequest<string>? SurName { get; set; }

    }

}

