﻿using Core.Domain;
using Core.Modules.EF.Abstraction;

namespace Core.Application.Services
{
    public interface IStudentService : IAppFilteredService<Student>
    {
    }
}