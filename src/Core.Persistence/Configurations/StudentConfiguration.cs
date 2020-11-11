using Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Persistence.Configurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.Property(x => x.Code).HasMaxLength(30).IsRequired();
            builder.Property(x => x.UserId).HasMaxLength(100).IsRequired();
            builder.Property(x => x.FullName).HasMaxLength(100);

            builder.HasIndex(x => x.Code).IsUnique();
            builder.HasIndex(x => x.UserId).IsUnique();
        }
    }
}