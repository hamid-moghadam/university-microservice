using Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Persistence.Configurations
{
    public class TeacherConfiguration : IEntityTypeConfiguration<Teacher>
    {
        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
            builder.Property(x => x.PersonnelId).HasMaxLength(20).IsRequired();
            builder.Property(x => x.UserId).HasMaxLength(100).IsRequired();
            builder.Property(x => x.FullName).HasMaxLength(100);

            builder.HasIndex(x => x.PersonnelId).IsUnique();
            builder.HasIndex(x => x.UserId).IsUnique();
        }
    }
}