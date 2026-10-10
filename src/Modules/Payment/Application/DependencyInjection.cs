using CXChange.Modules.Payment.Application.Commands.BankCardAdd;
using CXChange.Modules.Payment.Application.Commands.IRTDepositReject;
using CXChange.Modules.Payment.Application.Commands.IRTDepositSubmit;
using CXChange.Modules.Payment.Application.Commands.ShabaAdd;
using CXChange.Modules.Payment.Application.Commands.TomanDepositVerify;
using CXChange.Modules.Payment.Application.EventConsumers.Integration;
using CXChange.Modules.Payment.Application.SchedulerJobs;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace CXChange.Modules.Payment.Application
{
    public static class DependencyInjection
    {
        public static void RegisterPaymentApplication(this IServiceCollection services)
        {
            services.AddHostedService<SendIRTWithdrawJob>();
        }
        public static void RegisterPaymentApplicationMediator(this IMediatorRegistrationConfigurator cfg)
        {
            cfg.AddConsumer<SubmitBankCardHandler>();
            cfg.AddConsumer<SubmitShabaHandler>();
            cfg.AddConsumer<SubmitIRTDepositHandler>(x =>
            {

            });
            cfg.AddConsumer<VerifyIRTDepositHandler>(x =>
            {

            });
            cfg.AddConsumer<RejectIRTDepositHandler>();



        }
        public static void AddPaymentApplicationBus(this IBusRegistrationConfigurator cfg)
        {

            cfg.AddConsumer<UserRegisterConfirmedConsumer>();



        }
    }
}
