using System;
using Core.Domain.Interfaces;
using Kasp.Data.Models.Helpers;

namespace Core.Domain
{
    public class Teacher : IFullModel, IUser
    {
        public int Id { get; set; }
        public DateTime? UpdateTime { get; set; }
        public DateTime CreateTime { get; set; }


        public string UserId { get; set; }

        public string FullName { get; set; }
        public string PersonnelId { get; set; }
    }
}