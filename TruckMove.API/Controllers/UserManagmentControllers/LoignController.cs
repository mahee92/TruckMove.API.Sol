using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Login([FromBody] LoginDto loginModel)
        {
            // Replace this with your actual user validation logic
            if (loginModel.UserName == "test" && loginModel.Password == "password")
            {
                var token = _jwtTokenGenerator.GenerateToken(loginModel.UserName);
                return Ok(new { Token = token });
            }

            return Unauthorized();
        }
        [HttpGet("protected")]
        [Authorize]
        public IActionResult Protected()
        {
            return Ok("You have accessed a protected endpoint.");
        }
    }
}
