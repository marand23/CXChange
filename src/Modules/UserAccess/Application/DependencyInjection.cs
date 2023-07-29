using CXChange.BuildingBlocks.Application.MessageInterfaces;
using CXChange.Modules.UserAccess.Application.CommandFilters;
using CXChange.Modules.UserAccess.Application.Commands.UserRegistration;
using CXChange.Modules.UserAccess.Application.DomainEventHandlers;

using FluentValidation;
using MassTransit;
using MassTransit.Internals;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CXChange.Modules.UserAccess.Application
{
    public static class DependencyInjection
    {
        public static void RegisterUserAccessApplication(this IServiceCollection services, IWebHostEnvironment environment)
        {








            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());




        }
        public static void AddUserAccessApplicationMediator(this IMediatorRegistrationConfigurator cfg)
        {

            cfg.AddConsumer<RegisterUserHandler>();
            cfg.AddConsumer<UserRegisteredDomainEventHandler>();


            cfg.ConfigureMediator((context, mcfg) =>
            {

                mcfg.UseConsumeFilter(typeof(ValidationFilter<>), context, x =>
                {
                    x.Include(type => type.HasInterface<IValidatable>());

                });

                mcfg.UseConsumeFilter(typeof(UnitOfWorkFilter<>), context, x =>
                {
                    x.Include(type => type.HasInterface<IPersistable>());
                });



            });


        }
        public static void AddUserManagmentApplicationBus(this IBusRegistrationConfigurator cfg)
        {


            cfg.AddConsumer<UserRegisteredDomainEventHandler>();
        }


    }
}
