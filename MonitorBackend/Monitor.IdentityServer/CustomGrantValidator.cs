using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using Microsoft.Extensions.Configuration;
using Monitor.Common;

namespace Monitor.IdentityServer
{
    public class CustomGrantValidator : IExtensionGrantValidator
    {
        private readonly string _guestSecret;

        public CustomGrantValidator(IConfiguration configuration)
        {
            _guestSecret = configuration[Constants.CONFIG_GUEST_SECRET];
        }

        public string GrantType => IdentityConfig.CUSTOM_GRANT_TYPE;
        /// <summary>
        /// Validate 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        #pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task ValidateAsync(ExtensionGrantValidationContext context)
        #pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            var guestSecret = context.Request.Raw.Get("guest_secret");

            if (string.IsNullOrWhiteSpace(guestSecret) || !guestSecret.Equals(_guestSecret))
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidRequest, "The authorization code is incorrect!");
            }
            else
            {
                context.Result = new GrantValidationResult("-1", GrantType);
            }
        }
    }
}
