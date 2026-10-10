using CXChange.Modules.Payment.Application.GateWayAdapter.InquiryWithdraw;

namespace CXChange.Modules.Payment.PaymentApiAdapters.GateWay.Zibal.WithdrawInquiry.Maps
{
    internal static class MapZibalWithdrawInquiryResponseToWithdrawInquiryResponse
    {
        public static InquiryWithdrawResponse MapToInquiryWithdrawResponse(this ZibalWithdrawalInquiryResponse src)
        {
            return new InquiryWithdrawResponse
            {
                Status = GetStatus(src.data.type),
                BankTransferId = src.data.trackId,

            };

        }
        private static InquiryWithdrawStatus GetStatus(string type)
        {
            InquiryWithdrawStatus status = type switch
            {
                "checkout" => InquiryWithdrawStatus.Completed,
                "checkoutQueue" => InquiryWithdrawStatus.InProgress,
                "canceledCheckout" => InquiryWithdrawStatus.Cancelled,
                _ => throw new NotImplementedException()
            };
            return status;
        }
    }
}
