using CXChange.API.Modules.UserManagment.Requests.Filter;
using CXChange.BuildingBlocks.Application.Query.QueryInterfaces;
using CXChange.BuildingBlocks.Contract.Mapper;

namespace CXChange.API.Modules.UserManagment.Maps
{
    public class MapFilterItemRequestToFilterItemQuery<T> : IMapper<FilterItemRequest<T>, FilterItemQuery<T>>
    {
        public FilterItemQuery<T> Map(FilterItemRequest<T> source, object extraData = null)
        {
            OperationType operation = (extraData == null) ? OperationType.Equal : (OperationType)extraData;
            return new FilterItemQuery<T>(source.Value, operation);
        }


    }
}
