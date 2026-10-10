using CXChange.BuildingBlocks.Application.Exceptions;
using CXChange.BuildingBlocks.Domain;
using CXChange.BuildingBlocks.Infrastructure.Exceptions;
using CXChange.Modules.Payment.PaymentApiAdapters.GateWay;
using MassTransit;
using Newtonsoft.Json;

namespace CXChange.API.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        Serilog.ILogger _logger;
        public ExceptionHandlingMiddleware(RequestDelegate next, Serilog.ILogger logger)
        {
            _next = next;
            _logger = logger;

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
            catch (ApiException exception)
            {

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
            IApiError errorResponse = GetError(exception);
            response.StatusCode = exception switch
            {

                BusinessRuleException => 400,
                ExchangeCommandException => 400,
                ApiException => 400,
                GatewayException => 400,
                _ => 500
            };

            if (response.StatusCode == 500)
            {
                _logger.Error("An error occurred while processing the request: {Exception}", exception);

            }
            //else
            //{
            //    _logger.Error("{Message} {UserId}", exception.Message, NewId.NextSequentialGuid());

            //}
            var result = JsonConvert.SerializeObject(errorResponse);
            await response.WriteAsync(result);
        }
        private IApiError GetError(Exception ex)
        {

            //if (ex is ValidationException e)
            //{
            //    ApiValidationResponse errors = e;
            //    return errors;
            //}

            ApiErrorResponse err = ex;
            return err;



        }



    }

}



