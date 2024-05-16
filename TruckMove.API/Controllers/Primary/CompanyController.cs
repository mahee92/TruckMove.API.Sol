using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TruckMove.API.BLL.Models.Primary;
using TruckMove.API.BLL;
using TruckMove.API.DAL.Models;
using TruckMove.API.BLL.Services.Primary;


namespace TruckMove.API.Controllers.Primary
{
    [ApiController]
    [Route("[controller]")]
    public class CompanyController : ControllerBase
    {


        private readonly ILogger<CompanyController> _logger;
        private readonly ICompanyService _companyService;

        public CompanyController(ILogger<CompanyController> logger, ICompanyService companyService, DbContextOptions<TrukMoveLocalContext> dbContextOptions)
        {
            _logger = logger;
            _companyService = new CompanyService(dbContextOptions);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {

            Response<Company> response = await _companyService.GetAsync(id);


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
        public async Task<IActionResult> PutAsync([FromBody] UpdateCompany company)
        {
            
            Response<UpdateCompany> response = await _companyService.UpdateAsync(company);
           
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
        public async Task<IActionResult> PostAsync([FromBody] Company company)
        {
            Response<Company> response = await _companyService.AddAsync(company);
            if (response.Success)
            {
                return CreatedAtRoute("Company", new { id = response.Object.CompanyId }, response.Object);
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
            Response<Company> response = await _companyService.DeleteAsync(id);
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




    }
}