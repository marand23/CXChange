using CXChange.API.Extensions;
using CXChange.BuildingBlocks.Application;
using CXChange.BuildingBlocks.Domain;
using Newtonsoft.Json;
using System.Net;

namespace CXChange.API.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
    
        Serilog.ILogger _logger;
        public ExceptionHandlingMiddleware(RequestDelegate next,Serilog.ILogger logger) { 
            _next = next;
            _logger = logger;  
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);   
            }
           
            catch (Exception ex) { 
            
                await HandleExceptionAsync(context, ex);
            }
           
        }

        private async Task HandleExceptionAsync(HttpContext context,Exception exception)
        {
            context.Response.ContentType = "application/json";
            var response = context.Response;
            IApiError errorResponse = null;
            switch (exception)
            {
                case BusinessRuleException ex:
                    response.StatusCode = 400;
                    errorResponse = new ApiErrorModel
                    {
                        message = ex.Message,
                        success = false
                    };
                    break;
                case InvalidCommandException ex:
                    response.StatusCode = 422;
                    errorResponse = new ApiValidationErrorModel
                    {
                        errors = ex.Errors,
                        message = "validation Failed",
                        success = false
                    };
                    break;
                default :
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    errorResponse = new ApiErrorModel
                    {
                        success = false,
                        message = exception.Message,
                    };
                    break;
            }
            ;
         

            _logger.Error(exception.Message);
           var result= JsonConvert.SerializeObject(errorResponse);
            await response.WriteAsync(result);
        }
    
    }
}
