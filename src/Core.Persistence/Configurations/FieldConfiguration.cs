using Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Persistence.Configurations
{
    public class FieldConfiguration:IEntityTypeConfiguration<Field>
    {
        public void Configure(EntityTypeBuilder<Field> builder)
        {
            builder.Property(x => x.Title).HasMaxLength(50).IsRequired();
            
            builder.HasIndex(x => x.Title).IsUnique();
        }
    }
}