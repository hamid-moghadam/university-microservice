using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Identity.API.Factories
{
    public class PersistedGrantDbContextFactory : IDesignTimeDbContextFactory<PersistedGrantDbContext>
    {
        public PersistedGrantDbContext CreateDbContext(string[] args)
        {
            // var config = new ConfigurationBuilder()
            //     .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
            //     .AddJsonFile("appsettings.json")
            //     .AddEnvironmentVariables()
            //     .Build();

            var optionsBuilder = new DbContextOptionsBuilder<PersistedGrantDbContext>();
            var operationOptions = new OperationalStoreOptions();

            optionsBuilder.UseNpgsql("Server=127.0.0.1;Port=5433;Database=Identity;User Id=postgres;Password=postgres;", o => o.MigrationsAssembly("Identity.API"));

            return new PersistedGrantDbContext(optionsBuilder.Options, operationOptions);
        }
    }
}