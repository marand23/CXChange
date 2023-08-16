using CXChange.BuildingBlocks.Application.MessageInterfaces;
using CXChange.Modules.UserAccess.Application.CommandFilters;
using CXChange.Modules.UserAccess.Application.Commands.ConfirmLogin;
using CXChange.Modules.UserAccess.Application.Commands.UserDeletion;
using CXChange.Modules.UserAccess.Application.Commands.UserLogin;
using CXChange.Modules.UserAccess.Application.Commands.UserRegisterConfirmation;
using CXChange.Modules.UserAccess.Application.Commands.UserRegistration;
using CXChange.Modules.UserAccess.Application.DomainEventHandlers;
using CXChange.Modules.UserAccess.Application.Queries.Users;
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
            cfg.AddConsumer<GetUsersQueryHandler>();
            cfg.AddConsumer<DeleteUserHandler>();
            cfg.AddConsumer<ConfirmUserRegistrationHandler>();
            cfg.AddConsumer<UserLoginCheckHandler>();
            cfg.AddConsumer<UserLoginConfirmHandler>();
            cfg.ConfigureMediator((context, mcfg) =>
            {
                //mcfg.UseConsumeFilter(typeof(LoggingFilter<>), context);

                mcfg.UseConsumeFilter(typeof(ValidationFilter<>), context, x =>
                {
                    x.Include(type => type.HasInterface<IValidatable>());

                });

                //mcfg.UseConsumeFilter(typeof(UnitOfWorkFilter<>), context, x =>
                //{
                //    x.Include(type => type.HasInterface<IPersistable>());
                //});



            });


        }
        public static void AddUserManagmentApplicationBus(this IBusRegistrationConfigurator cfg)
        {


            cfg.AddConsumer<UserRegisteredDomainEventHandler>();
        }


    }
}
