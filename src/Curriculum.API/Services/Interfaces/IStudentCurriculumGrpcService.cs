using System.Threading.Tasks;
using Curriculum.API.Dto.Grpc;

namespace Curriculum.API.Services.Interfaces
{
    public interface IStudentCurriculumGrpcService
    {
        Task<SemesterStatusDto> GetSemesterStatusAsync(int curriculumId, int studentId);
    }
}