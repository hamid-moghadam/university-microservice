using System;
using Core.Application.Dto.FieldGroup;
using Core.Application.Dto.Semester;
using Kasp.Data.Models.Helpers;

namespace Core.Application.Dto.CurriculumSchedule
{
    public class CurriculumScheduleDto : IModel<int>
    {
        public int Id { get; set; }
        public DateTime? UpdateTime { get; set; }
        public DateTime CreateTime { get; set; }

        public SemesterPartialDto CurrentSemester { get; set; }

        public SemesterPartialDto FromSemester { get; set; }

        public SemesterPartialDto ToSemester { get; set; }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public int FieldGroupId { get; set; }
    }
}