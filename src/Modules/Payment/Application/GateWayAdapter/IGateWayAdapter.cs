using CXChange.BuildingBlocks.Application;
using CXChange.Modules.Payment.Application.GateWayAdapter.Inquiry;
using CXChange.Modules.Payment.Application.GateWayAdapter.InquiryWithdraw;
using CXChange.Modules.Payment.Application.GateWayAdapter.Purchase;
using CXChange.Modules.Payment.Application.GateWayAdapter.RetryWithdraw;
using CXChange.Modules.Payment.Application.GateWayAdapter.Verify;
using CXChange.Modules.Payment.Application.GateWayAdapter.Withdraw;

namespace CXChange.Modules.Payment.Application.GateWayAdapter
{
    public interface IGatewayAdapter
    {

        Task<Result<GateWayPurchaseResponse>> CreatePurchase(GateWayPurchaseRequest request);
        Task<Result<GateWayVerifyResponse>> Verify(GateWayVerifyRequest request);
        Task<GatewayInquiryResponse> Inquiry(GatewayInquiryRequest request);
        Task<Result<SendWithdrawResponse>> SendWithdraw(SendWithdrawRequest request);
        Task<Result<InquiryWithdrawResponse>> InquiryWithdraw(InquiryWithdrawRequest request);
        Task<Result<RetryWithdrawResponse>> RetryWithdraw(RetryWithdrawRequest request);
    }
}
