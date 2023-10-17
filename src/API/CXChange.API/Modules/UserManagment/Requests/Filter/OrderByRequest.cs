namespace CXChange.API.Modules.UserManagment.Requests.Filter
{
    public record OrderByRequest
    {
        public string Field { get; init; }
        public string Dir { get; init; }
    }
}
