using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Core.Domain.Enums
{
    public enum CourseType
    {
        [Display(Name = "عمومی")] Universal = 1,
        [Display(Name = "اصلی")] Primary,
        [Display(Name = "اختصاصی")] Particular,
        [Display(Name = "اختیاری")] Optional
    }
}