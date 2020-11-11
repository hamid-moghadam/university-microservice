using System;
using System.Collections.Generic;
using Core.Domain.Interfaces;
using Kasp.Data.Models.Helpers;

namespace Core.Domain
{
    public class FieldGroup : IFullModel
    {
        public int Id { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }

        public string Title { get; set; }
        public ICollection<Field> Fields { get; set; }
        public ICollection<CurriculumSchedule> CurriculumSchedules { get; set; }
    }
}