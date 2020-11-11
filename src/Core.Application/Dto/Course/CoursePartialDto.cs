using System.ComponentModel;
using Core.Application.Dto.Common;
using Core.Domain.Enums;

namespace Core.Application.Dto.Course
{
    public class CoursePartialDto : BaseModelDto
    {
        [DisplayName("عنوان")] public string Title { get; set; }
        [DisplayName("کد")] public string Code { get; set; }

        [DisplayName("تعداد واحد عملی")] public ushort PracticalUnitCount { get; set; }
        [DisplayName("تعداد واحد تئوری")] public ushort TheoryUnitCount { get; set; }

        [DisplayName("نوع")] public CourseType Type { get; set; }
        
        [DisplayName("رشته")] public string FieldTitle { get; set; }
        
        // public ICollection<Curriculum> Curriculums { get; set; }
    }
}