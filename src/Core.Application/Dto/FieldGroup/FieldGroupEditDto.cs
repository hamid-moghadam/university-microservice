using System.ComponentModel;
using Core.Domain.Enums;
using FluentValidation;

namespace Core.Application.Dto.FieldGroup
{
    public class FieldGroupEditDto
    {
        [DisplayName("عنوان")] public string Title { get; set; }
    }

    public class FieldGroupEditDtoValidator : AbstractValidator<FieldGroupEditDto>
    {
        public FieldGroupEditDtoValidator()
        {
            RuleFor(x => x.Title).NotEmpty().MaximumLength(20);
        }
    }
}