using Microsoft.AspNetCore.Mvc;
using TruckMove.API.BLL;
using TruckMove.API.BLL.Services;

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
        public IEnumerable<Company> Get()
        {
            return companyService.Get();
        }
    }
}