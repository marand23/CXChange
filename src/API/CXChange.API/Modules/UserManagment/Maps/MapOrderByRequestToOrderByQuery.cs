using CXChange.API.Modules.UserManagment.Requests.Filter;
using CXChange.BuildingBlocks.Application.Query.QueryInterfaces;
using CXChange.BuildingBlocks.Contract.Mapper;

namespace CXChange.API.Modules.UserManagment.Maps
{
    public class MapOrderByRequestToOrderByQuery : IMapper<OrderByRequest, OrderByQuery>
    {
        public OrderByQuery Map(OrderByRequest source, object extraData = null)
        {
            return new OrderByQuery
            {
                Field = source.Field,
                Dir = source.Dir,
            };
        }
    }
}
