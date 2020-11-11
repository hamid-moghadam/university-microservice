using System.Collections.Generic;
using IdentityServer4;
using IdentityServer4.Models;

namespace Identity.API.Configuration
{
    public class Config
    {
        public static IEnumerable<ApiResource> GetApis()
        {
            return new List<ApiResource>
            {
                new ApiResource("panel", "Panel Service")
                    {Scopes = {"panel", "curriculum"}},
                new ApiResource("curriculum", "Curriculum Service")
                    {Scopes = {"curriculum", "aggregator"}},
                new ApiResource("aggregator", "Http Aggregator Service")
                    {Scopes = {"curriculum", "aggregator"}}
            };
        }

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>
            {
                new ApiScope("panel", "Panel Scope"),
                new ApiScope("curriculum", "Curriculum Scope"),
                new ApiScope("aggregator", "Http Aggregator Scope"),
            };
        }

        public static IEnumerable<IdentityResource> GetResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        }


        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "vue",
                    ClientName = "curriculums",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "curriculum",
                        "aggregator",
                    },
                    AllowOfflineAccess = true,
                    RefreshTokenUsage = TokenUsage.OneTimeOnly,
                    RequireClientSecret = false,
                    AllowRememberConsent = false
                },
                new Client
                {
                    ClientId = "angular",
                    ClientName = "panel",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "panel",
                        "curriculum"
                    },
                    AllowOfflineAccess = true,
                    RefreshTokenUsage = TokenUsage.OneTimeOnly,
                    RequireClientSecret = false,
                    AllowRememberConsent = false
                }
            };
        }
    }
}