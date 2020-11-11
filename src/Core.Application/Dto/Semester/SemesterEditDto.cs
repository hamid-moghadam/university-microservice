using System;
using System.ComponentModel;
using Core.Domain.Enums;
using FluentValidation;
using Kasp.Data.Models.Helpers;

namespace Core.Application.Dto.Semester
{
    public class SemesterEditDto
    {
        [DisplayName("سال تحصیلی")] public string Year { get; set; }
        [DisplayName("نیمسال تحصیلی")] public SemesterType Type { get; set; }
        [DisplayName("تاریخ فعال سازی")] public DateTime? ActivatedAt { get; set; }
    }

    public class SemesterEditDtoValidator : AbstractValidator<SemesterEditDto>
    {
        public SemesterEditDtoValidator()
        {
            RuleFor(x => x.Year).Length(4).NotEmpty();
            RuleFor(x => x.Type).NotNull();
        }
    }
}