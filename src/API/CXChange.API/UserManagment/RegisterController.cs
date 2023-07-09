using CXChange.Modules.UserAccess.Application.RequestModels.UserRegistration;
using CXChange.Modules.UserAccess.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CXChange.API.UserManagment
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private IUserRegistrationService _userRegistrationService;
        public RegisterController(IUserRegistrationService userRegistrationService)
        {
            _userRegistrationService = userRegistrationService;
        }
        /// <summary>
        /// registering user for the first time 
        /// </summary>
        /// <param name="PhoneNumber"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> RegisterUser(Register register)
        {
            var a = await _userRegistrationService.RegisterUser(register);
            return Ok(a);
        }
        [HttpPost]
        public async Task<IActionResult> ConfirmUser(RegisterConfirmation confirmation)
        {
            await _userRegistrationService.ConfirmRegistration(confirmation);
            return Ok();
        }
    }
}
