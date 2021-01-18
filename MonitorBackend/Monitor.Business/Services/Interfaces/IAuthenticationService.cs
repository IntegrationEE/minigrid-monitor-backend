using System;
using System.Threading.Tasks;
using Monitor.Common.Enums;
using Monitor.Common.Models;

namespace Monitor.Business.Services
{
    public interface IAuthenticationService : IDisposable
    {
        Task<SignInModel> GetUser(int id);

        Task<AuthenticationModel> Authenticate(string userName);

        Task<bool> IsActive(int id);

        Task<bool> ValidateRole(int userId, params RoleCode[] roles);

        Task<bool> CanManageSite(int userId, RoleCode role, int id);

        Task<bool> CanManageProgrammeIndicator(int userId, int id);

        Task<bool> CanManageUser(int companyId, int userId);

        Task<bool> IsHeadOfCompany(int userId, int companyId);
    }
}
