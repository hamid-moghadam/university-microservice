using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Core.Domain.Enums
{
    public enum DegreeType
    {
        [Display(Name = "کاردانی")] Associate = 1,
        [Display(Name = "کارشناسی")] Bachelor,
        [Display(Name = "ارشد")] Master,
        [Display(Name = "دکتر")] Doctoral,
        [Display(Name = "فوق دکترا")] PostDoc
    }
}