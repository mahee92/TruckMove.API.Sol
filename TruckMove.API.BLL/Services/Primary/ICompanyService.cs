using TruckMove.API.BLL.Models.Primary;
using TruckMove.API.DAL.Models;

namespace TruckMove.API.BLL.Services.Primary
{
    public interface ICompanyService
    {
        Task<Response<Company>> AddAsync(Company company);
        Task<Response<Company>> GetAsync(int id);
        Task<Response<UpdateCompany>> UpdateAsync(UpdateCompany updatedCompany);

        Task<Response<Company>> DeleteAsync(int id);
        Task<Response<CompanyModel>> GetAllAsync();
    }
}