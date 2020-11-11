using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
 using Microsoft.Extensions.Configuration;
using System.IO;
 using Identity.API.Data;

 namespace Identity.API.Factories
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            // var config = new ConfigurationBuilder()
            //    .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
            //    .AddJsonFile("appsettings.json")
            //    .AddEnvironmentVariables()
            //    .Build();

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

            optionsBuilder.UseNpgsql("Server=127.0.0.1;Port=5433;Database=Identity;User Id=postgres;Password=postgres;", o => o.MigrationsAssembly("Identity.API"));

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}