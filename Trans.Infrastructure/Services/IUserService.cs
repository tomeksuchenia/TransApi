using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trans.Core.Domain;
using Trans.Infrastructure.Dto;

namespace Trans.Infrastructure.Services
{
    public interface IUserService
    {
        Task<UserDto> GetAsync(Guid userId);
        Task<UserDto> GetAsync(string email);
        Task<IEnumerable<UserDto>> BrowseAsync(int pageSize, int pageNumber);
        Task RegisterAsync(Guid userId, string email, string password, string username, string fullName, string role);
        Task LoginAsync(string email, string password);
        Task DeleteAsync(Guid userId);
    }
}
