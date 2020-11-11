using System.ComponentModel;
using Core.Domain.Enums;
using FluentValidation;
using Kasp.FormBuilder.Components.Handlers;

namespace Core.Application.Dto.Field
{
    public class FieldEditDto
    {
        [DisplayName("عنوان")] public string Title { get; set; }
        [DisplayName("مقطع")] public DegreeType DegreeType { get; set; }

        [DisplayName("گروه‌ آموزشی"), Select("c/api/v1/panel/FieldGroup")]
        public int FieldGroupId { get; set; }
    }


    public class FieldEditDtoValidator : AbstractValidator<FieldEditDto>
    {
        public FieldEditDtoValidator()
        {
            RuleFor(x => x.Title).NotEmpty().MaximumLength(50);
            RuleFor(x => x.DegreeType).NotNull();
            RuleFor(x => x.FieldGroupId).NotEmpty();
        }
    }
}