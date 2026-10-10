using CXChange.BuildingBlocks.Domain;
using CXChange.Modules.Payment.Application.Commands.TomanDepositConfirm;
using CXChange.Modules.Payment.Application.GateWayAdapter;
using CXChange.Modules.Payment.Domain.Interfaces;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CXChange.Modules.Payment.Application.Commands.TomanDepositVerify
{
    internal class VerifyIRTDepositHandler : IConsumer<VerifyIRTDeposit>
    {
        IDepositRepository _depositRepository;
        IUserRepository _userRepository;
        IUnitOfWork _uow;
        IWalletRepository _walletRepository;
        IGatewayAdapter _gatewayAdapter;
        public VerifyIRTDepositHandler(IDepositRepository depositRepository, IUserRepository userRepository, [FromKeyedServices("payment")] IUnitOfWork uow, IWalletRepository walletRepository, IGatewayAdapter gatewayAdapter)
        {
            _depositRepository = depositRepository;
            _userRepository = userRepository;
            _uow = uow;
            _walletRepository = walletRepository;
            _gatewayAdapter = gatewayAdapter;
        }

        public async Task Consume(ConsumeContext<VerifyIRTDeposit> context)
        {
            var gateResult = await _gatewayAdapter.Verify(new GateWayAdapter.Verify.GateWayVerifyRequest
            {
                TrackId = context.Message.TrackId,
            });
            var saved = false;
            while (!saved)
            {
                try
                {
                    var deposit = await _depositRepository.GetDepositById(context.Message.DepositId);
                    var wallet = await _walletRepository.GetWalletById(deposit.WalletId, "IRT");
                    wallet.ConfirmDeposit(deposit);
                    var result = await _gatewayAdapter.Verify(new GateWayAdapter.Verify.GateWayVerifyRequest
                    {
                        TrackId = deposit.TrackId.TrackId
                    });
                    await _uow.CommitChangesAsync();
                    saved = true;
                    await context.RespondAsync(new IRTDepositVerified { });
                }
                catch (DbUpdateConcurrencyException)
                {
                    await _uow.Clear();
                }
            }




        }
    }
}
