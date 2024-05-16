
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
        public CompanyModel Get(int id)
        {

            return _context.Companies.Where(x => x.CompanyId == id).FirstOrDefault();

        }

        // complete update method to update company
        public CompanyModel Update(CompanyModel companyModel)
        {

            companyModel.CompanyName = companyModel.CompanyName;
           _context.SaveChanges();


            return companyModel;
        }
    }
}