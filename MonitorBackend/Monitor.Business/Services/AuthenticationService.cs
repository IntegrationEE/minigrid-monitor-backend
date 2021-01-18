using System;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Monitor.Common.Enums;
using Monitor.Common.Models;
using Monitor.Infrastructure;
using Monitor.Domain.Entities;
using Monitor.Business.Extensions;

namespace Monitor.Business.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IBaseRepository _repository;

        public AuthenticationService(IBaseRepository repository)
        {
            _repository = repository;
        }

        public async Task<SignInModel> GetUser(int id)
        {
            return await _repository.GetQuery<User>(x => x.Id == id)
                .Select(x => new SignInModel
                {
                    Id = x.Id,
                    Login = x.Login,
                    Email = x.Email,
                    Role = x.Role,
                    CompanyId = x.CompanyId,
                    IsHeadOfCompany = x.IsHeadOfCompany
                }).SingleOrDefaultAsync();
        }

        public async Task<AuthenticationModel> Authenticate(string userName)
        {
            return await _repository.GetQuery<User>(x => x.Login == userName || x.Email == userName)
                .Select(x => new AuthenticationModel
                {
                    Id = x.Id,
                    Login = x.Login,
                    Email = x.Email,
                    Role = x.Role,
                    IsActive = x.IsActive,
                    Password = x.Password,
                }).SingleOrDefaultAsync();
        }

        public async Task<bool> ValidateRole(int userId, params RoleCode[] roles)
        {
            Expression<Func<User, bool>> exp = null;

            foreach (var role in roles)
            {
                exp = exp == null ?
                    x => x.Role == role :
                    exp.OR(z => z.Role == role);
            }

            return await _repository.GetQuery<User>(z => z.Id == userId && z.IsActive)
                .Where(exp)
                .AnyAsync();
        }

        public async Task<bool> CanManageSite(int userId, RoleCode role, int id)
        {
            Expression<Func<User, bool>> exp = null;

            if (role == RoleCode.PROGRAMME_MANAGER)
            { exp = x => x.Programmes.Any(z => z.Programme.Sites.Any(y => y.Id == id)); }
            else
            { exp = x => x.CompanyId.HasValue && x.Company.Sites.Any(x => x.Id == id); }

            return await _repository.GetQuery<User>(z => z.Id == userId)
                .Where(exp)
                .AnyAsync();
        }

        public async Task<bool> CanManageProgrammeIndicator(int userId, int id)
            => await _repository.Exists<User>(z => z.Id == userId && z.Programmes.Any(x => x.Programme.Indicators.Any(y => y.Id == id)));

        public async Task<bool> CanManageUser(int companyId, int userId)
            => await _repository.Exists<User>(z => z.Id == userId && z.CompanyId == companyId);

        public async Task<bool> IsHeadOfCompany(int userId, int companyId)
            => await _repository.Exists<User>(z => z.Id == userId && z.CompanyId == companyId && z.IsHeadOfCompany);

        public async Task<bool> IsActive(int id)
            => await _repository.Exists<User>(x => x.Id == id && x.IsActive);

        #region Dispose
        private bool _disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            { return; }

            if (disposing)
            {
                _repository?.Dispose();
            }

            _disposed = true;
        }
        #endregion
    }
}
