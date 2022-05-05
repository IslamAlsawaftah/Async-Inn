using AsyncInn.Models.Api;
using AsyncInn.Models.DTO;
using AsyncInn.Models.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AsyncInn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService) // inject IUserService 
        {
            _userService = userService;
        }
        //POST: -http://localhost:62689/api/Users/Register
        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register([FromBody] RegisterUser data)
        {
            try
            {
                var user = await _userService.Register(data, this.ModelState);
                if (ModelState.IsValid)
                {
                    return  user;//Ok("Registeration done");
                }
                // if error occur ValidationProblemDetails it will stop application and show error
                // "The field Username must be a string or array type with a minimum length of '3'."
               
                // ModelState because its dictionary we can add key and value
                //ModelState.AddModelError("key", "value error");
                /*"errors": {
                    "key": [
                        "value error"
                ]}*/
                return BadRequest(new ValidationProblemDetails(ModelState));
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginData loginData)
        {
            try
            {
                var user = await _userService.Authenticate(loginData.Username, loginData.Password);
                if (user != null)
                {
                    return user;
                }
                else
                {
                    return BadRequest("User not found");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

/*
 * {
    "Username": "e",
    "Password": "abcdfg#D0",
    "Email": "ilm135@yahoo.com",
    "PhoneNumber": "123456789"
}
is not valid
 */

//public async Task<ActionResult> Register([FromBody] RegisterUser data)
//{
//    try
//    {
//        if (ModelState.IsValid)
//        {

//            return Ok("Registered done");
//        }
//        else
//        {
//            return BadRequest("is not valid");
//        }
//        await _userService.Register(data, this.ModelState);
//        return Ok("sd");
//    }

//    catch (Exception e)
//    {

//        return BadRequest(e.Message);
//    }
//}