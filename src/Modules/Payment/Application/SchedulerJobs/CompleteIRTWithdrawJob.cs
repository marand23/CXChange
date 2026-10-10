using CXChange.BuildingBlocks.Domain;
using CXChange.Modules.Payment.Application.GateWayAdapter;
using CXChange.Modules.Payment.Application.GateWayAdapter.InquiryWithdraw;
using CXChange.Modules.Payment.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace CXChange.Modules.Payment.Application.SchedulerJobs
{

    internal class CompleteIRTWithdrawJob
    {
        IWithdrawRepository _withdrawRepo;
        IWalletRepository _walletRepo;
        IUnitOfWork _uow;
        ILogger _logger;
        IGatewayAdapter _gatewayAdapter;
        public CompleteIRTWithdrawJob(IWithdrawRepository withdrawRepo, IWalletRepository walletRepo, [FromKeyedServices("payment")] IUnitOfWork uow, ILogger logger, IGatewayAdapter gatewayAdapter)
        {
            _withdrawRepo = withdrawRepo;
            _walletRepo = walletRepo;
            _uow = uow;
            _logger = logger;
            _gatewayAdapter = gatewayAdapter;
        }
        public async Task CompleteWithdraws()
        {
            var sentWithdraws = await _withdrawRepo.GetSentWithdraws("IRT");
            if (sentWithdraws != null)
            {
                return;
            }

            foreach (var withdraw in sentWithdraws.ToList())
            {

                try
                {
                    var wallet = await _walletRepo.GetWalletByUserIdAsync(withdraw.UserId, withdraw.Currency.Symbol);
                    //if wallet is null there is a bug(withdraw cannot be crated without wallet)
                    if (wallet == null)
                    {
                        _logger.Error("Wallet not found for withdraw in CompleteIRTWithdrawJob , withdrawId:{0} , UserId: {1}", withdraw.Id, withdraw.UserId);
                        continue;
                    }

                    var inquiryWithdrawResult = await _gatewayAdapter.InquiryWithdraw(new InquiryWithdrawRequest { WithdrawId = withdraw.Id });
                    //if succeded
                    if (inquiryWithdrawResult.isSucceed)
                    {
                        //if withdraw completed deduct balance and set as completed
                        if (inquiryWithdrawResult.Data.Status == InquiryWithdrawStatus.Completed)
                        {
                            wallet.CompleteWithdraw(withdraw);

                        }
                        //if withdraw failed set retry with failure reason
                        else if (inquiryWithdrawResult.Data.Status == InquiryWithdrawStatus.Failed)
                        {

                            wallet.SetWithdawAsRetryable(inquiryWithdrawResult.Data.FailureReason, withdraw);
                        }
                        //in other cases we do not take any action until the withdraw is completed or failed
                    }
                    //if result of inquiry is not successfull,set retry with reason
                    wallet.SetWithdawAsRetryable(inquiryWithdrawResult.Error, withdraw);
                    await _uow.CommitChangesAsync();

                }
                catch (BusinessRuleException ex)
                {
                    _logger.Error("business rule broken in CompleteIRTWithdrawJob : {Rule}  message :{Message}, WithdrawId :{withdrawId} ", ex._specification.GetType().FullName, ex.Message, withdraw.Id);
                }
                catch (Exception ex)
                {
                    _logger.Error("unhandled exception occured in CompleteIRTWithdrawJob Message:{0} ,stackTrace : {1}", ex.Message, ex.StackTrace);
                }

            }


        }



    }
}
