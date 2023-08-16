using CXChange.API.Modules.UserManagment.SuccessMessages.Register;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;

namespace CXChange.API
{
    public class ExchangeControllerBase : ControllerBase
    {
        private IMediator _mediator;

        public ExchangeControllerBase(IMediator mediator)
        {
            _mediator = mediator;
        }
        private async Task<TResponse> GetMediatorResponse<TRequest, TResponse>(TRequest request) where TRequest : class where TResponse : class
        {

            var client = _mediator.CreateRequestClient<TRequest>();


            var response = await client.GetResponse<TResponse>(request);
            return response.Message;



        }
        public async Task<ActionResult<TResponse>> ReturnResponse<TRequest, TResponse>(TRequest result) where TRequest : class where TResponse : class
        {
            var mediatorResponse = await GetMediatorResponse<TRequest, TResponse>(result);
            return TranslateToHttp(mediatorResponse);
        }
        private ActionResult<T> TranslateToHttp<T>(T result) where T : class
        {

            return Ok(new ApiSuccessModel<T> { data = result, success = true, message = RegisterMessages.GetMessage(result) });

        }


    }
}
