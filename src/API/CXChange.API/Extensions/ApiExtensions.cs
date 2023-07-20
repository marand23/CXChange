using CXChange.BuildingBlocks.Domain;
using MassTransit.SagaStateMachine;
using Microsoft.AspNetCore.Mvc;

namespace CXChange.API.Extensions
{
    public static class ApiExtensions
    {
        public static ActionResult<T> ReturnSuccess<T>(this ControllerBase controller, T data) where T : class
        {
            return controller.Ok(new SuccessApiModel<T> { data = data, success = true, Message = "successfull message" });
        }
        public static ActionResult ReturnFailure(this ControllerBase controller,Exception exp) 
        {
           
            if (exp is BusinessRuleException)
            {
                return controller.BadRequest(new ApiErrorMode { Message = ((BusinessRuleException)exp).Message, success = false });
            }
            return controller.NotFound(exp.Message);
           
        }


    }
    public enum ResponseStatusEnum
    {
        success,
        fail,
    }
    public class ApiErrorMode
    {
        public string Message { get; set; }
        public bool success { get; set; }
    }
    public class SuccessApiModel<T>
    {
        public string Message { get; set; }
        public bool success { get; set; }
        public T data { get; set; }
    }
}
