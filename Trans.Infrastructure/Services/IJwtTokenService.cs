using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trans.Infrastructure.Dto;

namespace Trans.Infrastructure.Services
{
    public interface IJwtTokenService
    {
        JwtDto Create(Guid id, string role);
    }
}
