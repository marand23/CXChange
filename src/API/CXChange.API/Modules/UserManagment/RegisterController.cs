
using CXChange.Modules.UserAccess.Application.Commands.UserRegistration;
using CXChange.Modules.UserAccess.Application.DomainEventHandlers;
using CXChange.Modules.UserAccess.Application.RequestModels.UserRegistration;
using CXChange.Modules.UserAccess.Application.Services.Interfaces;
using CXChange.Modules.UserAccess.Domain.User.DomainEvents;
using CXChange.Modules.UserAccess.Integration.IntegrationEvents;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Serilog;
using CXChange.API.Extensions;
namespace CXChange.API.Modules.UserManagment
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
       private MediatR.IMediator _mediator;
       private Serilog.ILogger _logger;
        public RegisterController(IMediator mediator,Serilog.ILogger logger)
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
        public async Task<ActionResult<UserRegistered>> RegisterUser(string PhoneNumber, string Password)
        {
            try
            {
               
                //  await _publishEndpoint.Publish(new UserRegistrationConfirmedIntegrationEvent { PhoneNumber=PhoneNumber, Id = "ujuh" });
                //    await _publishEndpoint.Publish(new UserRegisteredDomainEvent { PhoneNumber = "fsf", Email = "fsfd" });
                var b= await _mediator.Send(new RegisterUser { Password = Password ,PhoneNumber=PhoneNumber});

                // await _mediator.Publish(new UserRegisteredDomainEvent {  Email="",PhoneNumber="fsdf"});
                return this.ReturnSuccess(b);  
            }
            catch (Exception ex)
            {
               return  this.ReturnFailure(ex);
            }
           
           

            return Ok("");
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
