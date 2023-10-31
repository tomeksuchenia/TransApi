using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trans.Infrastructure.Dto;

namespace Trans.Infrastructure.Services
{
    public interface ICompanyService
    {
        Task<CompanyDetailsDto> GetAsync(Guid id);
        Task<IEnumerable<CompanyDto>> BrowseAsync();
        Task RegisterAsync(Guid userId, string name, string Email, string TaxNumber,
            string country, string city, string postalCode, string street, string buildingNumber);
        Task UpdateAsync(CompanyDto company);
        Task DeleteAsync(Guid companyId);

    }
}
