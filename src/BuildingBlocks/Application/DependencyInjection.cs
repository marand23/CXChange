using CXChange.BuildingBlocks.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace CXChange.BuildingBlocks.Application
{
    public static class DependencyInjection
    {
        public static void RegisterCoreElementstApplication(this IServiceCollection services)
        {
            services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();

        }
    }
}
