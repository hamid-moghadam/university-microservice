using Core.Application.Dto.Field;
using Core.Application.Dto.Semester;

namespace Core.Application.Dto.Student
{
    public class StudentDto : StudentPartialDto
    {
        public int Id { get; set; }
        public FieldDto Field { get; set; }

        public int SemesterId { get; set; }

        public SemesterPartialDto Semester { get; set; }
    }
}