using TruckMove.API.BLL.Models.Primary;
using TruckMove.API.DAL.DTO;
using TruckMove.API.DAL.Repositories.Primary;

namespace TruckMove.API.BLL.Services.Primary
{

    public class CompanyService : ICompanyService
    {
        private readonly ICompanyDataRepository companyDataRepository;

        public CompanyService(ICompanyDataRepository companyDataRepository)
        {
            this.companyDataRepository = companyDataRepository;
        }



        public IEnumerable<Company> Get()
        {
            return companyDataRepository.Get().Select(CompanyDto => new Company { Name = CompanyDto.Name });
            //  companyDataRepository.GetByIdAsync(10);
        }
    }
}