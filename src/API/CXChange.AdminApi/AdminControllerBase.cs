using CXChange.BuildingBlocks.Contract.Mapper;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CXChange.AdminAPI
{
    public class AdminControllerBase : ControllerBase
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

        public AdminControllerBase(IScopedMediator mediator, IMapService mapService)
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
        public async Task<ActionResult<TResponse>> ReturnResponse<TRequest, TResponse>(TRequest result) where TRequest : class where TResponse : class
        {
            var mediatorResponse = await GetMediatorResponse<TRequest, TResponse>(result);

            return TranslateToHttp(mediatorResponse);
        }
        private ActionResult<T> TranslateToHttp<T>(T result) where T : class
        {
            return Ok(new { data = result, success = true });
        }
        public async Task<TResponse> ReturnRawResponse<TRequest, TResponse>(TRequest result) where TRequest : class where TResponse : class
        {
            var mediatorResponse = await GetMediatorResponse<TRequest, TResponse>(result);

            return mediatorResponse; ;
        }

    }
}
