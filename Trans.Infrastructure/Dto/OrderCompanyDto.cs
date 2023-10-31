using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trans.Infrastructure.Dto
{
    public class OrderCompanyDto
    {
        public string Name { get; set; }
        public AdressDto Adress { get; set; }
        public string TelephoneNumber { get; set; }
    }    
}
