using System.ComponentModel;
using Core.Application.Dto.Common;
using Core.Domain.Enums;

namespace Core.Application.Dto.Field
{
    public class FieldPartialDto : BaseModelDto
    {
        [DisplayName("عنوان")] public string Title { get; set; }
        [DisplayName("مقطع")] public DegreeType DegreeType { get; set; }

        [DisplayName("گروه آموزشی")]
        public string FieldGroupTitle { get; set; }
    }
}