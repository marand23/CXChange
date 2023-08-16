namespace CXChange.API
{
    public enum ResponseStatusEnum
    {
        success,
        fail,
    }
    public class ApiErrorModel : IApiError
    {
        public string message { get; set; }

        public bool success { get; set; }
    }
    public class ApiSuccessModel<T>
    {
        public string message { get; set; }
        public bool success { get; set; }
        public T data { get; set; }
    }
    public class ApiValidationErrorModel : IApiError
    {
        public string message { get; set; }
        public bool success { get; set; }
        public string[] errors { get; set; }
    }
    public interface IApiError { }
}
