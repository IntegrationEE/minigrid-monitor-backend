using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Monitor.Common;

namespace Monitor.IdentityServer
{
    public static class IdentityServerBuilderExtensions
    {
        public static IIdentityServerBuilder AddCustomUserStore(this IIdentityServerBuilder builder)
        {
            builder.AddProfileService<ProfileService>();
            builder.AddResourceOwnerValidator<ResourceOwnerPasswordValidator>();
            builder.AddExtensionGrantValidator<CustomGrantValidator>();
            builder.AddDeveloperSigningCredential();
            builder.AddInMemoryApiScopes(IdentityConfig.GetApiScopes());
            builder.AddInMemoryIdentityResources(IdentityConfig.GetIdentityResources());
            builder.AddInMemoryApiResources(IdentityConfig.GetApiResources());
            builder.AddJwtBearerClientAuthentication();
            builder.AddInMemoryCaching();
            builder.AddInMemoryClients(IdentityConfig.GetClients());
            builder.AddCorsPolicyService<AllowCorsPolicyService>();

            return builder;
        }
    }

    public class AllowCorsPolicyService : ICorsPolicyService
    {
        private readonly string[] _allowedOrigins;

        public AllowCorsPolicyService(IConfiguration configuration)
        {
            _allowedOrigins = configuration.GetSection(Constants.CONFIG_ALLOWED_ORIGINS).Get<string[]>();
        }

        public async Task<bool> IsOriginAllowedAsync(string origin)
        {
            return await Task.Factory.StartNew(() => { return _allowedOrigins.Contains(origin); });
        }
    }
}
