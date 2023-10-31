using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trans.Core.Domain;
using Trans.Infrascture.EF_DB;
using Trans.Infrastructure.Services;

namespace TestsEndToEnd
{
    public static class DataInitializer
    {
        public static void InitializeTestDatabase(TransContext context)
        {
            var passwordEncrypter = new PasswordEncrypter();
            var salt = passwordEncrypter.GetSalt();
            var hash = passwordEncrypter.GetHash("secret", salt);

            var user = new User(Guid.NewGuid(), "test1@email.com", hash, "test", "test", salt, "user");

            context.Users.Add(user);
            context.SaveChanges();
        }
    }
}
