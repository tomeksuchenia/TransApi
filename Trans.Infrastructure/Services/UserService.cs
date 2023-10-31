using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trans.Core.Domain;
using Trans.Core.Repository;
using Trans.Infrastructure.Dto;
using Trans.Infrastructure.Extensions;
using BCrypt.Net;
using Microsoft.Extensions.Logging;
using Trans.Infrastructure.Exceptions;
using System.Runtime.Serialization;

namespace Trans.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IPasswordEncrypter _passwordEncrypter;

        public UserService(IUserRepository userRepository, IMapper mapper,
            IPasswordEncrypter passwordEncrypter)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _passwordEncrypter = passwordEncrypter;
        }
        public async Task<UserDto> GetAsync(Guid userId)
        {
            var user = await _userRepository.GetOrFail(userId);
            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> GetAsync(string email)
        {
            var user = await _userRepository.GetOrFail(email);
            return _mapper.Map<UserDto>(user);
        }

        public async Task<IEnumerable<UserDto>> BrowseAsync(int pageSize, int pageNumber)
        {
            var users = await _userRepository.BrowseAsync();

            var userDtos = _mapper.Map<IEnumerable<UserDto>>(users);

            if (pageSize == 0)
            {
                return _mapper.Map<IEnumerable<UserDto>>(users);
            }

            return _mapper.Map<IEnumerable<UserDto>>(users)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);
        }

        public async Task RegisterAsync(Guid userId, string email, string password, string username, string fullName, string role)
        {
            await _userRepository.NullOrFail(email);
            var salt = _passwordEncrypter.GetSalt();
            var passwordHash = _passwordEncrypter.GetHash(password, salt);
            var user = new User(userId, email, passwordHash, username, fullName, salt, role);
            await _userRepository.AddAsync(user);
        }

        public async Task LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetAsync(email);
            if (user == null)
            {
                throw new ServiceException(ErrorCodesService.InvalidCredentials, "You put invalid credentails.");
            }
            var passwordHash = _passwordEncrypter.GetHash(password, user.Salt);
            if(passwordHash != user.Password)
            {
                throw new ServiceException(ErrorCodesService.InvalidCredentials, "You put invalid credentails.");
            }

            return;
        }

        public async Task DeleteAsync(Guid userId)
        {
            var user = await _userRepository.GetOrFail(userId);
            await _userRepository.RemoveAsync(user);
        }
    }
}
