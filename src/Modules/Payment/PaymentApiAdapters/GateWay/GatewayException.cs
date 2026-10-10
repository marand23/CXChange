namespace CXChange.Modules.Payment.PaymentApiAdapters.GateWay
{
    public class GatewayException : Exception
    {

        public Enum ErrorCode { get; protected set; }
        public GatewayException(Enum errorCode, string message) : base(message)
        {

            ErrorCode = errorCode;


        }

    }
}
