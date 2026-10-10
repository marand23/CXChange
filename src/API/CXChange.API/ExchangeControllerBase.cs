
using CXChange.API.Modules.UserManagment.SuccessMessages.Register;
using CXChange.API.ResourceFiles;
using CXChange.BuildingBlocks.Application;
using CXChange.BuildingBlocks.Application.Error;
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
            var mediatorResponse = await GetMediatorResponseWithResult<TRequest, TResponse>(result);
            return TranslateToHttp<TResponse>(mediatorResponse);
        }
        private ActionResult TranslateToHttp<T>(Result result, string responseType = null)
        {

            ActionResult res = result.Status switch
            {
                ResultStatus.ValidationFailed => UnprocessableEntity((ApiValidationResponse)(ValidationError)result.Errors),

                ResultStatus.Success => Ok(new ApiSuccessResponse<T> { data = ((Result<T>)result).Data, success = true, message = RegisterMessages.GetMessage(result) }),

                ResultStatus.CommandError => BadRequest(new ApiErrorResponse { code = Convert.ToInt32(((CommandError)(result).Errors).ErrorCode), message = CommandErrorMessages.ResourceManager.GetString(((CommandError)(result).Errors).ErrorCode.ToString()), success = false }),

                _ => throw new NotImplementedException(),
            };
            return res;

        }
        public async Task<TResponse> GetRawResponse<TRequest, TResponse>(TRequest request) where TRequest : class where TResponse : class
        {
            return await GetMediatorResponse<TRequest, TResponse>(request);
        }
        private async Task<Result> GetMediatorResponseWithResult<TRequest, TResponse>(TRequest request) where TRequest : class where TResponse : class
        {

            var client = _mediator.CreateRequestClient<TRequest>();
            var response = await client.GetResponse<Result>(request);
            return response.Message;




        }


    }
}
