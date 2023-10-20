namespace CXChange.AdminAPI.Modules.UserAccess.Requests.Filter
{
    public record OrderByRequest
    {
        public string Field { get; init; }
        public string Dir { get; init; }
    }
}
