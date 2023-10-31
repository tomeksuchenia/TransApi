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
    public class CompanyVehicleService : ICompanyVehicleService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public CompanyVehicleService(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<VehicleDto>> BrowseAsync(Guid companyId)
        {
            var company = await _companyRepository.GetAsync(companyId);
            return _mapper.Map<IEnumerable<VehicleDto>>(company.Vehicles);
        }

        public async Task AddAsync(Guid companyId, string brand, string model, string maxVehicleWeight)
        {
            var company = await _companyRepository.GetOrFail(companyId);
            company.AddVehicle(brand, model, maxVehicleWeight);
            await _companyRepository.UpdateAsync(company);
        }

        public async Task RemoveAsync(Guid companyId, Guid vehicleId)
        {
            var company = await _companyRepository.GetOrFail(companyId);
            company.DeleteVehicle(vehicleId);
            await _companyRepository.UpdateAsync(company);
        }
    }
}
