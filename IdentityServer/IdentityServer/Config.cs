using IdentityServer4.Models;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using IdentityModel;

namespace IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        }

        public static IEnumerable<ApiResource> GetApis()
        {
            return new ApiResource[]
            {
                 new ApiResource("alevelwebsite.com")
                {
                    Scopes = new List<Scope>
                    {
                        new Scope("mvc")
                    },
                },

                new ApiResource("catalog")
                {
                    Scopes = new List<Scope>
                    {
                        new Scope("catalog.catalogbff"),
                        new Scope("catalog.cataloggame"),
                        new Scope("catalog.catalogplatform"),
                        new Scope("catalog.cataloggenre"),
                        new Scope("catalog.catalogpublisher"),
                    }
                },

                new ApiResource("basket")
                {
                    Scopes = new List<Scope>
                    {
                        new Scope("basket.basketbff")
                    }
                },

                new ApiResource("order")
                {
                    Scopes = new List<Scope>
                    {
                        new Scope("order.order")
                    }
                }

            };
        }

        public static IEnumerable<Client> GetClients(IConfiguration configuration)
        {
            return new[]
            {
                 new Client
                {
                    ClientId = "mvc_pkce",
                    ClientName = "MVC PKCE Client",
                    AllowedGrantTypes = GrantTypes.Code,
                    ClientSecrets = {new Secret("secret".Sha256())},
                    RedirectUris = { $"{configuration["MvcUrl"]}/signin-oidc"},
                    AllowedScopes = {"openid", "profile", "mvc"},
                    RequirePkce = true,
                    RequireConsent = false
                },
                new Client
                {
                    ClientId = "catalog",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new Secret("secret".ToSha256())
                    },
                },

                new Client
                {
                    ClientId = "catalogswaggerui",
                    ClientName = "Catalog Swagger UI",
                    AllowedGrantTypes= GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris = {$"{configuration["CatalogApi"]}/swagger/oauth2-redirect.html"},
                    PostLogoutRedirectUris = {$"{configuration["CatalogApi"]}/swagger" },
                    AllowedScopes =
                    {
                       "mvc", "catalog.catalogbff", "catalog.cataloggame", "catalog.catalogplatform", "catalog.cataloggenre", "catalog.cataloggenre", "catalog.catalogpublisher"
                    }
                },
                new Client
                {
                    ClientId = "basket",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new Secret("secret".ToSha256())
                    },
                },

                new Client
                {
                    ClientId = "basketswaggerui",
                    ClientName = "Basket Swagger UI",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    RedirectUris = {$"{configuration["BasketApi"]}/swagger/oauth2-redirect.html" },
                    PostLogoutRedirectUris = {$"{configuration["BasketApi"]}/swagger"},
                    AllowedScopes =
                    {
                        "mvc", "basket.basketbff", "order.order"
                    }
                },

                new Client
                {
                    ClientId = "order",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new Secret("secret".ToSha256())
                    },

                },
                 new Client
                {
                    ClientId = "orderswaggerui",
                    ClientName = "Order Swagger UI",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    RedirectUris = {$"{configuration["OrderApi"]}/swagger/oauth2-redirect.html" },
                    PostLogoutRedirectUris = {$"{configuration["OrderApi"]}/swagger"},
                    AllowedScopes =
                    {
                        "mvc", "order.order", "basket.basketbff"
                    }
                },
            };
        }
    }
}