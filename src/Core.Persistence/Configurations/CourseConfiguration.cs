using Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Persistence.Configurations
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.Property(x => x.Code).HasMaxLength(10).IsRequired();
            builder.Property(x => x.Title).HasMaxLength(30).IsRequired();
            
            builder.HasIndex(x => x.Title).IsUnique();
            builder.HasIndex(x => x.Code).IsUnique();
        }
    }
}