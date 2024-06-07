
using Microsoft.EntityFrameworkCore;
using TruckMove.API.DAL.Models;

namespace TruckMove.API.DAL.Repositories.Primary
{
    public class CompanyRepository : IContactRepository
    {
        private readonly DbContext _context;
        private readonly DbSet<Contact> _dbSet;
        public CompanyRepository(DbContextOptions<TrukMoveContext> options)
        {
            _context = new TrukMoveContext(options);
            _dbSet = _context.Set<Contact>();
        }

        public async Task<List<Contact>> GetContactsByCompany(int companyId)
        {
            return await _dbSet.Where(e => e.CompanyId== companyId & e.IsActive==true).ToListAsync();

        }
        public async Task<List<Contact>> GetAllAsync()
        {
            return await _dbSet.Where(e => e.IsActive & e.Company.IsActive==true).OrderByDescending(x => x.CreatedDate).ToListAsync();
        }

    }
}