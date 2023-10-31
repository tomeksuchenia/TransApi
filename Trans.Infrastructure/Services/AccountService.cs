using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trans.Core.Repository;
using Trans.Infrastructure.Exceptions;
using Trans.Infrastructure.Extensions;

namespace Trans.Infrastructure.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordEncrypter _passwordEncrypter;

        public AccountService(IUserRepository userRepository, IPasswordEncrypter passwordEncrypter)
        {
            _userRepository = userRepository;
            _passwordEncrypter = passwordEncrypter;
        }
        public async Task ChangePassword(Guid userId, string oldPassword, string newPassword, string repeatNewPassword)
        {
            var user = await _userRepository.GetOrFail(userId);
            var userSalt = user.Salt;
            var newPasswordHash = _passwordEncrypter.GetHash(oldPassword, userSalt);
            if(user.Password != newPasswordHash)
            {
                throw new ServiceException(ErrorCodesService.InvalidCredentials, "Inavlid credentials");
            }
            if(newPassword != repeatNewPassword)
            {
                throw new ServiceException("password_different", "New password different");
            }
            var salt = _passwordEncrypter.GetSalt();
            var hash = _passwordEncrypter.GetHash(newPassword, salt);
            user.SetPassword(hash, salt);
            await _userRepository.UpdateAsync(user);
        }
    }
}
