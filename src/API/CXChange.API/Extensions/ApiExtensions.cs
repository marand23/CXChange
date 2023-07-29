using Microsoft.AspNetCore.Mvc;

namespace CXChange.API.Extensions
{
    public static class ApiExtensions
    {
        public static ActionResult<T> TranslateToHttp<T>(this ControllerBase controller, T result) where T : class
        {
            //switch (result.Status)
            //{
            /*  case ResultStatus.Success: */
            return controller.Ok(new ApiSuccessModel<T> { data = result, success = true, message = "succeded" });
            //  case ResultStatus.CommandNotValid:
            // return controller.StatusCode(422,
            // new ApiValidationErrorModel { errors = result.Errors, message = "validation-problem", success = false });
            //  case ResultStatus.DomainRuleNotValid: return controller.BadRequest(new ApiErrorModel { success = false,  });

            //default: throw new NotImplementedException();
        }

    }



}
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

