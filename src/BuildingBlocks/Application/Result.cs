using CXChange.BuildingBlocks.Application.Error;

namespace CXChange.BuildingBlocks.Application
{
    public class Result
    {
        public bool Success { get; }
        public string Error { get; private set; }
        public ResultStatus Status { get; private set; }
        protected Result(bool success, string error)
        {
            Success = success;
            Error = error;
        }
        protected Result(bool success, IError errors, ResultStatus status)
        {
            Success = success;
            Errors = errors;
            Status = status;

        }
        public IError Errors { get; }

        public bool isSucceed => Success;
        public static Result Fail(string message)
        {
            return new Result(false, message);
        }
        public static Result OK()
        {
            return new Result(true, "OK");
        }
        public static Result<T> Fail<T>(string message)
        {
            return new Result<T>(false, default(T), message);
        }
        public static Result<T> OK<T>(T data)
        {
            return new Result<T>(true, data, "OK");
        }
        public static Result ValidationFailed(ValidationError validationErrors)
        {
            return new Result(false, validationErrors, ResultStatus.ValidationFailed);
        }
        public static Result CommandError(Enum errorCode, string message = null)
        {
            return new Result(false, new CommandError(errorCode, message), ResultStatus.CommandError);

        }
        public static Result<T> CommandError<T>(Enum errorCode, string message = null)
        {
            return new Result<T>(false, new CommandError(errorCode, message), ResultStatus.CommandError, default(T));

        }


    }
    public class Result<T> : Result
    {



        public T Data { get; set; }

        public Result(bool success, T data, string message) : base(success, message)
        {
            Data = data;
        }
        public Result(bool success, IError error, ResultStatus status, T data) : base(success, error, status)
        {

        }






    }
    public enum ResultStatus
    {
        Success,
        ValidationFailed,
        CommandError,
    }
}
