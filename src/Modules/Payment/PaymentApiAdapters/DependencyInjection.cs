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
            services.AddScoped<IGatewayAdapter, Zibal>();
            services.AddScoped<ZibalClient>();
            services.AddScoped<JibitClient>();
            services.AddHttpClient();
            services.Configure<ZibalGatewayURLs>(configuration.GetSection("GatewayConfiguration:ZibalURLs"));
            services.Configure<JibitGatewayURLs>(configuration.GetSection("GatewayConfiguration:JibitURLs"));

        }
    }
}
