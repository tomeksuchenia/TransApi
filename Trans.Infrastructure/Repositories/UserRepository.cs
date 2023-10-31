using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trans.Core.Domain;
using Trans.Core.Repository;
using Trans.Infrascture.EF_DB;

namespace Trans.Infrascture.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TransContext _context;
        public UserRepository(TransContext context)
        {
            _context = context;
        }
        public async Task<User> GetAsync(Guid userId)
            => await _context.Users.SingleOrDefaultAsync(x => x.Id == userId);    

        public async Task<User> GetAsync(string email)
            => await _context.Users.SingleOrDefaultAsync(x => x.Email == email);
        public async Task<IEnumerable<User>> BrowseAsync()
            => await _context.Users.ToListAsync();

        public async Task AddAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(User user)
        {
            var getUser = await GetAsync(user.Id);
            _context.Users.Remove(getUser);
            await _context.SaveChangesAsync();
        }

       
    }
}
