using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TruckMove.API.BLL;
using TruckMove.API.BLL.Models.UserManagmentDTO;
using TruckMove.API.BLL.Services.Primary;
using TruckMove.API.Helper;

namespace TruckMove.API.Controllers.UserManagmentControllers
{
    public class LoignController : ControllerBase
    {
        private readonly JwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserService _userService;
        public LoignController(JwtTokenGenerator jwtTokenGenerator, IUserService userService)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginModel)
        {
            var authres= await _userService.Auth(loginModel);
            if (authres.Success)
            {
                
                var token = _jwtTokenGenerator.GenerateToken(loginModel.UserName, authres.Object.Id ,authres.Object.Roles);
                authres.Object.jwtToken = token;
                return Ok(authres.Object);
            }
            else
            {                    
                    return StatusCode((int)authres.ErrorType, authres.ErrorMessage);
            }

        }
        //[HttpGet("protected")]
        //[Authorize]
        //public IActionResult Protected()
        //{
        //    return Ok("You have accessed a protected endpoint.");
        //}

        [HttpPost("logout")]

        public IActionResult Logout()
        {
            var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
            {
                TokenBlacklist.AddToken(token);
                return Ok(new { message = "Logged out successfully." });
            }

            return BadRequest(new { message = "Token is missing." });
        }
    }
}
