using System;
using System.Threading.Tasks;
using Grpc.Curriculum;
using Grpc.Net.Client;
using HttpAggregator.Dto.Grpc;
using HttpAggregator.Services.Interfaces;
using Microsoft.Extensions.Configuration;

namespace HttpAggregator.Services
{
    public class CurriculumGrpcService : ICurriculumGrpcService
    {
        private readonly IConfiguration _configuration;

        public CurriculumGrpcService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<CurriculumOverviewGrpcDto> GetOverviewAsync(int semesterId, string userId)
        {
            AppContext.SetSwitch(
                "System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

            using var curriculumChannel = GrpcChannel.ForAddress(_configuration["CurriculumGrpc"]);

            var curriculumClient = new StudentCurriculumService.StudentCurriculumServiceClient(curriculumChannel);

            var curriculumInfo = await curriculumClient.GetOverviewAsync(new OverviewRequest
            {
                SemesterId = semesterId,
                UserId = userId
            });

            AppContext.SetSwitch(
                "System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", false);

            return new CurriculumOverviewGrpcDto
            {
                TotalAcceptedCount = curriculumInfo.TotalCount
            };
        }
    }
}