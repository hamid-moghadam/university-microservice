using System.Threading.Tasks;
using HttpAggregator.Dto.Grpc;

namespace HttpAggregator.Services.Interfaces
{
    public interface IStudentCurriculumGrpcService
    {
        Task<StudentInformationGrpcDto> GetInfoAsync(string userId);
    }
}