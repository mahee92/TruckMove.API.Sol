using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TruckMove.API.BLL.Models.Primary;
using TruckMove.API.BLL.Models.PrimaryDTO;
using TruckMove.API.BLL.Services.Primary;
using TruckMove.API.BLL.Services.PrimaryServices;
using TruckMove.API.Controllers.Primary;
using TruckMove.API.DAL.Models;
using Microsoft.Extensions.Options;
using TruckMove.API.Helper;
using TruckMove.API.Settings;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using TruckMove.API.BLL.Helper;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TruckMove.API.Controllers.PrimaryControllers
{
    [ApiController]
    [Route("[controller]")]
    //[Authorize(Roles = "Administrator,OpsManager")]
    public class ContactController : ControllerBase
    {


        private readonly ILogger<ContactController> _logger;
        private readonly IContactService _contactservice;
        private readonly MySettings _mySettings;
        private readonly IAuthUserService _authUserService;

        public ContactController(ILogger<ContactController> logger, IContactService companService, IOptions<MySettings> mySettings, IAuthUserService authUserService)
        {
            _logger = logger;
            _contactservice = companService;
            _mySettings = mySettings.Value;
            _authUserService = authUserService;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {

            Response<ContactDto> response = await _contactservice.GetAsync(id);


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
        public async Task<IActionResult> PostAsync([FromBody] ContactDto contact)
        {
            contact.CreatedById = Convert.ToInt32(_authUserService.GetUserId());
            Response<ContactDto> response = await _contactservice.AddAsync(contact);
            if (response.Success)
            {
                //  return CreatedAtRoute("Contact", new { id = response.Object.Id }, response.Object);
                return Ok(response.Object);
            }
            else
            {
                _logger.BeginScope(response.ErrorMessage);
                return StatusCode((int)response.ErrorType, response.ErrorMessage);
            }
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            Response<ContactDto> response = await _contactservice.DeleteAsync(id);
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
            var response = await _contactservice.GetAllAsync();
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
        public async Task<IActionResult> PutAsync([FromBody] ContactUpdateDto contact)
        {
            contact.UpdatedById = Convert.ToInt32(_authUserService.GetUserId());
            Response<ContactUpdateDto> response = await _contactservice.UpdateAsync(contact);

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

        [HttpPost("UploadImage")]
        public async Task<IActionResult> UploadImage([FromForm] FileUpload fileUpload)
        {
            if (fileUpload == null || fileUpload.file == null || fileUpload.file.Length == 0)
            {
                return StatusCode((int)ErrorCode.fileNotFound, ErrorMessages.FileNotFound);
            }
            try
            {                
               var fileUrl = await FileUploderUtil.UploadImage(_mySettings.FileLocation, fileUpload, Meta.CONTACT_IMG_PATH, Request.Scheme, Request.Host);
               return Ok(fileUrl);

            }
            catch (Exception ex)
            {
                return StatusCode((int)ErrorCode.InternalServerError, ex.InnerException);

            }

        }





    }
}
