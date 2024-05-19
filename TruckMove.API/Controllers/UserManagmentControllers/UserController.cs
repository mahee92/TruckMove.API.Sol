using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TruckMove.API.BLL.Models.Primary;
using TruckMove.API.BLL;
using TruckMove.API.BLL.Models.PrimaryDTO;
using TruckMove.API.BLL.Services.Primary;
using TruckMove.API.BLL.Services.PrimaryServices;
using TruckMove.API.DAL.Models;
using Microsoft.Extensions.Options;
using TruckMove.API.Helper;
using Microsoft.AspNetCore.JsonPatch;
using TruckMove.API.BLL.Models.UserManagmentDTO;

namespace TruckMove.API.Controllers.PrimaryControllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;
        private readonly MySettings _mySettings;

        public UserController(ILogger<UserController> logger, IUserService userService, IOptions<MySettings> mySettings)
        {
            _logger = logger;
            _userService = userService;
            _mySettings = mySettings.Value;
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

       
        ////[HttpPost("UploadImage")]
        ////public async Task<IActionResult> UploadImage([FromForm] FileUpload fileUpload)
        ////{
        ////    if (fileUpload == null || fileUpload.file == null || fileUpload.file.Length == 0)
        ////    {
        ////        return StatusCode((int)ErrorCode.fileNotFound, ErrorMessages.FileNotFound);
        ////    }
        ////    try
        ////    {
        ////        var fileUrl = await FileUploaderUtil.UploadImage(_mySettings.FileLocation, fileUpload, Meta.USER_IMG_PATH, Request.Scheme, Request.Host);
        ////        return Ok(fileUrl);
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        return StatusCode((int)ErrorCode.InternalServerError, ex.InnerException);
        ////    }
        ////}
    }
}
