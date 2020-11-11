using Core.Domain;
using Curriculum.API.Data.Models;
using Kasp.Data.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Curriculum.API.Persistence
{
    public class CurriculumDbContext : KDbContext<CurriculumDbContext>
    {
        public CurriculumDbContext(DbContextOptions<CurriculumDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CurriculumDbContext).Assembly);
        }

        public DbSet<StudentCurriculum> StudentCurriculums { get; set; }
    }

    public class CoreDbContextDesignTime : IDesignTimeDbContextFactory<CurriculumDbContext>
    {
        public CurriculumDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CurriculumDbContext>();
            optionsBuilder.UseNpgsql(
                "Server=127.0.0.1;Port=5433;Database=Curriculum;User Id=postgres;Password=postgres;");

            return new CurriculumDbContext(optionsBuilder.Options);
        }
    }
}