using CXChange.BuildingBlocks.Application;
using CXChange.BuildingBlocks.Application.Exceptions;
using CXChange.BuildingBlocks.Domain;
using CXChange.Modules.Payment.Application.GateWayAdapter;
using CXChange.Modules.Payment.Domain.Interfaces;
using CXChange.Modules.Payment.Domain.Wallet;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace CXChange.Modules.Payment.Application.Commands.IRTDepositSubmit
{
    internal class SubmitIRTDepositHandler : IConsumer<SubmitIRTDeposit>
    {
        IBankCardRepository _bankCardRepository;
        IUserRepository _userRepository;
        ICurrencyConfigRepository _currencyConfigRepository;
        IWalletRepository _walletRepository;
        IUnitOfWork _uow;
        IDepositRepository _depositRepository;
        IGatewayAdapter _gatewayAdapter;
        public SubmitIRTDepositHandler(
            IBankCardRepository bankCardRepository,
            IUserRepository userRepository,
            ICurrencyConfigRepository currencyConfigRepository,
            IWalletRepository walletRepository,
            IDepositRepository depositRepository,
            IGatewayAdapter gatewayAdapter,
            [FromKeyedServices("payment")] IUnitOfWork uow)
        {
            _bankCardRepository = bankCardRepository;
            _userRepository = userRepository;
            _currencyConfigRepository = currencyConfigRepository;
            _walletRepository = walletRepository;
            _depositRepository = depositRepository;
            _uow = uow;
            _gatewayAdapter = gatewayAdapter;
        }

        public async Task Consume(ConsumeContext<SubmitIRTDeposit> context)
        {


            var user = await _userRepository.FindById(context.Message.UserId);
            var bankCard = await _bankCardRepository.GetUserBankCardByCardNumber(context.Message.CardNumber, user.Id);

            if (bankCard == null)
            {
                throw new ExchangeCommandException(CommandExceptionEnum.BankCardNotFound, $"bank card not found user:{user.Id},cardNumber{context.Message.CardNumber}");
            }
            user.CheckUserDepositRequirements();
            var currency = await _currencyConfigRepository.GetCurrencyConfigBySymbolAsync("IRT");
            var wallet = await _walletRepository.GetWalletByUserIdAsync(context.Message.UserId, currency.Currency.Symbol);


            if (wallet == null)
            {
                wallet = Wallet.Create(NewId.NextSequentialGuid(), user.Id, currency.Id, 0, 0, currency.Currency.Symbol, currency.Currency.Precision);
                await _walletRepository.AddAsync(wallet);
            }
            var deposit = wallet.CreateDeposit(NewId.NextSequentialGuid(), user.Id, context.Message.Amount, currency.Currency, context.Message.CardNumber);
            await _depositRepository.AddAsync(deposit);
            var gateResponse = await _gatewayAdapter.CreatePurchase(new GateWayAdapter.Purchase.GateWayPurchaseRequest
            {
                Amount = deposit.Amount.Value,
                Currency = "IRT",
                CardNumber = deposit.Address.Address,
                DepositId = deposit.Id.ToString(),
                Desciption = "",
                PhoneNumber = user.PhoneNumber.PhoneNumber

            });
            if (!gateResponse.Success)
            {
                await context.RespondAsync(gateResponse);
                return;
            }
            await _uow.CommitChangesAsync();
            await context.RespondAsync(Result.OK(new IRTDepositSubmitted { PayLink = gateResponse.Data.PayLink }));

        }
    }
}

