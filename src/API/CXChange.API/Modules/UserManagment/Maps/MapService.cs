using CXChange.BuildingBlocks.Contract.Mapper;

namespace CXChange.API.Modules.UserManagment.Maps
{
    public class MapService : IMapperService
    {

        IServiceProvider _services;
        public MapService(IServiceProvider services)
        {

            _services = services;
        }

        public TDestination Map<TSource, TDestination>(TSource source)
        {
            return _services.GetRequiredService<IMapper<TSource, TDestination>>().Map(source);
        }
    }
}
