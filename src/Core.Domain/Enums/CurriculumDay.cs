using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Core.Domain.Enums
{
    public enum CurriculumDay
    {
        [Display(Name = "شنبه")] Saturday = 1,
        [Display(Name = "یکشنبه")] Sunday,
        [Display(Name = "دوشنبه")] Monday,
        [Display(Name = "سه‌شنبه")] Tuesday,
        [Display(Name = "چهارشنبه")] Wednesday,
        [Display(Name = "پنج‌شنبه")] Thursday,
        [Display(Name = "جمعه")] Friday
    }
}