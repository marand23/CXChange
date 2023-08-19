using CXChange.API.Modules.UserManagment.ErrorMessages;
using CXChange.BuildingBlocks.Application.Exceptions;
using CXChange.BuildingBlocks.Domain;
using MassTransit;
using Newtonsoft.Json;
using System.Net;

namespace CXChange.API.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IEnumerable<IError> _error;
        Serilog.ILogger _logger;
        public ExceptionHandlingMiddleware(RequestDelegate next, Serilog.ILogger logger, IEnumerable<IError> error)
        {
            _next = next;
            _logger = logger;
            _error = error;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }

            catch (RequestException ex)
            {
                await HandleExceptionAsync(context, ex.GetBaseException());

            }
            catch (Exception ex)
            {

                await HandleExceptionAsync(context, ex);
            }


        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            var response = context.Response;
            IApiError errorResponse = null;
            switch (exception)
            {
                case BuildingBlocks.Application.Exceptions.ValidationException ex:
                    {
                        response.StatusCode = 422;
                        ValidationErrorCodes errorCodes;
                        List<ValidationError> error = ex.Errors
                        .Select(e => new ValidationError { Code = (ValidationErrorCodes)Enum.Parse(typeof(ValidationErrorCodes), e.ErrorCode), Message = e.ErrorMessage, Name = e.PropertyName }).ToList();

                        errorResponse = new ApiValidationErrorModel
                        {
                            errors = error,
                            message = "Validation Failed",
                            success = false
                        };
                    };
                    break;
                case BusinessRuleException ex:
                    response.StatusCode = 400;
                    errorResponse = new ApiErrorResponse { code = ex.Code, message = ex.Message, success = false };

                    break;
                case ExchangeCommandException ex:
                    response.StatusCode = 400;

                    errorResponse = new ApiErrorResponse { success = false, code = ex.ErrorCode, message = ex.Message };
                    break;
                default:
                    _logger.Error(exception.Message);
                    _logger.Error(exception.StackTrace);
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    errorResponse = new ApiErrorResponse
                    {
                        success = false,
                        message = "مشکلی پیش امده لطفا بعدا امتحان بکنید",
                        code = 500
                    };
                    break;
            }
            ;



            var result = JsonConvert.SerializeObject(errorResponse);
            await response.WriteAsync(result);
        }
        private IApiError ErrorClass(string innerMessage, string nameSpace)
        {

            if (nameSpace.Contains("UserAccess"))
            {
                var error = _error.FirstOrDefault(x => x.GetType() == typeof(UserAccessError));
                return (ApiErrorResponse)error.GetError(innerMessage);


            }
            throw new NotImplementedException();

        }



    }

}



