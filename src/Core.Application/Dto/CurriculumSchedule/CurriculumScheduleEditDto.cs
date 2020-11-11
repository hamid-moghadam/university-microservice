using System;
using System.ComponentModel;
using Core.Domain.Enums;
using FluentValidation;
using Kasp.FormBuilder.Components.Handlers;

namespace Core.Application.Dto.CurriculumSchedule
{
    public class CurriculumScheduleEditDto
    {
        [DisplayName("نیمسال تحصیلی"), Select("c/api/v1/panel/Semester")]
        public int CurrentSemesterId { get; set; }

        [DisplayName("شروع بازه نیمسال مجاز"), Select("c/api/v1/panel/Semester")]
        public int FromSemesterId { get; set; }

        [DisplayName("پایان بازه نیمسال مجاز"), Select("c/api/v1/panel/Semester")]
        public int ToSemesterId { get; set; }

        [DisplayName("تاریخ شروع")] public DateTime Start { get; set; }
        [DisplayName("تاریخ پایان")] public DateTime End { get; set; }

        [DisplayName("گروه آموزشی"), Select("c/api/v1/panel/FieldGroup")]
        public int FieldGroupId { get; set; }
    }

    public class CurriculumScheduleEditDtoValidator : AbstractValidator<CurriculumScheduleEditDto>
    {
        public CurriculumScheduleEditDtoValidator()
        {
            RuleFor(x => x.End).NotEmpty();
            RuleFor(x => x.Start).NotEmpty();
            RuleFor(x => x.FieldGroupId).NotEmpty();
            RuleFor(x => x.CurrentSemesterId).NotEmpty();
            RuleFor(x => x.ToSemesterId).NotEmpty();
            RuleFor(x => x.FromSemesterId).NotEmpty();
        }
    }
}