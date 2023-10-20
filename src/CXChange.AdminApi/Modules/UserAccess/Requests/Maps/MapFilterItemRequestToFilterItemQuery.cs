using CXChange.BuildingBlocks.Application.Query.QueryInterfaces;
using CXChange.BuildingBlocks.Contract.Mapper;

namespace CXChange.AdminAPI.Modules.UserAccess.Maps
{
    public class MapFilterItemRequestToFilterItemQuery<T> : IMapper<FilterItemRequest<T>, FilterItemQuery<T>>
    {


        public FilterItemQuery<T> Map(FilterItemRequest<T> source, object extraData = null)
        {

            return new FilterItemQuery<T>(source.Value, source.FilterType);
        }


    }

}
