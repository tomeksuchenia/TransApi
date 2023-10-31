using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trans.Infrastructure.Dto;

namespace Trans.Infrastructure.Services
{
    public interface ICompanyDriverService
    {
        Task<IEnumerable<DriverDto>> GetAsync(Guid companyId);
        Task AddAsync(Guid companyId, string name, string fullname, string telephoneNumber);
        Task RemoveAsync(Guid companyId, Guid id);
    }
}
