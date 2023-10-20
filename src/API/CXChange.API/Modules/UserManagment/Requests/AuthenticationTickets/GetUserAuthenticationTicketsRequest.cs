namespace CXChange.API.Modules.UserManagment.Requests.AuthenticationTickets
{
    public class GetUserAuthenticationTicketsRequest
    {


        public int Page { get; init; }
        public int PageSize { get; init; }
        public string OrderBy { get; set; }
        public string Dir { get; set; }


    }
}
