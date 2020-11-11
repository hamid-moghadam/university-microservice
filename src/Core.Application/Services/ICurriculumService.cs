using System.Threading.Tasks;
using Core.Application.Curriculums;
using Core.Domain;
using Core.Modules.EF.Abstraction;

namespace Core.Application.Services
{
    public interface ICurriculumService : IAppFilteredService<Curriculum, CurriculumFilterDto>
    {
        public Task<bool> ReserveAsync(int id);
        public Task<int> FreeAsync(int id);
    }
}