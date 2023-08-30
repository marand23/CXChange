using CXChange.BuildingBlocks.Application.Data;
using CXChange.BuildingBlocks.Application.Totp;
using CXChange.BuildingBlocks.Infrastructure.Totp;
using Microsoft.Extensions.DependencyInjection;

namespace CXChange.CoreElements.Infrastructure
{
    public static class DependencyInjection
    {
        public static void RegisterCoreElementstInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<ITotp, Totp.Totp>();
            services.AddScoped<IAuthenticator, Authenticator>();
            services.AddScoped<ISqlConnectionFactory, SqlConnectionFactory>();

        }
    }
}
