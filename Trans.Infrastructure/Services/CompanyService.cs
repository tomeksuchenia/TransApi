using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trans.Core.Domain;
using Trans.Core.Repositories;
using Trans.Infrastructure.Dto;
using Trans.Infrastructure.Extensions;

namespace Trans.Infrastructure.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;
        public CompanyService(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
        }
        public async Task<CompanyDetailsDto> GetAsync(Guid id)
        {
            var company = await _companyRepository.GetOrFail(id);
            return _mapper.Map<CompanyDetailsDto>(company);  
        }
        public async Task<IEnumerable<CompanyDto>> BrowseAsync()
        {
            var company = await _companyRepository.BrowseAsync();
            return _mapper.Map<IEnumerable<CompanyDto>>(company);
        }
        public async Task RegisterAsync(Guid companyId, string name, string Email, string TaxNumber,
            string country, string city, string postalCode, string street, string buildingNumber)
        {
            await _companyRepository.NullOrFail(companyId);
            var adress = Adress.Create(country, city, postalCode, street, buildingNumber);
            var company = new Company(companyId, name, Email, adress, TaxNumber);
            await _companyRepository.AddAsync(company);
         
        }

        public Task UpdateAsync(CompanyDto company)
        {
            throw new NotImplementedException();
        }
        public async Task DeleteAsync(Guid companyId)
        {
            var company = await _companyRepository.GetOrFail(companyId);
            await _companyRepository.DeleteAsync(companyId);
        }
    }
}
