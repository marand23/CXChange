
using CXChange.API.Modules.UserManagment.SuccessMessages.Register;
using CXChange.BuildingBlocks.Contract.Mapper;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CXChange.API
{
    public class ExchangeControllerBase : ControllerBase
    {
        private readonly IMediator _mediator;
        private string _cachedUserId;
        protected readonly IMapperService _mapService;


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

        public ExchangeControllerBase(IMediator mediator, IMapperService mapService)
        {
            _mediator = mediator;
            _mapService = mapService;


        }

        private async Task<TResponse> GetMediatorResponse<TCommand, TResponse>(TCommand request) where TCommand : class where TResponse : class
        {
            var client = _mediator.CreateRequestClient<TCommand>();
            var response = await client.GetResponse<TResponse>(request);
            return response.Message;
        }
        public async Task<ActionResult<TResponse>> ReturnResponse<TRequest, TResponse>(TRequest result) where TRequest : class where TResponse : class
        {
            //     TCommand command;
            //     if (typeof(TCommand) is INeedsAuthentication cmd)
            //     {
            //         command = _mapper.Map<TRequest, TCommand>(result, opt =>
            //opt.AfterMap((src, dest) => ((INeedsAuthentication)dest).UserId = cmd.UserId));
            //     }
            //     else
            //     {
            //         command = _mapper.Map<TRequest, TCommand>(result);
            //     }
            //     var mediatorResponse = await GetMediatorResponse<TCommand, TResponse>(command);
            var mediatorResponse = await GetMediatorResponse<TRequest, TResponse>(result);
            return TranslateToHttp(mediatorResponse);
        }
        private ActionResult<T> TranslateToHttp<T>(T result) where T : class
        {
            return Ok(new ApiSuccessResponse<T> { data = result, success = true, message = RegisterMessages.GetMessage(result) });
        }


    }
    public enum CommandType
    {
        NeedsAuthentication
    }
}
