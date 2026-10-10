using System.ComponentModel.DataAnnotations;

namespace CXChange.Modules.Payment.PaymentApiAdapters.GateWay.Zibal
{
    internal class ZibalGatewayURLs
    {
        [Required]

        public string Purchase { get; init; }
        [Required]
        public string Verify { get; init; }
        [Required]
        public string CallBackUri { get; init; }
        [Required]
        public string Inquiry { get; init; }
        [Required]
        public string PayLink { get; init; }

        public string SendWithdraw { get; init; }

        public string InquiryWithdraw { get; init; }

        public string RetryWithdraw { get; init; }
    }
}
