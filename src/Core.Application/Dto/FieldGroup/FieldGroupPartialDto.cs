using System.ComponentModel;
using Core.Application.Dto.Common;
using Core.Domain.Enums;

namespace Core.Application.Dto.FieldGroup
{
    public class FieldGroupPartialDto : BaseModelDto
    {
        [DisplayName("عنوان")] public string Title { get; set; }
        
        // public ICollection<Field> Fields { get; set; }
    }
}