using Core.Domain;
using Core.Modules.EF.Abstraction;

namespace Core.Application.Services
{
    public interface ICourseService : IAppFilteredService<Course>
    {
    }
}