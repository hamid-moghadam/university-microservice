using Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Persistence.Configurations
{
    public class CurriculumConfiguration : IEntityTypeConfiguration<Curriculum>
    {
        public void Configure(EntityTypeBuilder<Curriculum> builder)
        {
            builder.Property(x => x.StartTime).HasMaxLength(5).IsFixedLength().IsRequired();
            builder.Property(x => x.EndTime).HasMaxLength(5).IsFixedLength().IsRequired();
        }
    }
}