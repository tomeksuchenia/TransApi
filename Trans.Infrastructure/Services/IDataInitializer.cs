using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trans.Infrastructure.Services
{
    public interface IDataInitializer
    {
        Task SeedAsync();
    }
}
