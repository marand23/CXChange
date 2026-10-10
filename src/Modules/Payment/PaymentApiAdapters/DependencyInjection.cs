using CXChange.Modules.Payment.Application.GateWayAdapter;
using CXChange.Modules.Payment.PaymentApiAdapters.GateWay.Jibit;
using CXChange.Modules.Payment.PaymentApiAdapters.GateWay.Zibal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CXChange.Modules.Payment.PaymentApiAdapters
{
    public static class DependencyInjection
    {
        public static void RegisterPaymentApiAdapters(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IGatewayAdapter, Jibit>();
            services.AddScoped<ZibalClient>();
            services.AddScoped<JibitClient>();
            services.AddHttpClient();
            services.AddOptions<ZibalGatewayURLs>().Bind(configuration.GetSection(Sections.ZibalGateway))
                .ValidateDataAnnotations()
                .ValidateOnStart();

            services.AddOptions<JibitGatewayURLs>().Bind(configuration.GetSection(Sections.JibitGateway))
                .ValidateDataAnnotations()
                .ValidateOnStart();


        }

    }
    internal class Sections
    {
        public const string ZibalGateway = "GatewayConfiguration:ZibalURLs";
        public const string JibitGateway = "GatewayConfiguration:JibitURLs";

    }

}
