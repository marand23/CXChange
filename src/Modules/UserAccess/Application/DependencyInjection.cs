using CXChange.BuildingBlocks.Application.MessageInterfaces;
using CXChange.Modules.UserAccess.Application.CommandFilters;
using CXChange.Modules.UserAccess.Application.Commands.Authenticator.AuthenticatorConfirm;
using CXChange.Modules.UserAccess.Application.Commands.Authenticator.AuthenticatorInitiate;
using CXChange.Modules.UserAccess.Application.Commands.EmailActivate;
using CXChange.Modules.UserAccess.Application.Commands.Login.ConfirmLogin;
using CXChange.Modules.UserAccess.Application.Commands.Login.UserLogin;
using CXChange.Modules.UserAccess.Application.Commands.PasswordReset.UserResetPasswordCofirm;
using CXChange.Modules.UserAccess.Application.Commands.Register.UserRegisterConfirmation;
using CXChange.Modules.UserAccess.Application.Commands.Register.UserRegistration;
using CXChange.Modules.UserAccess.Application.Commands.UserDeletion;
using CXChange.Modules.UserAccess.Application.Commands.VerificationCode.CodeSendToAuthenticatedUser;
using CXChange.Modules.UserAccess.Application.Commands.VerificationCode.CodeSendToUnAuthenticatedUser;
using CXChange.Modules.UserAccess.Application.Commands.VerificationCode.CodeValidate;
using CXChange.Modules.UserAccess.Application.DomainEventHandlers.UserRegisteredDomainEventHandlers;
using CXChange.Modules.UserAccess.Application.DomainEventHandlers.UserRegistrationConfirmedDomainEventHandlers;
using CXChange.Modules.UserAccess.Application.Queries.Users;
using CXChange.Modules.UserAccess.Application.Services.Implementation;
using CXChange.Modules.UserAccess.Application.Services.Interfaces;
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
            services.AddScoped<IVerificationCodeFunctions, VerificationCodeFunctions>();



        }
        public static void AddUserAccessApplicationMediator(this IMediatorRegistrationConfigurator cfg)
        {


            cfg.AddConsumer<RegisterUserHandler>(c =>
            {
                c.UseMessageRetry(c =>
                {
                    c.Immediate(2);
                    c.Ignore(typeof(NotImplementedException));
                });


            });
            cfg.AddConsumer<SendCodeToAuthenticatedUserHandler>();
            cfg.AddConsumer<SendCodeToUnAuthenticatedUserHandler>();

            cfg.AddConsumer<SendVerificationSmsHandler>();
            cfg.AddConsumer<SendUserRegistrationConfirmedNotificationHandler>();
            cfg.AddConsumer<GetUsersQueryHandler>();
            cfg.AddConsumer<DeleteUserHandler>();
            cfg.AddConsumer<ConfirmUserRegistrationHandler>();
            cfg.AddConsumer<UserLoginCheckHandler>();
            cfg.AddConsumer<UserLoginConfirmHandler>();
            cfg.AddConsumer<ActivateEmailHandler>();
            cfg.AddConsumer<ResetUserPasswordHandler>();
            cfg.AddConsumer<InitiateAuthenticatorHandler>();
            cfg.AddConsumer<ValidateCodeHandler>();
            cfg.AddConsumer<ConfirmAuthenticatorHandler>();

            cfg.ConfigureMediator((context, mcfg) =>
            {
                mcfg.UseConsumeFilter(typeof(LoggingFilter<>), context);

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



        }


    }
}
