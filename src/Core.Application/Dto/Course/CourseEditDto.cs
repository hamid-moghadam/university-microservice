using System.ComponentModel;
using Core.Domain.Enums;
using FluentValidation;
using Kasp.FormBuilder.Components.Handlers;

namespace Core.Application.Dto.Course
{
    public class CourseEditDto
    {
        [DisplayName("عنوان")] public string Title { get; set; }
        [DisplayName("کد")] public string Code { get; set; }

        [DisplayName("تعداد واحد عملی")] public ushort PracticalUnitCount { get; set; }
        [DisplayName("تعداد واحد تئوری")] public ushort TheoryUnitCount { get; set; }

        [DisplayName("نوع")] public CourseType Type { get; set; }

        [DisplayName("رشته"), Select("c/api/v1/panel/Field")]
        public int FieldId { get; set; }
    }

    public class CourseEditDtoValidator : AbstractValidator<CourseEditDto>
    {
        public CourseEditDtoValidator()
        {
            RuleFor(x => x.Title).NotEmpty().MaximumLength(30);
            RuleFor(x => x.Code).NotEmpty().MaximumLength(10);
            RuleFor(x => x.Type).NotNull();
            RuleFor(x => x.PracticalUnitCount).NotEmpty();
            RuleFor(x => x.TheoryUnitCount).NotEmpty();
            RuleFor(x => x.FieldId).NotEmpty();
        }
    }
}