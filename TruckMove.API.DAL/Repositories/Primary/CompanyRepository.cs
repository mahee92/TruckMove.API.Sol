
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
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
         };
        public IEnumerable<CompanyModel> Get()
        {
            var xx = _context.Companies.ToList();
            return xx;
        }


    }
}