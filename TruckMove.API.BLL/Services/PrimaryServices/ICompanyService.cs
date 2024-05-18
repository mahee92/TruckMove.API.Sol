using Microsoft.AspNetCore.JsonPatch;
using TruckMove.API.BLL.Models.Primary;
using TruckMove.API.DAL.Models;

namespace TruckMove.API.BLL.Services.Primary
{
    public interface ICompanyService
    {
        Task<Response<CompanyDto>> AddAsync(CompanyDto company);
        Task<Response<CompanyDto>> GetAsync(int id);
        Task<Response<CompanyDtoUpdate>> UpdateAsync(CompanyDtoUpdate updatedCompany);

        Task<Response<CompanyDto>> DeleteAsync(int id);
        Task<Response<CompanyModel>> GetAllAsync();

        Task<bool> ValidateCompanyById(int id);
        Task UpdateCompanyPartialAsync(int id, JsonPatchDocument<CompanyDtoUpdate> patchDoc);
        Task<Response<ContactModel>> GetContactsByCompany(int companyId);
    }
}