namespace CXChange.API.Modules.UserManagment.Requests.User
{
    public class GetUsersRequest
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public Filtersreq Filters { get; set; }
        public string PhoneNumber { get; set; }
    }

}
public class Filtersreq
{


    public IFilterItemReq<string> PhoneNumber { get; set; }
    public IFilterItemReq<string> PhoneNumberRange { get; set; }


    public IFilterItemReq<string> Email { get; set; }


    public IFilterItemReq<bool> PhoneNumberConfirmed { get; set; }


    public IFilterItemReq<bool> EmailConfirmed { get; set; }

}
public class IFilterItemReq<T>
{

    public T Value { get; set; }

}


