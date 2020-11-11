using System;
using System.ComponentModel;
using Core.Application.Dto.Common;
using Core.Domain.Enums;

namespace Core.Application.Dto.Semester
{
    public class SemesterPartialDto : BaseModelDto
    {
        [DisplayName("سال تحصیلی")] public string Year { get; set; }
        [DisplayName("نیمسال تحصیلی")] public SemesterType Type { get; set; }
        [DisplayName("تاریخ فعال سازی")] public DateTime? ActivatedAt { get; set; }
        [DisplayName("عنوان")] public string Title { get; set; }

        public int IntegerTitle { get; set; }
    }
}