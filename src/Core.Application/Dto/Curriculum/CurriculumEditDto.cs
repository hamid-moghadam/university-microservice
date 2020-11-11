using System.ComponentModel;
using Core.Domain.Enums;
using FluentValidation;
using Kasp.FormBuilder.Components.Handlers;

namespace Core.Application.Dto.Curriculum
{
    public class CurriculumEditDto
    {
        [DisplayName("ظرفیت کل")] public ushort Capacity { get; set; }
        [DisplayName("زمان شروع")] public string StartTime { get; set; }
        [DisplayName("زمان پایان")] public string EndTime { get; set; }
        [DisplayName("روز ارائه")] public CurriculumDay Day { get; set; }

        [DisplayName("استاد"), Select("c/api/v1/panel/Teacher")]
        public int TeacherId { get; set; }

        [DisplayName("نیمسال"), Select("c/api/v1/panel/Semester")]
        public int SemesterId { get; set; }

        [DisplayName("رشته"), Select("c/api/v1/panel/Field")]
        public int FieldId { get; set; }

        [DisplayName("درس"), Select("c/api/v1/panel/Course")]
        public int CourseId { get; set; }
    }


    public class CurriculumEditDtoValidator : AbstractValidator<CurriculumEditDto>
    {
        public CurriculumEditDtoValidator()
        {
            RuleFor(x => x.Capacity).NotEmpty();
            RuleFor(x => x.StartTime).Length(5).NotEmpty();
            RuleFor(x => x.EndTime).Length(5).NotEmpty();
            RuleFor(x => x.Day).NotNull();
            RuleFor(x => x.TeacherId).NotEmpty();
            RuleFor(x => x.SemesterId).NotEmpty();
            RuleFor(x => x.CourseId).NotEmpty();
            RuleFor(x => x.FieldId).NotEmpty();
        }
    }
}