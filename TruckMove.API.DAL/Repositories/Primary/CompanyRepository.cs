
using TruckMove.API.DAL.Models;

namespace TruckMove.API.DAL.Repositories.Primary
{
    public class CompanyDataRepository : ICompanyDataRepository
    {
        private readonly TrukMoveLocalContext _context;
        public CompanyDataRepository(TrukMoveLocalContext context)
        {
            _context = context;
        }       
       
    }
}