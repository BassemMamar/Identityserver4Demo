using System.Collections.Generic;
using System.Security.Claims;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using static IdentityServer4.IdentityServerConstants;

 
namespace AuthServer
{

    public class Config
    {
        // Scopes define the resources in your system that you want to protect, e.g. APIs.
        // scopes represent something you want to protect and that clients want to access
        public static IEnumerable<ApiResource> GetApiResources() => new List<ApiResource>
            {
                new ApiResource("ApiDemo", "My Demo API")
            };

        // define a client that can access this API.
        public static IEnumerable<Client> GetClients() => new List<Client>
        {
            new Client
            {
                ClientId = "ConsoleClientDemo",

                // no interactive user, use the clientid/secret for authentication
                AllowedGrantTypes = GrantTypes.ClientCredentials,  

                // secret for authentication
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },

                // scopes that client has access to
                AllowedScopes = { "ApiDemo" }
            },
            new Client
            {
                ClientId = "ro.ConsoleClientDemo",

                // no interactive user, use the clientid/secret for authentication
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword, 

                // secret for authentication
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },

                // scopes that client has access to
                AllowedScopes = { "ApiDemo" }
            },

            // OpenID Connect implicit flow client (MVC)
            new Client
            {
                ClientId = "MVC",
                ClientName = "MVC Client",
                AllowedGrantTypes = GrantTypes.Implicit,

                // where to redirect to after login
                RedirectUris = { "http://localhost:5002/signin-oidc" },

                // where to redirect to after logout
                PostLogoutRedirectUris = { "http://localhost:5002/signout-callback-oidc" },

                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile
                }
            }
        };

        public static List<TestUser> GetUsers() => new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "alice",
                    Password = "password",

                    Claims = new []
                    {
                        new Claim("name", "Alice"),
                        new Claim("website", "https://alice.com")
                    }
                },
                new TestUser
                {
                    SubjectId = "2",
                    Username = "bob",
                    Password = "password",

                    Claims = new []
                    {
                        new Claim("name", "Bob"),
                        new Claim("website", "https://bob.com")
                    }
                }
            };

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
        }
    }

    //   public class Config
    //{ 
    //	public static IEnumerable<Scope> GetScopes()
    //	{
    //		return new List<Scope>
    //		{

    //            new Scope
    //			{
    //				Name = StandardScopes.OfflineAccess,
    //				Description = "Offline Access"
    //            },
    //		    new Scope
    //		    {
    //		    Name = "api1",
    //		    Description = "My API"
    //		},
    //            new Scope
    //            {
    //                Name = "api1",
    //                Description = "My API"
    //            }
    //        };
    //	}


    //        public static IEnumerable<Client> GetClients() => new List<Client>
    //        {
    //            new Client
    //            {
    //                ClientId = "google",
    //                ClientSecrets =
    //                {
    //                    new Secret("secret".Sha256())
    //                },
    //                AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,

    //                AllowedScopes =
    //                {
    //                   "offline_access",
    //                    "api1"
    //                }
    //            },
    //            new Client
    //            {
    //                ClientId = "resourceOwner",
    //                ClientSecrets =
    //                {
    //                    new Secret("secret".Sha256())
    //                },
    //                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

    //                AllowedScopes =
    //                {
    //                    "offline_access",
    //                    "api1"
    //                }
    //            }
    //        };

    //        public static List<TestUser> GetUsers()
    //	{
    //		return new List<TestUser>
    //		{
    //			new TestUser
    //            {
    //				SubjectId = "0001",
    //				Username = "behrooz",
    //				Password = "mypass" 
    //			},
    //			new TestUser
    //            {
    //                SubjectId = "0002",
    //				Username = "mahnaz",
    //				Password = "anotherpass" 
    //			}
    //		};
    //	}
    //}

}
