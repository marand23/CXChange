
using CXChange.Modules.UserAccess.Application.Services.Implementation;
using CXChange.Modules.UserAccess.Application.Services.Interfaces;
using MassTransit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CXChange.Modules.UserAccess.Application
{
    public static class DependencyInjection
    {
        public static void RegisterUserManagmentApplication(this IServiceCollection services,IWebHostEnvironment environment)
        {
           


            // Register the command and its corresponding handler
           // services.AddScoped<IRequestHandler<CreateUserCommand>, CreateUserCommandHandler>();
          
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPasswordService, PasswordService>();
            services.AddScoped<LoginService, LoginService>();
            services.AddScoped<IUserRegistrationService,UserRegistertrationService>();
            AddMAssTransit(services, environment);

        }
        private static void AddMAssTransit(IServiceCollection services,IWebHostEnvironment environment)
        {
           

        }


    }
}
