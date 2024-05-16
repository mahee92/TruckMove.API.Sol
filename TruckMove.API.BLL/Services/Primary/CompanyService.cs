using TruckMove.API.BLL.Models.Primary;
using TruckMove.API.DAL.Models;
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



        public Company Get(int id)
        {
            var company = companyDataRepository.Get(id);

            if (company == null)
                return null;
            
            var companyDto = new Company
            {
                CompanyId = company.CompanyId,
                CompanyName = company.CompanyName,
            };

            return companyDto;

        }



        // write the Update method to update company
        public Company Update(Company company, Company existingCompany)
        {
            var companyModel = new CompanyModel
            {
                CompanyId = company.CompanyId,
                CompanyName = company.CompanyName,
            };

            //companyDataRepository.Update(companyModel);

            return company;
        }
    }
}