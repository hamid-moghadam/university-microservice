using System.Threading.Tasks;
using Core.Modules.EF.Abstraction;
using Curriculum.API.Data.Models;
using Curriculum.API.Dto;

namespace Curriculum.API.Services.Interfaces
{
    public interface IStudentCurriculumService : IAppFilteredService<StudentCurriculum, StudentCurriculumFilterDto>
    {
        Task<int> GetCurriculumCount(int semesterId, string userId);
    }
}