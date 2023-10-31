using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trans.Infrastructure.Dto
{
    public class CompanyDetailsDto : CompanyDto
    {
        public IEnumerable<DriverDto> Drivers { get; set; }
        public IEnumerable<VehicleDto> Vehicles { get; set; }
    }
}
