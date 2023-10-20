using CXChange.AdminAPI.Modules.UserAccess.Requests.Filter;
using CXChange.BuildingBlocks.Application.Query.QueryInterfaces;
using CXChange.BuildingBlocks.Contract.Mapper;

namespace CXChange.AdminAPI.Modules.UserAccess.Maps
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
