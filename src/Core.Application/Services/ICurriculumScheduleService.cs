using System;
using System.Threading.Tasks;
using Core.Application.Dto.CurriculumSchedule;
using Core.Domain;
using Core.Modules.EF.Abstraction;

namespace Core.Application.Services
{
    public interface ICurriculumScheduleService : IAppFilteredService<CurriculumSchedule>
    {
        Task<Tuple<CurriculumScheduleDto, bool>> GetCurrentScheduleAsync(int currentSemesterId,
            int studentSemesterIntegerTitle, int fieldGroupId);
    }
}