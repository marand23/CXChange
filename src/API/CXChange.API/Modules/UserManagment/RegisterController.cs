using CXChange.Modules.UserAccess.Application.Commands.UserRegistration;
using CXChange.Modules.UserAccess.Application.Queries.Users;
using Microsoft.AspNetCore.Mvc;

namespace CXChange.API.Modules.UserManagment
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private MassTransit.Mediator.IMediator _mediator;
        private Serilog.ILogger _logger;
        public RegisterController(MassTransit.Mediator.IMediator mediator, Serilog.ILogger logger)
        {
            _mediator = mediator;
            _logger = logger;

        }
        /// <summary>
        /// registering user for the first time 
        /// </summary>
        /// <param name="PhoneNumber"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<UserRegistered>> RegisterUser([FromBody] RegisterRequest request)
        {
            await _mediator.Publish(new RegisterUser { Password = request.Password, PhoneNumber = request.PhoneNumber });

            //var client = _mediator.CreateRequestClient<RegisterUser>();

            //var response = await client.GetResponse<UserRegistered>(new RegisterUser { Password=request.Password, PhoneNumber = request.PhoneNumber });

            return Ok();
            // return this.TranslateToHttp(response.Message);
        }
        [HttpPost]
        public async Task<IActionResult> GetUsers(cred cred)
        {

            var client = _mediator.CreateRequestClient<GetUsers>();

            var response = await client.GetResponse<UserList>(new GetUsers(cred.Page, cred.pageSize, cred.Filters));
            return Ok(response);

        }
    }
    public class cred
    {
        public int Page { get; set; }
        public int pageSize { get; set; }
        public Dictionary<string, string> Filters { get; set; }
    }
}
