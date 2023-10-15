using CXChange.API.Modules.UserManagment.Requests.User;
using CXChange.BuildingBlocks.Application.Query.QueryInterfaces;
using CXChange.BuildingBlocks.Contract.Mapper;
using CXChange.Modules.UserAccess.Application.Queries.Users;

namespace CXChange.API.Modules.UserManagment.Maps
{
    public class MapUsersReqToUsers : IMapper<GetUsersRequest, GetUsers>
    {

        public GetUsers Map(GetUsersRequest source)
        {
            return new GetUsers
            {
                Page = source.Page,
                PageSize = source.PageSize,
                Filters = MapFilterItemRequestToFilterItem(source.Filters),
            };
        }
        private UserFilters MapFilterItemRequestToFilterItem(Filtersreq source)
        {
            return new UserFilters
            {
                Email = MapFilterItemRequestToFilterItem(source.Email),
                EmailConfirmed = MapFilterItemRequestToFilterItem(source.EmailConfirmed),
                PhoneNumber = MapFilterItemRequestToFilterItem(source.PhoneNumber),
                PhoneNumberConfirmed = MapFilterItemRequestToFilterItem(source.PhoneNumberConfirmed),
                PhoneNumberRange = MapFilterItemRequestToFilterItem(source.PhoneNumberRange)

            };
        }
        private IFilterItem<Tsrcvalue> MapFilterItemRequestToFilterItem<Tsrcvalue>(IFilterItemReq<Tsrcvalue> source)
        {
            return new IFilterItem<Tsrcvalue>
            {
                Value = source.Value,
            };
        }
    }


}



