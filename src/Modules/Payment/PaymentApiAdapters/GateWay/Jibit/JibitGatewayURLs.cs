namespace CXChange.Modules.Payment.PaymentApiAdapters.GateWay.Jibit
{
    internal class JibitGatewayURLs
    {
        private string _verify;
        public string Purchase { get; set; }
        public string Verify
        {
            get { return _verify; }
            set { _verify = value; }
        }

        public string CallbackUri { get; set; }

    }

}

