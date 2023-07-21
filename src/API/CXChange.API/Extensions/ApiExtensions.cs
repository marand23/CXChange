using CXChange.BuildingBlocks.Domain;
using MassTransit.SagaStateMachine;
using Microsoft.AspNetCore.Mvc;

namespace CXChange.API.Extensions
{
    public static class ApiExtensions
    {
        public static ActionResult<T> ReturnSuccess<T>(this ControllerBase controller, T data) where T : class
        {
            return controller.Ok(new ApiSuccessModel<T> { data = data, success = true, message = "successfull message" });
        }
        public static ActionResult ReturnFailure(this ControllerBase controller,Exception exp) 
        {

            if (exp is BusinessRuleException)
            {
                return controller.BadRequest(new ApiErrorModel { message = ((BusinessRuleException)exp).Message, success = false });
            }
            return controller.ValidationProblem();
           
           
        }


    }
    public enum ResponseStatusEnum
    {
        success,
        fail,
    }
    public class ApiErrorModel
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
}
