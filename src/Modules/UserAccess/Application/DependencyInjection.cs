
using CXChange.BuildingBlocks.Application;
using CXChange.Modules.UserAccess.Application.Commands.UserRegistration;
using CXChange.Modules.UserAccess.Application.DomainEventHandlers;
using CXChange.Modules.UserAccess.Application.Services.Implementation;
using CXChange.Modules.UserAccess.Application.Services.Interfaces;
using MassTransit;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CXChange.Modules.UserAccess.Application
{
    public static class DependencyInjection
    {
        public static void RegisterUserManagmentApplication(this IServiceCollection services,IWebHostEnvironment environment,MediatRServiceConfiguration cfg)
        {
           


            // Register the command and its corresponding handler
           // services.AddScoped<IRequestHandler<CreateUserCommand>, CreateUserCommandHandler>();
          
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPasswordService, PasswordService>();
            services.AddScoped<LoginService, LoginService>();
            services.AddScoped<IUserRegistrationService,UserRegistertrationService>();
            cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());



        }
        public static void AddUserManagmentApplicationMediator(this IMediatorRegistrationConfigurator cfg)
        {
           

         

        }
        public static void AddUserManagmentApplicationBus(this IBusRegistrationConfigurator cfg)
        {

           
            cfg.AddConsumer<sendemailcosumer>();
        }


    }
}
