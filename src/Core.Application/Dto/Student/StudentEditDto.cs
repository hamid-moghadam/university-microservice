using System.ComponentModel;
using Core.Domain.Enums;
using FluentValidation;
using Kasp.FormBuilder.Components.Handlers;

namespace Core.Application.Dto.Student
{
    public class StudentEditDto
    {
        [DisplayName("کد دانشجویی")] public string Code { get; set; }

        [DisplayName("کد ملی")] public string NationalCode { get; set; }
        [DisplayName("نام")] public string Firstname { get; set; }
        [DisplayName("نام خانوادگی")] public string LastName { get; set; }

        [DisplayName("رشته"), Select("c/api/v1/panel/Field")]
        public int FieldId { get; set; }

        [DisplayName("نیمسال ورود"), Select("c/api/v1/panel/Semester")]
        public int SemesterId { get; set; }
    }

    public class StudentEditDtoValidator : AbstractValidator<StudentEditDto>
    {
        public StudentEditDtoValidator()
        {
            RuleFor(x => x.Code).NotEmpty().MaximumLength(30);
            RuleFor(x => x.NationalCode).NotEmpty().Length(10);
            RuleFor(x => x.Firstname).NotEmpty().MaximumLength(30);
            RuleFor(x => x.LastName).NotEmpty().MaximumLength(50);
            RuleFor(x => x.FieldId).NotEmpty();
        }
    }
}