using IdentityModel;
using IdentityServer4.Models;

namespace Authorization.IdentityServer
{
    public static class Configuration
    {
        public static IEnumerable<Client> GetClients()
        {
            Console.WriteLine("Configuring clients...");

            return new List<Client>
                {
                   new Client
        {
            ClientId = "client_id",
            ClientSecrets = { new Secret("client_secret".ToSha256()) },
            AllowedGrantTypes = GrantTypes.ClientCredentials,
            AllowedScopes = { "OrdersAPI" },
            AllowOfflineAccess = true,
            AlwaysSendClientClaims = true,
            UpdateAccessTokenClaimsOnRefresh = true,
            AccessTokenType = AccessTokenType.Jwt,
            AccessTokenLifetime = 3600
        }
            };
        }

        public static IEnumerable<ApiResource> GetApiResources() =>
            new List<ApiResource>
            {
                new ApiResource("OrdersAPI")
            };

        public static IEnumerable<IdentityResource> GetIdentityResources() =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId()
            };
        //public static IEnumerable<ApiScope> GetApiScopes()
        //{
        //    return new[]
        //        {
        //            new ApiScope("OrdersAPI")
        //        };
        //}
    }
}
