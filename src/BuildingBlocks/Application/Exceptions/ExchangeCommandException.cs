namespace CXChange.BuildingBlocks.Application.Exceptions
{
    public class ExchangeCommandException : Exception
    {
        public string Message { get; }
        public Enum ErrorCode { get; }
        public ExchangeCommandException(string message, Enum errorCode)
        {
            Message = message;
            ErrorCode = errorCode;
        }
    }
}
