using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trans.Infrastructure.Dto
{
    public class JwtDto
    {
        public string Token { get; set; }
        public DateTime ExpiresTime { get; set; }
    }
}
