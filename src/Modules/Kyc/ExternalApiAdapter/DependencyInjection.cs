using CXChange.Modules.Kyc.Domain.Interfaces;
using CXChange.Modules.Kyc.ExternalApiAdapter.Services.Clients;
using Microsoft.Extensions.DependencyInjection;

namespace CXChange.Modules.Kyc.ExternalApiAdapter
{
    public static class DependencyInjection
    {
        public static void RegisterExternalApiAdaper(this IServiceCollection services)
        {
            services.AddScoped<IkycPhotoActions, GoogleDriveActions>();
            services.AddScoped<IkycPhotoActions, CustomS3Actions>();
        }
    }
}
