using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trans.Core.Domain;

namespace Trans.Core.Repositories
{
    public interface ICompanyRepository
    {
        public Task<Company> GetAsync(Guid companyId);
        public Task<IEnumerable<Company>> BrowseAsync();
        public Task AddAsync(Company company);
        public Task UpdateAsync(Company company);
        public Task DeleteAsync(Guid companyId);

    }
}
