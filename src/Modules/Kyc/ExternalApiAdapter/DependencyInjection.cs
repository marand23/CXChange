using CXChange.Modules.Kyc.Application.ExternalApiServices;
using CXChange.Modules.Kyc.ExternalApiAdapter.Services.Clients;
using Microsoft.Extensions.DependencyInjection;

namespace CXChange.Modules.Kyc.ExternalApiAdapter
{
    public static class DependencyInjection
    {
        public static void RegisterExternalApiAdaper(this IServiceCollection services)
        {
            services.AddScoped<IKycPhotoActions, GoogleDriveActions>();
            services.AddScoped<IKycPhotoActions, CustomS3Actions>();
        }
    }
}
