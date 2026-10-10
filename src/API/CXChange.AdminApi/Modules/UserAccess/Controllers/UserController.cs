using CXChange.AdminAPI.Modules.UserAccess.Requests.User;
using CXChange.BuildingBlocks.Contract.Mapper;
using CXChange.Modules.UserAccess.Administrator.Application.Queries.Users;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;

namespace CXChange.AdminAPI.Modules.UserAccess.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : AdminControllerBase
    {
        public UserController(IScopedMediator mediator, IMapService _mapService) : base(mediator, _mapService)
        {
        }

        [HttpPost]
        public async Task<ActionResult<UserList>> GetUsers(GetUsersRequest request)
        {

            return await ReturnResponse<GetUsers, UserList>(_mapService.Map<GetUsersRequest, GetUsers>(request, "this is user id"));
        }
    }
}
