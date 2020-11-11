using Identity.API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Identity.API.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
    	// public class ApplicationDbContextDesignTime : IDesignTimeDbContextFactory<ApplicationDbContext> {
    	// 	public ApplicationDbContext CreateDbContext(string[] args) {
    	// 		var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
    	// 		optionsBuilder.UseNpgsql("Server=127.0.0.1;Port=5433;Database=University;User Id=postgres;Password=postgres;");
     //
    	// 		return new ApplicationDbContext(optionsBuilder.Options);
    	// 	}
    	// }

}