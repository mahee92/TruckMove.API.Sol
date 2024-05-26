
using Microsoft.EntityFrameworkCore;
using TruckMove.API.DAL.Models;

namespace TruckMove.API.DAL.Repositories.Primary
{
    public class CompanyRepository : IContactRepository
    {
        private readonly DbContext _context;
        private readonly DbSet<ContactModel> _dbSet;
        public CompanyRepository(DbContextOptions<TrukMoveLocalContext> options)
        {
            _context = new TrukMoveLocalContext(options);
            _dbSet = _context.Set<ContactModel>();
        }

        public async Task<List<ContactModel>> GetContactsByCompany(int companyId)
        {
            return await _dbSet.Where(e => e.CompanyId== companyId & e.IsActive==true).ToListAsync();

        }
        public async Task<List<ContactModel>> GetAllAsync()
        {
            return await _dbSet.Where(e => e.IsActive & e.Company.IsActive==true).OrderByDescending(x => x.CreatedDate).ToListAsync();
        }

    }
}