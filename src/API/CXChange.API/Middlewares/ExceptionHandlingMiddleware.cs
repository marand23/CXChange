using CXChange.BuildingBlocks.Application.Exceptions;
using CXChange.BuildingBlocks.Domain;
using CXChange.BuildingBlocks.Infrastructure;
using MassTransit;
using Newtonsoft.Json;
using System.Net;

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
                //   var body = await System.Text.Json.JsonSerializer.DeserializeAsync<JsonElement>(context.Request.Body);
                // Console.WriteLine(body);
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

            switch (exception)
            {
                case BuildingBlocks.Application.Exceptions.ValidationException ex:
                    {
                        response.StatusCode = 422;

                    };
                    break;
                case BusinessRuleException ex:
                    response.StatusCode = 400;
                    break;
                case ExchangeCommandException ex:
                    response.StatusCode = 400;
                    break;
                case ApiException ex:
                    response.StatusCode = 500;
                    break;
                default:
                    _logger.Error(exception.Message);
                    _logger.Error(exception.StackTrace);
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }
            ;



            var result = JsonConvert.SerializeObject(errorResponse);
            await response.WriteAsync(result);
        }
        private IApiError GetError(Exception ex)
        {

            if (ex is ValidationException e)
            {
                ApiValidationResponse errors = e;
                return errors;
            }
            else
            {
                ApiErrorResponse err = ex;
                return err;
            }


        }



    }

}



