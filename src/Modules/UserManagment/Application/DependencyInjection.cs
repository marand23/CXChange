using CXChange.Modules.UserManagment.Application.Services.Implementation;
using CXChange.Modules.UserManagment.Application.Services.Interfaces;
using CXChange.Modules.UserManagment.Application.User;
using CXChange.Modules.UserManagment.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CXChange.Modules.UserManagment.Application
{
    public static class DependencyInjection
    {
        public static void RegisterUserManagmentApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));


            // Register the command and its corresponding handler
           // services.AddScoped<IRequestHandler<CreateUserCommand>, CreateUserCommandHandler>();
            services.AddScoped<ISendCommand, SendCommand>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPasswordService, PasswordService>();
            services.AddScoped<LoginService, LoginService>();
            services.AddScoped<IUserRegistrationService,UserRegistertrationService>();
        }
      
    }
}
