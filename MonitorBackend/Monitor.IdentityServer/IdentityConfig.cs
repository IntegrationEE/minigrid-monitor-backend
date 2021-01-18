using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace Monitor.IdentityServer
{
    public static class IdentityConfig
    {
        public static string SecretHash => "".Sha256();
        public const string AUDIENCE = "API";
        public const string SWAGGER_CLIENT = "auth_swagger";
        public const string WEB_APP_CLIENT = "WebApp";
        public const string SWAGGER_SCOPE = "swagger";
        public const string SWAGGER_APP = "SwaggerApp";
        public const string API_FULL_ACCCESS = "api.full_access";
        public const string CUSTOM_GRANT_TYPE = "custom";

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
            };
        }

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>
            {
                new ApiScope
                {
                    Name = API_FULL_ACCCESS,
                    DisplayName = "API",
                },
                new ApiScope
                {
                    Name = SWAGGER_SCOPE,
                    DisplayName = "Swagger",
                }
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource
                {
                    Name = AUDIENCE,
                    Description = "Mini-Grid Monitor API",
                    Scopes = {
                        API_FULL_ACCCESS,
                        SWAGGER_SCOPE
                    }
                }
            };
        }

        // clients want to access resources (aka scopes)
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "AngularApp",
                    ClientName = "Angular Application",
                    ClientSecrets = { new Secret(SecretHash) },
                    AllowedGrantTypes = {
                        GrantType.ResourceOwnerPassword,
                        CUSTOM_GRANT_TYPE,
                    },
                    AlwaysSendClientClaims = true,
                    AlwaysIncludeUserClaimsInIdToken = true,
                    AllowAccessTokensViaBrowser = true,
                    AccessTokenLifetime = 3600,
                    AllowedScopes = {
                        API_FULL_ACCCESS,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                    },
                    AllowOfflineAccess = true,
                    RequireConsent = false,
                    RequireClientSecret = false,
                    RefreshTokenUsage = TokenUsage.OneTimeOnly,
                    RefreshTokenExpiration = TokenExpiration.Sliding,
                    UpdateAccessTokenClaimsOnRefresh = true
                },
                new Client
                {
                    ClientId = SWAGGER_CLIENT,
                    ClientName = "Swagger",
                    ClientSecrets = { new Secret(SecretHash) },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AlwaysSendClientClaims = true,
                    AlwaysIncludeUserClaimsInIdToken = true,
                    AllowAccessTokensViaBrowser = true,
                    AccessTokenLifetime = 3600,
                    AllowedScopes = {
                        SWAGGER_SCOPE,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.OpenId,
                    },
                    AllowOfflineAccess = true,
                    RequireConsent = false,
                    RequireClientSecret = false,
                    RefreshTokenUsage = TokenUsage.OneTimeOnly,
                    RefreshTokenExpiration = TokenExpiration.Sliding,
                    UpdateAccessTokenClaimsOnRefresh = true
                }
            };
        }
    }
}
