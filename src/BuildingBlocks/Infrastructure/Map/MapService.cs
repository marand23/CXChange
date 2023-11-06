using Microsoft.Extensions.DependencyInjection;

namespace CXChange.BuildingBlocks.Contract.Mapper
{
    internal class MapService : IMapService
    {
        IServiceProvider _provider;
        public MapService(IServiceProvider provider)
        {

            _provider = provider;
        }

        public TDestination Map<TSource, TDestination>(TSource source, object extraData = null)
        {

            return _provider.GetRequiredService<IMapper<TSource, TDestination>>().Map(source, extraData);

        }

    }
}
