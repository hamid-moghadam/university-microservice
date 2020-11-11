using System;
using System.Collections.Generic;
using Core.Domain.Enums;
using Core.Domain.Interfaces;
using Kasp.Data.Models.Helpers;

namespace Core.Domain
{
    public class Curriculum : IFullModel
    {
        public int Id { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }

        public ushort Capacity { get; set; }
        public ushort ReservedCapacity { get; set; }

        public string StartTime { get; set; }
        public string EndTime { get; set; }

        public int RemainingCapacity => Capacity - ReservedCapacity;
        public bool IsCapacityCompleted => RemainingCapacity <= 0;

        public CurriculumDay Day { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }

        public int FieldId { get; set; }
        public Field Field { get; set; }

        public int SemesterId { get; set; }
        public Semester Semester { get; set; }

        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
    }
}