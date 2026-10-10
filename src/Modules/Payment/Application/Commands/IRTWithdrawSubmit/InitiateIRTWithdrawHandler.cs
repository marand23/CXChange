using CXChange.BuildingBlocks.Domain;
using CXChange.Modules.Payment.Domain.Interfaces;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace CXChange.Modules.Payment.Application.Commands.IRTWithdrawSubmit
{
    internal class SubmitIRTWithdrawHandler : IConsumer<SubmitIRTWithdraw>
    {
        IUserRepository _userRepository;
        IShabaRepository _shabaRepository;
        IWalletRepository _walletRepository;
        IWithdrawRepository _withdrawRepository;
        ICurrencyConfigRepository _currencyConfigRepository;
        IUnitOfWork _uow;
        public SubmitIRTWithdrawHandler(
            IUserRepository userRepository,
            IShabaRepository shabaRepository,
            IWalletRepository walletRepository,
            IWithdrawRepository withdrawRepository,
            ICurrencyConfigRepository currencyConfigRepository,
            IUnitOfWork uow
            )
        {
            _userRepository = userRepository;
            _shabaRepository = shabaRepository;
            _walletRepository = walletRepository;
            _withdrawRepository = withdrawRepository;
            _currencyConfigRepository = currencyConfigRepository;
            _uow = uow;
        }
        public async Task Consume(ConsumeContext<SubmitIRTWithdraw> context)
        {
            bool saved = false;
            while (!saved)
            {
                try
                {
                    var user = await _userRepository.FindById(context.Message.UserId);
                    ArgumentNullException.ThrowIfNull(nameof(user));
                    user.CheckUserWithdrawRequirements();
                    var wallet = await _walletRepository.GetWalletByUserIdAsync(user.Id, "IRT");
                    var withdraw = wallet.CreateIRTWithdraw(NewId.NextSequentialGuid(), user.Id, context.Message.Amount, new Domain.Currency.ValueObjects.Currency("IRT", 1M), context.Message.Shaba);
                    await _withdrawRepository.AddAsync(withdraw);
                    await _uow.CommitChangesAsync();
                    saved = true;
                    await context.RespondAsync(new IRTWithdrawSubmitted { });
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    await _uow.Clear();
                }
            }






        }
    }
}
