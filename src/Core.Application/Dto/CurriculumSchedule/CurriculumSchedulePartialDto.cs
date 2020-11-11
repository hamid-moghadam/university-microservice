using System;
using System.ComponentModel;
using Core.Application.Dto.Common;
using Core.Domain;
using Core.Domain.Enums;

namespace Core.Application.Dto.CurriculumSchedule
{
    public class CurriculumSchedulePartialDto : BaseModelDto
    {
        [DisplayName("نیمسال تحصیلی")] public string CurrentSemesterTitle { get; set; }

        [DisplayName("شروع بازه نیمسال مجاز")] public string FromSemesterTitle { get; set; }

        [DisplayName("پایان بازه نیمسال مجاز")] public string ToSemesterTitle { get; set; }

        [DisplayName("تاریخ شروع")] public DateTime Start { get; set; }
        [DisplayName("تاریخ پایان")] public DateTime End { get; set; }

        [DisplayName("گروه آموزشی")] public string FieldGroupTitle { get; set; }
    }
}