using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Mappers;
using Duende.IdentityServer.Models;
using Microsoft.EntityFrameworkCore;

namespace id_server.Seed
{
    public static class IdentityServerSeeder
    {
        public static void SeedClients(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
            context.Database.Migrate();

            if (!context.Clients.Any())
            {
                var client = new Client
                {
                    ClientId = "sample-client",
                    ClientSecrets = { new Secret("sample-secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    AllowedScopes = { "api1", "offline_access" }, // offline_access enables refresh tokens
                    AllowOfflineAccess = true // Enable refresh tokens
                };
                context.Clients.Add(client.ToEntity());
                context.SaveChanges();
            }

            if (!context.ApiScopes.Any())
            {
                var apiScope = new ApiScope("api1", "Sample API");
                context.ApiScopes.Add(apiScope.ToEntity());
                context.SaveChanges();
            }
        }
    }
}
