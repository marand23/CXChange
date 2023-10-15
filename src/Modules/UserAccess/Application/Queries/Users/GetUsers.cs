using CXChange.BuildingBlocks.Application.Query.QueryInterfaces;

namespace CXChange.Modules.UserAccess.Application.Queries.Users
{
    public record GetUsers : IQueryBase, /*IPageQyery,*/  IFilteredQuery<UserFilters>/*, IOrderableQuery,*/
    {


        public int Page { get; set; }

        public int PageSize { get; set; }

        public UserFilters Filters { get; set; }
    }
    public class UserFilters
    {
        //public UserFilters(IFilterItem<string> phonenumber, IFilterItem<string> phoneNumberRange, IFilterItem<string> email, IFilterItem<bool> phoneNumberConfirmed)
        //{
        //    PhoneNumber = phonenumber;
        //    PhoneNumberRange = phoneNumberRange;
        //    Email = email;
        //    PhoneNumberConfirmed = phoneNumberConfirmed;
        //    PhoneNumberRange.OperationType = OperationType.Between;
        //    // EmailConfirmed = new IFilterItem<bool>(OperationType.Equal);
        //}
        public IFilterItem<string> PhoneNumber { get; set; }
        public IFilterItem<string> PhoneNumberRange { get; set; }
        public IFilterItem<string> Email { get; set; }
        public IFilterItem<bool> PhoneNumberConfirmed { get; set; }
        public IFilterItem<bool> EmailConfirmed { get; set; }


    }


}
