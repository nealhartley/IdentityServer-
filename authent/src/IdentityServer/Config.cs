// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using IdentityServer4;
using System.Collections.Generic;

namespace IdentityServer
{
    public static class Config
    {
        public static IEnumerable<ApiResource> Apis =>    
            new List<ApiResource>
            {
                new ApiResource("api1", "My API")
            };
    

        public static IEnumerable<Client> Clients => 
            new List<Client>
            {
                new Client 
                {
                    ClientId = "client",
    
                    //no interactive user, use the clientid/secret for auth
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    // secret for authentication
                    ClientSecrets = 
                    {
                        new Secret("secret".Sha256())
                    },

                    //scopes that client has access to
                    AllowedScopes = {"api1"}
                },
                new Client 
                {
                    ClientId = "mvc",
                    ClientSecrets = { new Secret("secret".Sha256()) },

                    AllowedGrantTypes = GrantTypes.Code,
                    RequireConsent = false,
                    RequirePkce = true,

                    //where to redirect after login
                    RedirectUris = { "http://localhost:8000/signin-oidc" },
                    // where to redirect to after logout
                    PostLogoutRedirectUris = { "http://localhost:8000/signout-callback-oidc" },

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "api1"                    
                    },

                    AllowOfflineAccess = true
                
                }         
        };


        public static IEnumerable<IdentityResource> Ids =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };       
        
    }
    
}