using System;
using Core.Domain.Enums;

namespace Core.Events
{
    public interface ICurriculumAddedResponse
    {
        CurriculumResponse CurriculumResponse { get; set; }
        StudentResponse StudentResponse { get; set; }
        StudentCurriculumStatus Status { get; set; }
        string StatusDescription { get; set; }
    }

    public class CurriculumAddedResponse : ICurriculumAddedResponse
    {
        public CurriculumResponse CurriculumResponse { get; set; }

        public StudentResponse StudentResponse { get; set; }
        public StudentCurriculumStatus Status { get; set; }
        public string StatusDescription { get; set; }
    }

    public enum StudentCurriculumStatus
    {
        Pending,
        Rejected,
        Accepted
    }

    public class StudentResponse
    {
        public int Id { get; set; }
        public DateTime CreateTime { get; set; }

        public string Code { get; set; }
        public string UserId { get; set; }
    }

    public class CurriculumResponse
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

    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }

        public ushort PracticalUnitCount { get; set; }
        public ushort TheoryUnitCount { get; set; }

        public CourseType Type { get; set; }
    }

    public class Teacher
    {
        public int Id { get; set; }
        public string PersonnelId { get; set; }
        public string UserId { get; set; }
        public string FullName { get; set; }
    }

    public class Field
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DegreeType DegreeType { get; set; }
    }

    public class Semester
    {
        public int Id { get; set; }
        public string Year { get; set; }
        public SemesterType Type { get; set; }

        public string Title { get; set; }
    }
}