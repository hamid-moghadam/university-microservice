using System.ComponentModel;
using Core.Application.Dto.Common;
using Core.Domain.Enums;

namespace Core.Application.Dto.Teacher
{
    public class TeacherPartialDto : BaseModelDto
    {
        [DisplayName("کد پرسنلی")] public string PersonnelId { get; set; }
        [DisplayName("کد کاربری")] public string UserId { get; set; }

        [DisplayName("کد ملی")] public string NationalCode { get; set; }
        // [DisplayName("نام")] public string Firstname { get; set; }
        // [DisplayName("نام خانوادگی")] public string LastName { get; set; }
        [DisplayName("نام و نام خانوادگی")] public string FullName { get; set; }
    }
}