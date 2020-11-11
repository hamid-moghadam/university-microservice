using Core.Application.Dto.Course;
using Core.Application.Dto.Field;
using Core.Application.Dto.Semester;
using Core.Application.Dto.Teacher;
using Core.Domain.Enums;
using Kasp.Data.Models.Helpers;

namespace Core.Application.Dto.Curriculum
{
    public class CurriculumDto : IModel
    {
        public int Id { get; set; }
        public ushort Capacity { get; set; }
        public ushort ReservedCapacity { get; set; }

        public string StartTime { get; set; }
        public string EndTime { get; set; }

        public int RemainingCapacity { get; set; }
        public bool IsCapacityCompleted { get; set; }
        
        public CurriculumDay Day { get; set; }

        // public int CourseId { get; set; }
        public CoursePartialDto Course { get; set; }

        // public int FieldId { get; set; }
        public FieldPartialDto Field { get; set; }

        // public int TeacherId { get; set; }
        public TeacherPartialDto Teacher { get; set; }

        public SemesterPartialDto Semester { get; set; }
    }
}