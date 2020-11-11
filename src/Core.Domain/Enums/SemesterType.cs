using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Core.Domain.Enums
{
    public enum SemesterType
    {
        [Display(Name = "نیمسال اول")] First1 = 1,
        [Display(Name = "نیمسال دوم")] Second2,
        [Display(Name = "نیمسال سوم")] Third3
    }

    public static class SemesterExtension
    {
        public static char GetNumber(this SemesterType type)
        {
            return (Enum.GetName(typeof(SemesterType), type) ?? "1").Last();
        }
    }
}