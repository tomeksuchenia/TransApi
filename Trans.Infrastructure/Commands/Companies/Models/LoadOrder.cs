using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trans.Infrastructure.Commands.Models
{
    public class LoadOrder
    {
        public string NameLoads { get;  set; }
        public int Weight { get;  set; }
        public int Width { get;  set; }
        public int Length { get;  set; }
        public int Height { get;  set; }
        public string Description { get;  set; }
    }
}
