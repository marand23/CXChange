namespace CXChange.BuildingBlocks.Application.Exceptions
{
    public class ExchangeCommandException : Exception
    {
        public string Message { get; set; }
        public Enum ErrorCode { get; protected set; }
        public ExchangeCommandException(Enum errorCode, string message) : base(message)
        {

            ErrorCode = errorCode;
            Message = message;
        }

    }
}
