using System;
using Core.Domain.Interfaces;
using Kasp.Data.Models.Helpers;

namespace Core.Domain
{
    public class Student : IFullModel, IUser
    {
        public int Id { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }

        public string Code { get; set; }
        public string UserId { get; set; }

        public string FullName { get; set; }

        public int FieldId { get; set; }
        public Field Field { get; set; }

        public int SemesterId { get; set; }
        public Semester Semester { get; set; }
    }
}