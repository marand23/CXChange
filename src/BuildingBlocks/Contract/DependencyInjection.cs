using CXChange.BuildingBlocks.Contract.Mapper;
using Microsoft.Extensions.DependencyInjection;

namespace CXChange.BuildingBlocks.Contract
{
    public static class DependencyInjection
    {
        public static void RegisterBuldingBlocksContract(this IServiceCollection services)
        {
            services.AddSingleton<IMapService, MapService>();
        }
    }
}
