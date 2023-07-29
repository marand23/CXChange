using CXChange.BuildingBlocks.Application.Totp;
using Microsoft.Extensions.DependencyInjection;

namespace CXChange.CoreElements.Infrastructure
{
    public static class DependencyInjection
    {
        public static void RegisterCoreElementstInfrastructure(this IServiceCollection servicesn)
        {
            servicesn.AddScoped<ITotp, Totp.Totp>();

        }
    }
}
