using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trans.Core.Domain;

namespace Trans.Core.Repository
{
    public interface IUserRepository
    {
        public Task<User> GetAsync(Guid userId);
        public Task<User> GetAsync(string email);
        public Task<IEnumerable<User>> BrowseAsync();
        public Task AddAsync(User user);
        public Task UpdateAsync(User user);
        public Task RemoveAsync(User user);
        
    }
}
