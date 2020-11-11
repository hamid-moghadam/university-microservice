using Curriculum.API.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Curriculum.API.Data.Configurations
{
    public class StudentCurriculumConfiguration : IEntityTypeConfiguration<StudentCurriculum>
    {
        public void Configure(EntityTypeBuilder<StudentCurriculum> builder)
        {
            builder.OwnsOne(x => x.Curriculum, sa =>
            {
                sa.OwnsOne(y => y.Course);
                sa.OwnsOne(y => y.Field);
                sa.OwnsOne(y => y.Semester);
                sa.OwnsOne(y => y.Teacher);
            });
            builder.OwnsOne(x => x.Student);
            builder.Property(x => x.StatusDescription).HasMaxLength(50).HasDefaultValue("");
        }
    }
}