using CXChange.Modules.UserAccess.Application.ApplicationEvents;
using CXChange.Modules.UserAccess.Application.Commands.Authenticator.AuthenticatorConfirm;
using CXChange.Modules.UserAccess.Application.Commands.Authenticator.AuthenticatorInitiate;
using CXChange.Modules.UserAccess.Application.Commands.EmailActivate;
using CXChange.Modules.UserAccess.Application.Commands.Login.ConfirmLogin;
using CXChange.Modules.UserAccess.Application.Commands.Login.UserLogin;
using CXChange.Modules.UserAccess.Application.Commands.PasswordReset.UserResetPasswordCofirm;
using CXChange.Modules.UserAccess.Application.Commands.PasswordReset.UserResetPasswordVerifCode;
using CXChange.Modules.UserAccess.Application.Commands.Register.UserRegisterConfirmation;
using CXChange.Modules.UserAccess.Application.Commands.Register.UserRegistration;
using CXChange.Modules.UserAccess.Application.Commands.UserDeletion;
using CXChange.Modules.UserAccess.Application.Commands.UserTwoFactorTypeChangeConfirm;
using CXChange.Modules.UserAccess.Application.Commands.VerificationCode.EmailCodeSend;
using CXChange.Modules.UserAccess.Application.Commands.VerificationCode.EmailCodeValidate;
using CXChange.Modules.UserAccess.Application.Commands.VerificationCode.PhoneNumberCodeSend;
using CXChange.Modules.UserAccess.Application.Commands.VerificationCode.PhoneNumberCodeValidate;
using CXChange.Modules.UserAccess.Application.DomainEventHandlers.UserPasswordResetDomainEventHandlers;
using CXChange.Modules.UserAccess.Application.DomainEventHandlers.UserRegisteredDomainEventHandlers;
using CXChange.Modules.UserAccess.Application.DomainEventHandlers.UserRegistrationConfirmedDomainEventHandlers;
using CXChange.Modules.UserAccess.Application.Queries.UserAuthenticationTickets;
using CXChange.Modules.UserAccess.Application.Queries.UserGet;
using CXChange.Modules.UserAccess.Application.Services.Implementation;
using CXChange.Modules.UserAccess.Application.Services.Interfaces;
using FluentValidation;
using MassTransit;
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
                //c.UseMessageRetry(c =>
                //{
                //    c.Immediate(2);
                //    c.Ignore(typeof(NotImplementedException));
                //});


            });
            cfg.AddConsumer<ResetUserPasswordHandler>(c =>
            {
                // c.UseDelayedRedelivery(r => r.Intervals(TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(2), TimeSpan.FromMinutes(3)));

                //c.UseMessageRetry(c =>
                //{
                //    c.Immediate(2);
                //});
            });
            cfg.AddConsumer<VerifyResetPasswordCodeHandler>();
            cfg.AddConsumer<SendCodeToPhoneNumberHandler>();
            cfg.AddConsumer<SendCodeToEmailHandler>();
            cfg.AddConsumer<SendCodeToPhoneNumberHandler>();
            cfg.AddConsumer<RevokeAuthenticationTicketsHandler>();
            cfg.AddConsumer<SendVerificationSmsHandler>();
            cfg.AddConsumer<SendUserRegistrationConfirmedNotificationHandler>();
            cfg.AddConsumer<DeleteUserHandler>();
            cfg.AddConsumer<ConfirmUserRegistrationHandler>();
            cfg.AddConsumer<UserLoginCheckHandler>();
            cfg.AddConsumer<UserLoginConfirmHandler>();
            cfg.AddConsumer<ActivateEmailHandler>();
            cfg.AddConsumer<ResetUserPasswordHandler>();
            cfg.AddConsumer<InitiateAuthenticatorHandler>();
            cfg.AddConsumer<ValidateEmailCodeHandler>();
            cfg.AddConsumer<ValidatePhoneNumberCodeHandler>();
            cfg.AddConsumer<ConfirmAuthenticatorHandler>();
            cfg.AddConsumer<ConfirmUserTwoFactorTypeChangeHandler>();
            cfg.AddConsumer<GetUserHandler>();
            cfg.AddConsumer<GetUserAuthenticationTicketsHandler>();
            cfg.AddConsumer<TotpCodeVerifiedEventHandler>();

            //cfg.ConfigureMediator((context, mcfg) =>
            //{
            //    mcfg.UseConsumeFilter(typeof(LoggingFilter<>), context);

            //    mcfg.UseConsumeFilter(typeof(ValidationFilter<>), context, x =>
            //    {
            //        x.Include(type => type.HasInterface<IValidatable>());

            //    });

            //    mcfg.UseConsumeFilter(typeof(UnitOfWorkFilter<>), context, x =>
            //    {
            //        x.Include(type => type.HasInterface<IPersistable>());
            //    });



            //});


        }
        public static void AddUserManagmentApplicationBus(this IBusRegistrationConfigurator cfg)
        {



        }


    }
}
