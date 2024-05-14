using TruckMove.API.DAL.DTO;
using TruckMove.API.DAL.Models;

namespace TruckMove.API.DAL.Repositories.Primary
{
    public interface ICompanyDataRepository
    {
        IEnumerable<CompanyModel> Get();
    }
}