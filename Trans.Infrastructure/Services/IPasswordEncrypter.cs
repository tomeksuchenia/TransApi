using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trans.Infrastructure.Services
{
    public interface IPasswordEncrypter
    {
        string GetSalt();
        string GetHash(string password, string salt);
    }
}
