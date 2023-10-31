using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trans.Core.Domain;
using Trans.Core.Repositories;
using Trans.Infrascture.EF_DB;

namespace Trans.Infrastructure.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly TransContext _context;
        public CompanyRepository(TransContext context)
        {
            _context = context;
        }

        public async Task<Company> GetAsync(Guid Id)
            => await _context.Companies.Include(c => c.Vehicles).Include(c => c.Drivers).Include(c => c.Orders).SingleOrDefaultAsync(x => x.Id == Id);

        public async Task<IEnumerable<Company>> BrowseAsync()
            => await _context.Companies.ToListAsync();

        public async Task AddAsync(Company company)
        {
            _context.Companies.AddAsync(company);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Company company)
        {
            _context.Companies.Update(company);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid Id)
        {
            var company = await _context.Companies.SingleOrDefaultAsync(x => x.Id == Id);
            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();
        }
    }
}
