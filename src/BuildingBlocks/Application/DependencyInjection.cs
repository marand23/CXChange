using CXChange.BuildingBlocks.Application.Query;
using Microsoft.Extensions.DependencyInjection;

namespace CXChange.BuildingBlocks.Application
{
    public static class DependencyInjection
    {
        public static void RegisterCoreElementstApplication(this IServiceCollection services)
        {
            services.AddTransient(typeof(QueryHandler<,>));

        }
    }
}
