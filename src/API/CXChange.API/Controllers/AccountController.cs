using CXChange.Modules.UserManagment.Application;
using CXChange.Modules.UserManagment.Application.Services.Interfaces;
using CXChange.Modules.UserManagment.Application.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CXChange.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase

    { 
        private IUserService _userService;
        public AccountController(IUserService user) {
             _userService = user;
        }
        /// <summary>
        /// registering user for the first time 
        /// </summary>
        /// <param name="PhoneNumber"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CreateUser(string PhoneNumber, string Password) {
           _userService.CreateUser(PhoneNumber, Password);  
            return Ok();
        }
    }
}
