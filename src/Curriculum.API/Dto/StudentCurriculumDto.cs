using System;
using Curriculum.API.Data.Models;

namespace Curriculum.API.Dto
{
    public class StudentCurriculumDto
    {
        public int Id { get; set; }
        public CurriculumDto Curriculum { get; set; }
        public DateTime CreateTime { get; set; }
        public StudentCurriculumStatus Status { get; set; }
    }
}