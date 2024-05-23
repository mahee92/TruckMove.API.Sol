using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TruckMove.API.BLL.Models.Primary;
using TruckMove.API.DAL.Models;
using TruckMove.API.BLL.Services.Primary;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using TruckMove.API.BLL.Helper;
using TruckMove.API.Helper;
using TruckMove.API.Settings;
using System.Runtime;
using Microsoft.Extensions.Options;

namespace TruckMove.API.Controllers.Primary
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "Administrator")]
    public class CompanyController : ControllerBase
    {


        private readonly ILogger<CompanyController> _logger;
        private readonly ICompanyService _companyService;
        private readonly IAuthUserService _authUserService;
        private readonly MySettings _mySettings;

        public CompanyController(ILogger<CompanyController> logger, ICompanyService companyService, IAuthUserService authUserService, IOptions<MySettings> mySettings)
        {
            _logger = logger;
            _companyService = companyService;
             _authUserService = authUserService;
            _mySettings = mySettings.Value;
        }
 

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {

            Response<CompanyDto> response = await _companyService.GetAsync(id);


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

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var response = await _companyService.GetAllAsync();
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

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] CompanyDto company)
        {
            company.CreatedById = Convert.ToInt32(_authUserService.GetUserId());
            Response<CompanyDto> response = await _companyService.AddAsync(company);
            if (response.Success)
            {
                // return CreatedAtRoute("Company", new { id = response.Object.Id }, response.Object);
                return Ok(response.Object);
            }
            else
            {
                _logger.BeginScope(response.ErrorMessage);
                return StatusCode((int)response.ErrorType, response.ErrorMessage);
            }
        }


        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody] CompanyDtoUpdate company)
        {

            company.UpdatedById = Convert.ToInt32(_authUserService.GetUserId());
            Response<CompanyDtoUpdate> response = await _companyService.UpdateAsync(company);
           
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


        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            Response<CompanyDto> response = await _companyService.DeleteAsync(id);
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
        
        
       
        


        [HttpGet("{id}/Contacts")]
        public async Task<IActionResult> GetContactsByCompany(int id)
        {
            var response = await _companyService.GetContactsByCompany(id);
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
        [HttpPost("UploadLogo")]
        public async Task<IActionResult> UploadLogo([FromForm] FileUpload fileUpload)
        {
            if (fileUpload == null || fileUpload.file == null || fileUpload.file.Length == 0)
            {
                return StatusCode((int)ErrorCode.fileNotFound, ErrorMessages.FileNotFound);
            }
            try
            {
                var fileUrl = await FileUploderUtil.UploadImage(_mySettings.FileLocation, fileUpload, Meta.COMPANY_IMG_PATH, Request.Scheme, Request.Host);
                return Ok(fileUrl);

            }
            catch (Exception ex)
            {
                return StatusCode((int)ErrorCode.InternalServerError, ex.InnerException);

            }

        }



    }
}