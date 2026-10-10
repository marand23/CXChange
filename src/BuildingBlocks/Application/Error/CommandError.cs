namespace CXChange.BuildingBlocks.Application.Error
{
    public class CommandError : IError
    {
        public string Message { get; set; }
        public Enum ErrorCode { get; protected set; }
        public CommandError(Enum errorCode, string message)
        {

            ErrorCode = errorCode;
            Message = message;
        }
    }
}
