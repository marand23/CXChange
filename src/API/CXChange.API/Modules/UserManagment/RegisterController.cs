
using CXChange.Modules.UserAccess.Application.Commands.UserRegistration;
using CXChange.Modules.UserAccess.Application.DomainEventHandlers;
using CXChange.Modules.UserAccess.Application.RequestModels.UserRegistration;

using CXChange.Modules.UserAccess.Domain.User.DomainEvents;
using CXChange.Modules.UserAccess.Integration.IntegrationEvents;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Serilog;
using CXChange.API.Extensions;
using MassTransit.Mediator;
using CXChange.BuildingBlocks.Application;

namespace CXChange.API.Modules.UserManagment
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
       private MassTransit.Mediator.IMediator _mediator;
       private Serilog.ILogger _logger;
        public RegisterController(MassTransit.Mediator.IMediator mediator,Serilog.ILogger logger)
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
        public async Task<ActionResult<UserRegistered>> RegisterUser([FromBody]RegisterRequest request)
        {
            await _mediator.Publish(new RegisterUser { Password = request.Password ,PhoneNumber=request.PhoneNumber});

            //var client = _mediator.CreateRequestClient<RegisterUser>();

            //var response = await client.GetResponse<UserRegistered>(new RegisterUser { Password=request.Password, PhoneNumber = request.PhoneNumber });

            return Ok();
           // return this.TranslateToHttp(response.Message);
        }
        [HttpPost]
        public async Task<IActionResult> ConfirmUser(RegisterConfirmation confirmation)
        {
            //  await _userRegistrationService.ConfirmRegistration(confirmation);
            return Ok();
        }
    }
    public class cred
    {
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
    }
}
