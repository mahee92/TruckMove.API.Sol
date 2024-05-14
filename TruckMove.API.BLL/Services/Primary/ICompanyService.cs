using TruckMove.API.BLL.Models.Primary;

namespace TruckMove.API.BLL.Services.Primary
{
    public interface ICompanyService
    {
        IEnumerable<Company> Get();
    }
}