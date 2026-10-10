
using CXChange.BuildingBlocks.Application.Query;
using CXChange.BuildingBlocks.Application.Totp;
using CXChange.BuildingBlocks.Contract.Mapper;
using CXChange.BuildingBlocks.Infrastructure.Data;
using CXChange.BuildingBlocks.Infrastructure.HttpClient;
using CXChange.BuildingBlocks.Infrastructure.Query;
using CXChange.BuildingBlocks.Infrastructure.Totp;
using Microsoft.Extensions.DependencyInjection;

namespace CXChange.BuildingBlocks.Infrastructure
{
    public static class DependencyInjection
    {
        public static void RegisterCoreElementstInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<ITotp, Totp.Totp>();
            services.AddScoped<IAuthenticator, Authenticator>();
            services.AddScoped<ISqlConnectionFactory, SqlConnectionFactory>();
            services.AddScoped(typeof(IQueryHandler<,>), typeof(QueryHandler<,>));
            services.AddSingleton<IMapService, MapService>();
            services.AddScoped<IExchangeHttpClient, ExchangeHttpClient>();


        }
    }
}
