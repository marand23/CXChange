using CXChange.Modules.Payment.Application.GateWayAdapter;
using CXChange.Modules.Payment.Domain.Interfaces;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace CXChange.Modules.Payment.Application.SchedulerJobs
{
    internal class SendIRTWithdrawJob : BackgroundService
    {
        IWithdrawRepository _withdrawRepo;
        IWalletRepository _walletRepo;
        IGatewayAdapter _gatewayAdapter;
        ILogger _logger;
        //IUnitOfWork _unitOfWork;
        public SendIRTWithdrawJob(/*IWithdrawRepository withdrawRepo, IWalletRepository walletRepo, IGatewayAdapter gatewayAdapter, ILogger logger, IUnitOfWork unitOfWork*/)
        {
            //_withdrawRepo = withdrawRepo;
            //_walletRepo = walletRepo;
            //_gatewayAdapter = gatewayAdapter;
            //_logger = logger;
            //_unitOfWork = unitOfWork;
        }
        public async Task SendWithdraw()
        {

        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                Console.WriteLine("background service running");
                //var withdrawToBeSent = await _withdrawRepo.GetWithdrawsVerifiedByUser("IRT");
                //if (withdrawToBeSent == null)
                //{
                //    return;
                //}
                //foreach (Withdraw withdraw in withdrawToBeSent)
                //{
                //    try
                //    {
                //        var wallet = await _walletRepo.GetWalletByUserIdAsync(withdraw.Id, withdraw.Currency.Symbol);
                //        if (wallet == null)
                //        {
                //            _logger.Error("wallet is null for withdraw in SendIRTWithdrawJob,withdrawId :{0} , UserId:{1}", withdraw.Id, withdraw.UserId);
                //            continue;
                //        }
                //        var sendWithdrawReponse = await _gatewayAdapter.SendWithdraw(new SendWithdrawRequest
                //        {
                //            WithdrawId = withdraw.Id,
                //        });
                //        //if the send request succeded set status as sent
                //        if (sendWithdrawReponse.Success)
                //        {
                //            wallet.SendWithdraw(withdraw);
                //        }
                //        //if the send request failed set withdraw as retriable with reason
                //        wallet.SetWithdawAsRetryable(sendWithdrawReponse.Error, withdraw);
                //    }
                //    catch (BusinessRuleException ex)
                //    {
                //        _logger.Error("business rule broken in CompleteIRTWithdrawJob : {Rule}  message :{Message}, WithdrawId :{withdrawId} ", ex._specification.GetType().FullName, ex.Message, withdraw.Id);
                //    }
                //    catch (Exception ex)
                //    {
                //        _logger.Error("unhandled exception occured in CompleteIRTWithdrawJob Message:{0} ,stackTrace : {1}", ex.Message, ex.StackTrace);
                //    }
                //}

                await Task.Delay(10000);
            }
        }
    }
}
