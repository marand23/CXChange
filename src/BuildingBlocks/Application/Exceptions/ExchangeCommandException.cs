namespace CXChange.BuildingBlocks.Application.Exceptions
{
    public class ExchangeCommandException : Exception
    {
        public string Message { get; }
        public int ErrorCode { get; }
        public ExchangeCommandException(string message, int errorCode)
        {
            Message = message;
            ErrorCode = errorCode;
        }
    }
}
