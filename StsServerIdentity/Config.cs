// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace StsServerIdentity
{
    public class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email()
            };
        }

        public static IEnumerable<ApiResource> GetApis(string apiSecret)
        {
            return new ApiResource[]
            {
                new ApiResource("ProtectedApi", "Protected API")
                {
                    ApiSecrets =
                    {
                        new Secret(apiSecret.Sha256())
                    },
                    Scopes =
                    {
                        new Scope
                        {
                            Name = "api_scope",
                            ShowInDiscoveryDocument = false
                        }
                    },
                }
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new[]
            {
                new Client
                {
                    ClientId = "native.code",
                    ClientName = "Native Client (Code with PKCE)",

                    RedirectUris = { "https://127.0.0.1:45656" },
                    PostLogoutRedirectUris = { "https://127.0.0.1:45656" },

                    RequireClientSecret = false,

                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    AllowedScopes = { "openid", "profile", "email", "ProtectedApi" },

                    AllowOfflineAccess = true,
                    RefreshTokenUsage = TokenUsage.ReUse
                 }
            };
        }
    }
}