using Microsoft.AspNetCore.Mvc;
using TruckMove.API.BLL.Models.Primary;
using TruckMove.API.BLL.Services.Primary;
using TruckMove.API.ExeptionHandler;

namespace TruckMove.API.Controllers.Primary
{
    [ApiController]
    [Route("[controller]")]
    public class CompanyController : ControllerBase
    {


        private readonly ILogger<CompanyController> _logger;
        private readonly ICompanyService companyService;

        public CompanyController(ILogger<CompanyController> logger, ICompanyService companyService)
        {
            _logger = logger;
            this.companyService = companyService;
        }

        [HttpGet(Name = "Company")]
        public Company Get(int id)
        {

            var Company = companyService.Get(id);

            if (Company == null)
            {
                throw new NotFoundException(ErrorMessages.NotFound);
            }
            return Company;


        }

        // create Put method to update company
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Company company)
        {

            var existingCompany = companyService.Get(id);

            if (existingCompany == null)
            {
                throw new NotFoundException(ErrorMessages.NotFound);
            }

            //companyService.Update(company);

            return Ok();
        }

        // create Post method to create company
        [HttpPost]
        public IActionResult Post([FromBody] Company company)
        {

            //companyService.Create(company);

            return Ok();
        }   

    }
}