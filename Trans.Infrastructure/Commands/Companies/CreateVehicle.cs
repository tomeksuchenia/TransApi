using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trans.Infrastructure.Commands.Companies
{
    public class CreateVehicle
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public string MaxVehicleWeight { get; set; }
    }
}
