using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TruckMove.API.BLL.Models.Primary;
using TruckMove.API.BLL;
using TruckMove.API.DAL.Models;
using TruckMove.API.BLL.Services.Primary;
using Microsoft.AspNetCore.JsonPatch;

namespace TruckMove.API.Controllers.Primary
{
    [ApiController]
    [Route("[controller]")]
    public class CompanyController : ControllerBase
    {


        private readonly ILogger<CompanyController> _logger;
        private readonly ICompanyService _companyService;

        public CompanyController(ILogger<CompanyController> logger, ICompanyService companyService)
        {
            _logger = logger;
            _companyService = companyService;
        }
        //test

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


        // create Put method to update company
        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody] CompanyDtoUpdate company)
        {
            
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

        
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] CompanyDto company)
        {
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

        //[HttpPatch("{id}")]
        //public async Task<IActionResult> PatchCompany(int id, [FromBody] JsonPatchDocument<CompanyDtoUpdate> patchDoc)
        //{
        //    if (patchDoc == null)
        //    {
        //        return BadRequest();
        //    }

        //    try
        //    {
        //        await _companyService.UpdateCompanyPartialAsync(id, patchDoc);
        //        return NoContent();
        //    }
        //    catch (KeyNotFoundException)
        //    {
        //        return NotFound();
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Error updating company");
        //        return StatusCode(500, "Internal server error");
        //    }
        //}




    }
}