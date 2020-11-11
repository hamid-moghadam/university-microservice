using Core.Domain.Enums;
using Curriculum.API.Data.Models;

namespace Curriculum.API.Dto
{
    public class CurriculumDto
    {
        public int Id { get; set; }
        public ushort Capacity { get; set; }
        public ushort ReservedCapacity { get; set; }

        public string StartTime { get; set; }
        public string EndTime { get; set; }

        public int RemainingCapacity { get; set; }
        public bool IsCapacityCompleted { get; set; }
        public CurriculumDay Day { get; set; }

        public Course Course { get; set; }

        public Field Field { get; set; }

        public Teacher Teacher { get; set; }

        public Semester Semester { get; set; }
    }
}