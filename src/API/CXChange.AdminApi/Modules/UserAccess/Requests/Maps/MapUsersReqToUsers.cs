using CXChange.AdminAPI.Modules.UserAccess.Requests.User;
using CXChange.BuildingBlocks.Application.Query.QueryInterfaces;
using CXChange.BuildingBlocks.Contract.Mapper;
using CXChange.Modules.UserAccess.Administrator.Application.Queries.Users;

namespace CXChange.API.Modules.UserManagment.Maps
{

    public class MapUsersReqToUsers : IMapper<GetUsersRequest, GetUsers>
    {

        public MapUsersReqToUsers(IMapperService mapperService)
        {

            _mapperService = mapperService;
        }

        IMapperService _mapperService;

        public GetUsers Map(GetUsersRequest source, object extraData = default)
        {

            return new GetUsers
            {
                UserId = extraData.ToString(),
                Page = source.Page,
                PageSize = source.PageSize,
                Filters = MapFilterItemRequestToFilterItem(source.Filters),
                OrderBy = source.OrderBy != null ? source.OrderBy.Select(x => new OrderByQuery { Field = x.Field, Dir = x.Dir }).ToList() : null

            };
        }

        private UserFilters MapFilterItemRequestToFilterItem(FiltersRequst source)
        {
            return new UserFilters()
            {
                Email = _mapperService.Map<FilterItemRequest<string>, FilterItemQuery<string>>(source.Email),
                EmailConfirmed = _mapperService.Map<FilterItemRequest<bool>, FilterItemQuery<bool>>(source.EmailConfirmed),
                PhoneNumber = _mapperService.Map<FilterItemRequest<string>, FilterItemQuery<string>>(source.PhoneNumber),
                PhoneNumberConfirmed = _mapperService.Map<FilterItemRequest<bool>, FilterItemQuery<bool>>(source.PhoneNumberConfirmed),


            };
        }

    }


}



