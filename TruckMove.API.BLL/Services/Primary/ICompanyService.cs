using TruckMove.API.BLL.Models.Primary;

namespace TruckMove.API.BLL.Services.Primary
{
    public interface ICompanyService
    {
        Company Get(int id);
        Company Update(Company company, Company existingCompany);
    }
}