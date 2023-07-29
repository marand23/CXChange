using CXChange.Modules.UserAccess.Application.Commands.UserRegistration;
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
        [HttpGet]
        public async Task<IActionResult> ConfirmUser(string PhoneNumber, string Password)
        {

            return Ok();
        }
    }
    public class cred
    {
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
    }
}
