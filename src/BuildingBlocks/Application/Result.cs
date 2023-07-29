namespace CXChange.BuildingBlocks.Application
{
    public class Result<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public ResultStatus Status { get; set; }
        public T Data { get; set; }

        public string[] Errors { get; set; }
        public Result()
        {
            // Initialize properties
        }


        public static Result<T> CreateSuccess(T data, string successMessage)
        {
            return new Result<T> { Data = data, Status = ResultStatus.Success };
        }
        public static Result<T> CreateFailure(string message, ResultStatus status, string[] errors = null)
        {
            return new Result<T> { Success = false, Status = status, Errors = errors, Message = message };



        }


    }
    public enum ResultStatus
    {
        Success,
        CommandNotValid,
        DomainRuleNotValid


    }
}
