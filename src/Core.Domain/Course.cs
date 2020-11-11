using System;
using System.Collections.Generic;
using Core.Domain.Enums;
using Core.Domain.Interfaces;
using Kasp.Data.Models.Helpers;

namespace Core.Domain
{
    public class Course : IFullModel
    {
        public int Id { get; set; }
        public DateTime? UpdateTime { get; set; }
        public DateTime CreateTime { get; set; }

        public string Title { get; set; }
        public string Code { get; set; }

        public ushort PracticalUnitCount { get; set; }
        public ushort TheoryUnitCount { get; set; }
        
        public CourseType Type { get; set; }
        
        public ICollection<Curriculum> Curriculums { get; set; }
    }
}