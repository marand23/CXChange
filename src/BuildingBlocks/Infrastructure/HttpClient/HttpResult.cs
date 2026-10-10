namespace CXChange.BuildingBlocks.Infrastructure.HttpClient
{
    public class HttpResult<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public HttpResultStatus Status { get; set; }
        public T Data { get; set; }

        public string[] Errors { get; set; }
        public string Error { get; set; }
        public HttpResult()
        {
            // Initialize properties
        }


        public static HttpResult<T> CreateSuccess(T data, string successMessage)
        {
            return new HttpResult<T> { Data = data, Status = HttpResultStatus.Success, Message = successMessage, Success = true };
        }
        public static HttpResult<T> CreateFailure(string message, HttpResultStatus status)
        {
            return new HttpResult<T> { Success = false, Error = message };
        }


    }
    public enum HttpResultStatus
    {
        Success,
        NotAccessable,




    }
}
