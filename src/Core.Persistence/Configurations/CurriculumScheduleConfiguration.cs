using Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Persistence.Configurations
{
    public class CurriculumScheduleConfiguration : IEntityTypeConfiguration<CurriculumSchedule>
    {
        public void Configure(EntityTypeBuilder<CurriculumSchedule> builder)
        {
            builder.HasOne(x => x.CurrentSemester)
                .WithMany(x => x.CurriculumSchedules)
                .HasForeignKey(x => x.CurrentSemesterId)
                .IsRequired();

            builder.HasOne(x => x.FromSemester)
                .WithMany()
                .HasForeignKey(x => x.FromSemesterId)
                .IsRequired();
            builder.HasOne(x => x.ToSemester)
                .WithMany()
                .HasForeignKey(x => x.ToSemesterId)
                .IsRequired();


            // builder.OwnsOne(x => x.AllowedSemesterRange);
        }
    }
}