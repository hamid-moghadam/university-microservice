using System.Threading.Tasks;
using HttpAggregator.Dto.Grpc;

namespace HttpAggregator.Services.Interfaces
{
    public interface ICurriculumGrpcService
    {
        Task<CurriculumOverviewGrpcDto> GetOverviewAsync(int semesterId, string userId);
    }
}