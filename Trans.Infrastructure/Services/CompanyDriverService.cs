using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trans.Core.Repositories;
using Trans.Infrastructure.Dto;
using Trans.Infrastructure.Extensions;

namespace Trans.Infrastructure.Services
{
    public class CompanyDriverService : ICompanyDriverService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public CompanyDriverService(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
        }
        public async Task AddAsync(Guid companyId, string name, string fullname, string telephoneNumber)
        {
            var company = await _companyRepository.GetOrFail(companyId);
            company.AddDriver(name, fullname, telephoneNumber);
            await _companyRepository.UpdateAsync(company);
        }

        public async Task<IEnumerable<DriverDto>> GetAsync(Guid companyId)
        {
            var company = await _companyRepository.GetOrFail(companyId);
            return _mapper.Map<IEnumerable<DriverDto>>(company.Drivers);
        }

        public async Task RemoveAsync(Guid companyId, Guid driverId)
        {
            var company = await _companyRepository.GetOrFail(companyId);
            company.DeleteDriver(driverId);
            await _companyRepository.UpdateAsync(company);
        }

        
    }
}
