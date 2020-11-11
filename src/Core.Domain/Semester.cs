using System;
using System.Collections.Generic;
using System.Linq;
using Core.Domain.Enums;
using Core.Domain.Interfaces;
using Kasp.Data.Models.Helpers;

namespace Core.Domain
{
    public class Semester : IFullModel
    {
        public int Id { get; set; }
        public DateTime? UpdateTime { get; set; }
        public DateTime CreateTime { get; set; }

        public string Year { get; set; }
        public SemesterType Type { get; set; }
        public DateTime? ActivatedAt { get; set; }

        public string Title => $"{Year}-{Type.GetNumber()}";

        public int IntegerTitle => int.Parse($"{Year}{Type.GetNumber()}");

        public ICollection<CurriculumSchedule> CurriculumSchedules { get; set; }
        public ICollection<Curriculum> Curriculums { get; set; }
    }
}