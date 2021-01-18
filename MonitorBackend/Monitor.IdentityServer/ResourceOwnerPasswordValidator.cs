using System;
using IdentityModel;
using IdentityServer4.Models;
using System.Threading.Tasks;
using IdentityServer4.Validation;
using Monitor.Business.Services;

namespace Monitor.IdentityServer
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator, IDisposable
    {
        private readonly IAuthenticationService _service;

        public ResourceOwnerPasswordValidator(IAuthenticationService service)
        {
            _service = service;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var user = await _service.Authenticate(context.UserName);

            if (user == null)
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidRequest, "Account does not exist!");
            }
            else if (!user.IsActive)
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidRequest, "Account is not active!");
            }
            else if (string.IsNullOrWhiteSpace(user.Password) || !user.Password.Equals(context.Password))
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidRequest, "Username or password is wrong!");
            }
            else
            {
                context.Result = new GrantValidationResult(user.Id.ToString(), OidcConstants.AuthenticationMethods.Password);
            }
        }

        public void Dispose()
        {
            _service.Dispose();
        }
    }
}
