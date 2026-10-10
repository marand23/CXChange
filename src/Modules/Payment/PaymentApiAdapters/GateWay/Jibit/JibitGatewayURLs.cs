using System.ComponentModel.DataAnnotations;

namespace CXChange.Modules.Payment.PaymentApiAdapters.GateWay.Jibit
{
    internal class JibitGatewayURLs
    {
        [Required]
        public string Purchase { get; init; }
        [Required]
        public string Verify { get; init; }
        [Required]
        public string Inquiry { get; init; }
        [Required]
        public string CallbackUri { get; init; }
        [Required]
        public string SendWithdraw { get; init; }

        public string InquriyWithdraw { get; init; }

        public string RetryWithdraw { get; init; }


        public string GetVerifyURL(string purchaseId) => string.Format(Verify, purchaseId);
        public string GetInquiryURL(string purchaseId) => Inquiry + $"?purchaseId={purchaseId}";

    }

}

