using CXChange.Modules.Payment.Application.Commands.BankCardAdd;
using CXChange.Modules.Payment.Application.Commands.ShabaAdd;
using CXChange.Modules.Payment.Application.Commands.TomanDepositInitiate;
using CXChange.Modules.Payment.Application.Commands.TomanDepositVerify;
using CXChange.Modules.Payment.Application.Commands.WalletAdd;
using CXChange.Modules.Payment.Application.IntegrationEventConsumers;
using MassTransit;

namespace CXChange.Modules.Payment.Application
{
    public static class DependencyInjection
    {
        public static void RegisterPaymentApplicationMediator(this IMediatorRegistrationConfigurator cfg)
        {
            cfg.AddConsumer<SubmitBankCardHandler>();
            cfg.AddConsumer<SubmitShabaHandler>();
            cfg.AddConsumer<AddWalletHandler>();
            cfg.AddConsumer<InitiateTomanDepositHandler>();
            cfg.AddConsumer<VerifyTomanDepositHandler>();



        }
        public static void AddPaymentApplicationBus(this IBusRegistrationConfigurator cfg)
        {

            cfg.AddConsumer<UserRegisterConfirmedConsumer>();



        }
    }
}
