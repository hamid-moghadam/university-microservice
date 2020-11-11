using System.ComponentModel;
using Core.Application.Dto.Common;
using Core.Domain.Enums;

namespace Core.Application.Dto.Curriculum
{
    public class CurriculumPartialDto : BaseModelDto
    {
        [DisplayName("ظرفیت کل")] public ushort Capacity { get; set; }
        [DisplayName("ظرفیت تکمیل شده")] public ushort ReservedCapacity { get; set; }

        [DisplayName("زمان شروع")] public string StartTime { get; set; }
        [DisplayName("زمان پایان")] public string EndTime { get; set; }

        [DisplayName("روز ارائه")] public CurriculumDay Day { get; set; }

        [DisplayName("درس")] public string CourseTitle { get; set; }
        [DisplayName("رشته")] public string FieldTitle { get; set; }
        [DisplayName("نیمسال")] public string SemesterTitle { get; set; }
        [DisplayName("استاد")] public string TeacherFullName { get; set; }
    }
}