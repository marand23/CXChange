
using CXChange.API.Modules.UserManagment.SuccessMessages.Register;
using CXChange.BuildingBlocks.Contract.Mapper;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CXChange.API
{
    public class ExchangeControllerBase : ControllerBase
    {
        private readonly IScopedMediator _mediator;
        private string _cachedUserId;
        protected readonly IMapService _mapService;


        protected string UserId
        {
            get
            {
                if (_cachedUserId == null)
                {
                    _cachedUserId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                }
                return _cachedUserId;
            }
        }

        public ExchangeControllerBase(IScopedMediator mediator, IMapService mapService)
        {
            _mediator = mediator;
            _mapService = mapService;


        }

        private async Task<TResponse> GetMediatorResponse<TRequest, TResponse>(TRequest request) where TRequest : class where TResponse : class
        {
            var client = _mediator.CreateRequestClient<TRequest>();
            var response = await client.GetResponse<TResponse>(request);
            return response.Message;
        }
        public async Task<ActionResult<TResponse>> ReturnResponse<TRequest, TResponse>(TRequest result, string responseType = null) where TRequest : class where TResponse : class
        {
            var mediatorResponse = await GetMediatorResponse<TRequest, TResponse>(result);

            return TranslateToHttp(mediatorResponse, responseType);
        }
        private ActionResult<T> TranslateToHttp<T>(T result, string responseType = null) where T : class
        {
            //    if (responseType == "jpg")
            //    {

            //        //if (result is KycInitiated d)
            //        //{
            //        //    Response.Headers.Add("Content-Disposition", $"attachment; filename={d.FileName}.jpg");
            //        //    return File(d.data, "image/jpeg");
            //        //}
            //    }
            return Ok(new ApiSuccessResponse<T> { data = result, success = true, message = RegisterMessages.GetMessage(result) });
        }


    }

}
