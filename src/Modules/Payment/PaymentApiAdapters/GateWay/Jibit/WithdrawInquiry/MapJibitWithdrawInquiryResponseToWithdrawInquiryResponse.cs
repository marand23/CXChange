using CXChange.Modules.Payment.Application.GateWayAdapter.InquiryWithdraw;

namespace CXChange.Modules.Payment.PaymentApiAdapters.GateWay.Jibit.WithdrawInquiry
{
    internal static class MapJibitWithdrawInquiryResponseToWithdrawInquiryResponse
    {
        public static InquiryWithdrawResponse MapToInquiryWithdrawResponse(this JibitWithdrawalInquiryResponse src)
        {
            return new InquiryWithdrawResponse
            {
                BankTransferId = src.bankTransferId,
                FailureReason = src.failReason,
                Status = GetStatus(src.state)


            };
        }
        private static InquiryWithdrawStatus GetStatus(string state)
        {
            InquiryWithdrawStatus status = state switch
            {
                "INITIALIZED" => InquiryWithdrawStatus.Initialized,
                "CANCELLING" => InquiryWithdrawStatus.Cancelling,
                "CANCELLED" => InquiryWithdrawStatus.Cancelled,
                "IN_PROGRESS" => InquiryWithdrawStatus.InProgress,
                "TRANSFERRED" => InquiryWithdrawStatus.Completed,
                "FAILED" => InquiryWithdrawStatus.Failed
            };
            return status;
        }
    }
}
