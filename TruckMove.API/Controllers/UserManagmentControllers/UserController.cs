using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TruckMove.API.BLL.Models.Primary;
using TruckMove.API.BLL.Models.PrimaryDTO;
using TruckMove.API.BLL.Services.Primary;
using TruckMove.API.BLL.Services.PrimaryServices;
using TruckMove.API.DAL.Models;
using Microsoft.Extensions.Options;
using TruckMove.API.Helper;
using Microsoft.AspNetCore.JsonPatch;
using TruckMove.API.BLL.Models.UserManagmentDTO;
using System.Collections.Generic;
using TruckMove.API.Settings;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using TruckMove.API.BLL.Helper;

namespace TruckMove.API.Controllers.PrimaryControllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "Administrator")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;
        private readonly IAuthUserService _authUserService;


        public UserController(ILogger<UserController> logger, IUserService userService, IOptions<MySettings> mySettings, IAuthUserService authUserService)
        {
            _logger = logger;
            _userService = userService;
            _authUserService = authUserService;

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            Response<UserOutputDto> response = await _userService.GetAsync(id);
            if (response.Success)
            {
                return Ok(response.Object);
            }
            else
            {
                _logger.BeginScope(response.ErrorMessage);
                return StatusCode((int)response.ErrorType, response.ErrorMessage);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] UserInputDto user)
        {
            user.CreatedById = Convert.ToInt32(_authUserService.GetUserId());
            Response<UserOutputDto> response = await _userService.AddAsync(user);
            if (response.Success)
            {
                return Ok(response.Object);
            }
            else
            {
                _logger.BeginScope(response.ErrorMessage);
                return StatusCode((int)response.ErrorType, response.ErrorMessage);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            Response<UserOutputDto> response = await _userService.DeleteAsync(id);
            if (response.Success)
            {
                return NoContent();
            }
            else
            {
                _logger.BeginScope(response.ErrorMessage);
                return StatusCode((int)response.ErrorType, response.ErrorMessage);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var response = await _userService.GetAllAsync();
            if (response.Success)
            {
                return Ok(response.Objects);
            }
            else
            {
                _logger.BeginScope(response.ErrorMessage);
                return StatusCode((int)response.ErrorType, response.ErrorMessage);
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody] UserUpdateDto user)
        {
            user.UpdatedById = Convert.ToInt32(_authUserService.GetUserId());
            Response<UserOutputDto> response = await _userService.UpdateAsync(user);
            if (response.Success)
            {
                return NoContent();
            }
            else
            {
                _logger.BeginScope(response.ErrorMessage);
                return StatusCode((int)response.ErrorType, response.ErrorMessage);
            }
        }


        [HttpPost("{id}/AddRoles")]
        public async Task<IActionResult> AddRoles(int id, [FromBody] List<int> roles)
        {
            var response = await _userService.AddRoles(id, roles);
            if (response.Success)
            {
                return Ok();
            }
            else
            {
                _logger.BeginScope(response.ErrorMessage);
                return StatusCode((int)response.ErrorType, response.ErrorMessage);
            }
        }

        [HttpGet("{id}/GetRoles")]
        public async Task<IActionResult> GetRoles(int id)
        {
            var response = await _userService.GetRolesByUser(id);
            if (response.Success)
            {
                return Ok(response.Objects);
            }
            else
            {
                _logger.BeginScope(response.ErrorMessage);
                return StatusCode((int)response.ErrorType, response.ErrorMessage);
            }
        }

    }
}
