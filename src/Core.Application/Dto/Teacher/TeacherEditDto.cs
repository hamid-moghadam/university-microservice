using System.ComponentModel;
using Core.Domain.Enums;
using FluentValidation;

namespace Core.Application.Dto.Teacher
{
    public class TeacherEditDto
    {
        [DisplayName("کد پرسنلی")] public string PersonnelId { get; set; }

        [DisplayName("کد ملی")] public string NationalCode { get; set; }
        [DisplayName("نام")] public string Firstname { get; set; }
        [DisplayName("نام خانوادگی")] public string LastName { get; set; }
    }


    public class TeacherEditDtoValidator : AbstractValidator<TeacherEditDto>
    {
        public TeacherEditDtoValidator()
        {
            RuleFor(x => x.PersonnelId).NotEmpty().MaximumLength(20);
            RuleFor(x => x.NationalCode).NotEmpty().Length(10);
            RuleFor(x => x.Firstname).NotEmpty().MaximumLength(30);
            RuleFor(x => x.LastName).NotEmpty().MaximumLength(50);
        }
    }
}