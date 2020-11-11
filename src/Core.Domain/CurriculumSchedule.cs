using System;
using Core.Domain.Interfaces;

namespace Core.Domain
{
    public class CurriculumSchedule : IFullModel
    {
        public int Id { get; set; }
        public DateTime? UpdateTime { get; set; }
        public DateTime CreateTime { get; set; }

        public int CurrentSemesterId { get; set; }
        public Semester CurrentSemester { get; set; }

        // public SemesterRange AllowedSemesterRange { get; set; }
        public int FromSemesterId { get; set; }
        public Semester FromSemester { get; set; }

        public int ToSemesterId { get; set; }
        public Semester ToSemester { get; set; }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public int FieldGroupId { get; set; }
        public FieldGroup FieldGroup { get; set; }
    }

    // public class SemesterRange
    // {
    //     public int FromSemesterId { get; set; }
    //     public Semester FromSemester { get; set; }
    //
    //     public int ToSemesterId { get; set; }
    //     public Semester StartSemester { get; set; }
    // }
}