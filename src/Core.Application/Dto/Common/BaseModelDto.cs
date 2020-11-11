using System;
using System.ComponentModel;
using Kasp.Data.Models.Helpers;

namespace Core.Application.Dto.Common
{
    public class BaseModelDto : IModel, ICreateTime
    {
        [DisplayName("شناسه")] public int Id { get; set; }

        [DisplayName("تاریخ ایجاد")] public DateTime CreateTime { get; set; }
    }
}