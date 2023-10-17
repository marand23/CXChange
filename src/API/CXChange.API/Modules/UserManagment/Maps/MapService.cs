using CXChange.BuildingBlocks.Contract.Mapper;

namespace CXChange.API.Modules.UserManagment.Maps
{
    public class MapService : IMapperService
    {
        IServiceProvider _provide;
        public MapService(IServiceProvider provider)
        {

            _provide = provider;
        }

        public TDestination Map<TSource, TDestination>(TSource source, object extraData = null)
        {

            return _provide.GetRequiredService<IMapper<TSource, TDestination>>().Map(source, extraData);

        }


    }
}
