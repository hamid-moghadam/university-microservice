using System;
using System.Collections.Generic;
using Core.Domain.Enums;
using Core.Domain.Interfaces;
using Kasp.Data.Models.Helpers;

namespace Core.Domain
{
    public class Field : IFullModel
    {
        public int Id { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }

        public string Title { get; set; }
        public DegreeType DegreeType { get; set; }

        public int FieldGroupId { get; set; }
        public FieldGroup FieldGroup { get; set; }

        public ICollection<Curriculum> Curriculums { get; set; }
        public ICollection<Student> Students { get; set; }
        public ICollection<CurriculumSchedule> CurriculumSchedules { get; set; }
    }
}