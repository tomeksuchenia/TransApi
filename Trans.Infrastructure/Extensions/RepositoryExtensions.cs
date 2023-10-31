using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trans.Core.Domain;
using Trans.Core.Repositories;
using Trans.Core.Repository;
using Trans.Infrastructure.Exceptions;

namespace Trans.Infrastructure.Extensions
{
    public static class RepositoryExtensions
    {
        public async static Task NullOrFail(this IUserRepository repository, string email)
        {
            var user = await repository.GetAsync(email);
            if(user != null)
            {
                throw new ServiceException(ErrorCodesService.EmailInUse,
                    $"User with email: '{email}' already exists.");
            }
            return;
        }

        public async static Task<User> GetOrFail(this IUserRepository repository, Guid userId)
        {
            var user = await repository.GetAsync(userId);
            if (user == null)
            {
                throw new ServiceException(ErrorCodesService.UserNotFound, $"User with id: {userId} was not found.");
            }
            return user;
        }

        public async static Task<User> GetOrFail(this IUserRepository repository, string email)
        {
            var user = await repository.GetAsync(email);
            if (user == null)
            {
                throw new ServiceException(ErrorCodesService.UserNotFound, $"User with email: {email} was not found.");
            }
            return user;
        }

        public async static Task<Company> GetOrFail(this ICompanyRepository repository, Guid id)
        {
            var company = await repository.GetAsync(id);
            if(company == null)
            {
                throw new ServiceException(ErrorCodesService.CompanyNotFound, $"Company with id: {id} was not found");
            }
            return company;
        }

        public async static Task NullOrFail(this ICompanyRepository repository, Guid companyId)
        {
            var company = await repository.GetAsync(companyId);
            if (company != null)
            {
                throw new ServiceException(ErrorCodesService.CompanyNotFound, $"Company with id {companyId} already exist.");
            }
        }
    }
}
