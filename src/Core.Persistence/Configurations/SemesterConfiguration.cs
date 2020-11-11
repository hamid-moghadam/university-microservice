using Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Persistence.Configurations
{
    public class SemesterConfiguration : IEntityTypeConfiguration<Semester>
    {
        public void Configure(EntityTypeBuilder<Semester> builder)
        {
            builder.Property(x => x.Year).HasMaxLength(4).IsRequired();

            builder.HasIndex(x => new {x.Year, x.Type}).IsUnique();
        }
    }
}