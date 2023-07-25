
using CXChange.BuildingBlocks.Application;
using CXChange.Modules.UserAccess.Application.Commands.UserRegistration;
using CXChange.Modules.UserAccess.Application.DomainEventHandlers;

using FluentValidation;
using MassTransit;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CXChange.Modules.UserAccess.Application
{
    public static class DependencyInjection
    {
        public static void RegisterUserManagmentApplication(this IServiceCollection services,IWebHostEnvironment environment)
        {
           


            // Register the command and its corresponding handler
           // services.AddScoped<IRequestHandler<CreateUserCommand>, CreateUserCommandHandler>();
          
   
         
          
             services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

          


        }
        public static void AddUserManagmentApplicationMediator(this IMediatorRegistrationConfigurator cfg)
        {
           

         

        }
        public static void AddUserManagmentApplicationBus(this IBusRegistrationConfigurator cfg)
        {

           
            cfg.AddConsumer<UserRegisteredDomainEventHandler>();
        }


    }
}
