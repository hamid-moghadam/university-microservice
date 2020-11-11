using Core.Events;
using Kasp.Data.Models;

namespace Curriculum.API.Dto
{
    public class StudentCurriculumFilterDto : FilterBase
    {
        public string UserId { get; set; }

        public StudentCurriculumStatus? Status { get; set; }

        public int? SemesterId { get; set; }
    }
}