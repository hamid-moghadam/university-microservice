using Kasp.Data.Models;

namespace Core.Application.Curriculums
{
    public class CurriculumFilterDto : FilterBase
    {
        public int SemesterId { get; set; }

        public int FieldId { get; set; }
    }
}