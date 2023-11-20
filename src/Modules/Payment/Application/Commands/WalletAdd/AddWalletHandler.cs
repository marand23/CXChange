using CXChange.BuildingBlocks.Domain;
using CXChange.Modules.Payment.Domain.Interfaces;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace CXChange.Modules.Payment.Application.Commands.WalletAdd
{
    internal class AddWalletHandler : IConsumer<AddWallet>
    {
        IWalletRepository WalletRepository;
        IUnitOfWork UnitOfWork;
        ICurrencyConfigRepository _currencyConfigRepository;

        public AddWalletHandler(IWalletRepository walletRepository, [FromKeyedServices("payment")] IUnitOfWork unitOfWork, ICurrencyConfigRepository currencyConfigRepository)
        {
            this.WalletRepository = walletRepository;
            this.UnitOfWork = unitOfWork;
            _currencyConfigRepository = currencyConfigRepository;
        }
        public async Task Consume(ConsumeContext<AddWallet> context)
        {
            var wallet = await WalletRepository.GetWalletAsync(context.Message.UserId, "");
            wallet.AddDepositAccount();
            await UnitOfWork.CommitChangesAsync();
            await context.RespondAsync(new WalletAdded { });

        }
    }
}
