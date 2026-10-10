using CXChange.BuildingBlocks.Domain;
using CXChange.Modules.Payment.Application.GateWayAdapter;
using CXChange.Modules.Payment.Domain.Interfaces;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace CXChange.Modules.Payment.Application.Commands.IRTDepositReject
{
    internal class RejectIRTDepositHandler : IConsumer<RejectIRTDeposit>
    {
        IDepositRepository _depositRepository;
        IUnitOfWork _uow;
        IGatewayAdapter _gatewayAdapter;
        ILogger _logger;
        ICurrencyConfigRepository _currencyConfigRepository;
        public RejectIRTDepositHandler(IDepositRepository depositRepository, [FromKeyedServices("payment")] IUnitOfWork uow, IGatewayAdapter gatewayAdapter, ILogger logger, ICurrencyConfigRepository currencyConfigRepository)
        {
            _depositRepository = depositRepository;
            _uow = uow;
            _gatewayAdapter = gatewayAdapter;
            _logger = logger;
            _currencyConfigRepository = currencyConfigRepository;
        }
        public async Task Consume(ConsumeContext<RejectIRTDeposit> context)
        {
            var currecy = await _currencyConfigRepository.GetCurrencyConfigBySymbolAsync("ETH");
            //currecy.AddNetwork("eth", "erc20", 0.002M, true, true, 10, "regex", "memo", 0.0005M, 0.0002M);
            //if (context.Message.Error == PurchaseErrorEnum.Unkown)
            //{
            //    //here we should inquiry first to know the exact reason and the return it 
            //    //to the api layer
            //}

            //var deposit = await _depositRepository.GetDepositById(context.Message.DepositId);
            //ArgumentNullException.ThrowIfNull(deposit, "deposit");
            //_logger.Error("purchase failed for trackId: {TrackId} DepositId:{DepositId} reason:{Error}", context.Message.TrackId, context.Message.DepositId, true);
            //deposit.RejectIRTDeposit(context.Message.Error.ToString());
            await _uow.CommitChangesAsync();
            await context.RespondAsync(new IRTDepositRejected
            {
                Error = context.Message.Error,

            });


        }
    }
}
