using System;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Services;
using System.Collections.Generic;
using IdentityServer4.Extensions;
using Monitor.Common;
using Monitor.Common.Enums;
using Monitor.Common.Models;
using Monitor.Business.Services;

namespace Monitor.IdentityServer
{
    public class ProfileService : IProfileService, IDisposable
    {
        private readonly IAuthenticationService _service;

        public ProfileService(IAuthenticationService service)
        {
            _service = service;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            int.TryParse(context.Subject.GetSubjectId(), out int userId);

            var user = userId > 0 ?
                await _service.GetUser(userId) :
                new SignInModel
                {
                    Login = RoleCode.GUEST.ToString(),
                    Email = RoleCode.GUEST.ToString(),
                    Role = RoleCode.GUEST,
                    Id = userId,
                    IsAnonymous = true,
                    IsHeadOfCompany = false,
                };

            if (user != null)
            {
                var claims = context.IssuedClaims = new List<Claim>
                {
                    new Claim(Constants.USER_ROLE, user.Role.ToString()),
                    new Claim(Constants.USERNAME, user.Login),
                    new Claim(Constants.EMAIL, user.Email),
                    new Claim(Constants.USER_ID, user.Id.ToString()),
                    new Claim(Constants.IS_ANONYMUOS, user.IsAnonymous.ToString()),
                    new Claim(Constants.IS_HEAD_OF_COMPANY, user.IsHeadOfCompany.ToString()),
                };

                if (user.CompanyId.HasValue)
                {
                    claims.Add(new Claim(Constants.COMPANY, user.CompanyId.Value.ToString()));
                }

                context.IssuedClaims = claims;
            }
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            int.TryParse(context.Subject.GetSubjectId(), out int userId);

            context.IsActive = userId < 0 || await _service.IsActive(userId);
        }

        public void Dispose()
        {
            _service.Dispose();
        }
    }
}
