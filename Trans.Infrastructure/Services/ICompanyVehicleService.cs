using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trans.Infrastructure.Dto;

namespace Trans.Infrastructure.Services
{
    public interface ICompanyVehicleService
    {
        Task<IEnumerable<VehicleDto>> BrowseAsync(Guid companyId);
        Task AddAsync(Guid companyId, string brand, string model, string maxVehicleWeight);
        Task RemoveAsync(Guid companyId, Guid vehicleId);
    }
}