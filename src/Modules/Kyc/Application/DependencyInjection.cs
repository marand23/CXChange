using CXChange.Modules.Kyc.Application.Commands.Kyc;
using CXChange.Modules.Kyc.Application.IntegrationEventsConsumers;
using FluentValidation;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
namespace CXChange.Modules.Kyc.Application
{
    public static class DependencyInjection
    {
        public static void RegisterKycApplication(this IServiceCollection services)
        {

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        }
        public static void AddKycApplicationMediator(this IMediatorRegistrationConfigurator cfg)
        {
            cfg.AddConsumer<InitiateKycHandler>();

        }
        public static void AddKycApplicationBus(this IBusRegistrationConfigurator cfg)
        {

            cfg.AddConsumer<UserRegisterConfirmedConsumer>();


        }


    }
}
