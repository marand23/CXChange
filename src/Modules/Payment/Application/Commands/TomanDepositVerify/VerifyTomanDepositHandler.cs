using CXChange.BuildingBlocks.Domain;
using CXChange.Modules.Payment.Application.Commands.TomanDepositConfirm;
using CXChange.Modules.Payment.Domain.Interfaces;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace CXChange.Modules.Payment.Application.Commands.TomanDepositVerify
{
    internal class VerifyTomanDepositHandler : IConsumer<VerifyTomanDeposit>
    {
        IDepositRepository _depositRepository;
        IUserRepository _userRepository;
        IUnitOfWork _uow;
        IWalletRepository _walletRepository;
        public VerifyTomanDepositHandler(IDepositRepository depositRepository, IUserRepository userRepository, [FromKeyedServices("payment")] IUnitOfWork uow, IWalletRepository walletRepository)
        {
            _depositRepository = depositRepository;
            _userRepository = userRepository;
            _uow = uow;
            _walletRepository = walletRepository;
        }

        public async Task Consume(ConsumeContext<VerifyTomanDeposit> context)
        {
            var deposit = await _depositRepository.GetDepositById(context.Message.DepositId);
            var wallet = await _walletRepository.GetWalletAsync(deposit.UserId, "");
            wallet.ConfirmDeposit(deposit);
            await _uow.CommitChangesAsync();
            await context.RespondAsync(new TomanDepositVerified { });
        }
    }
}
